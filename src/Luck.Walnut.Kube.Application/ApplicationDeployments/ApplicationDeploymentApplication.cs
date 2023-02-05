using k8s.Models;
using Luck.Framework.Exceptions;
using Luck.Framework.Extensions;
using Luck.Framework.UnitOfWorks;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Application.ApplicationDeployments;

public class ApplicationDeploymentApplication : IApplicationDeploymentApplication
{
    private readonly IApplicationDeploymentRepository _applicationDeploymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApplicationDeploymentApplication(IApplicationDeploymentRepository applicationDeploymentRepository, IUnitOfWork unitOfWork)
    {
        _applicationDeploymentRepository = applicationDeploymentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateApplicationDeploymentAsync(ApplicationDeploymentInputDto input)
    {
        if (await CheckIsExitApplicationDeploymentAsync(input.AppId, input.Name))
        {
            throw new BusinessException($"[{input.Name}]已存在，请刷新页面");
        }

        var applicationDeployment = new ApplicationDeployment(input.EnvironmentName,
            input.ApplicationRuntimeType, input.DeploymentType, input.ChineseName, input.Name, input.AppId,
            input.KubernetesNameSpaceId, input.Replicas, input.MaxUnavailable, input.ImagePullSecretId, false);
        _applicationDeploymentRepository.Add(applicationDeployment);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateApplicationDeploymentAsync(string id, ApplicationDeploymentInputDto input)
    {
        var applicationDeployment = await GetAndCheckApplicationDeploymentAsync(id);
        applicationDeployment.SetApplicationDeployment(input);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteApplicationDeploymentAsync(string id)
    {
        var applicationDeployment = await GetAndCheckApplicationDeploymentAsync(id);
        _applicationDeploymentRepository.Remove(applicationDeployment);
        await _unitOfWork.CommitAsync();
    }


    public async Task DeleteApplicationContainerAsync(string id, string applicationContainerId)
    {
        var applicationDeployment = await GetAndCheckApplicationDeploymentAsync(id);
        applicationDeployment.RemoveContainer(applicationContainerId);
        await _unitOfWork.CommitAsync();
    }


    private async Task<ApplicationDeployment> GetAndCheckApplicationDeploymentAsync(string id)
    {
        var cluster = await _applicationDeploymentRepository.GetApplicationDeploymentByIdAsync(id);
        if (cluster is null)
        {
            throw new BusinessException("集群不存在，请刷新页面");
        }

        return cluster;
    }

    private async Task<bool> CheckIsExitApplicationDeploymentAsync(string appId, string name)
    {
        var cluster = await _applicationDeploymentRepository.GetApplicationDeploymentByAppIdAndNameAsync(appId, name);
        if (cluster is null)
        {
            return false;
        }

        return true;
    }


    private V1Deployment GetDeployment(ApplicationDeployment applicationDeployment)
    {
        var v1Deployment = new V1Deployment();


        v1Deployment.Metadata.Name = applicationDeployment.Name;
        v1Deployment.Spec.Replicas = applicationDeployment.Replicas;
        v1Deployment.Metadata.NamespaceProperty = applicationDeployment.KubernetesNameSpaceId;

        v1Deployment.Spec.Template.Spec.InitContainers = GetV1Containers(applicationDeployment.ApplicationContainers.Where(x => x.IsInitContainer));
        v1Deployment.Spec.Template.Spec.Containers = GetV1Containers(applicationDeployment.ApplicationContainers.Where(x => !x.IsInitContainer));
        return v1Deployment;
    }

    private List<V1Container> GetV1Containers(IEnumerable<ApplicationContainer> applicationContainers)
    {
        var containers = applicationContainers.ToList();
        var v1Containers = new List<V1Container>(containers.Count);

        foreach (var applicationContainer in containers)
        {
            var limits = new Dictionary<string, ResourceQuantity>();

            var v1Container = new V1Container
            {
                Name = applicationContainer.ContainerName,
                Image = applicationContainer.Image
            };
            if (applicationContainer.ReadinessProbe is not null)
            {
                v1Container.ReadinessProbe = new V1Probe()
                {
                    PeriodSeconds = v1Container.ReadinessProbe.PeriodSeconds,
                    InitialDelaySeconds = v1Container.ReadinessProbe.InitialDelaySeconds,
                };
            }

            if (applicationContainer.LiveNessProbe is not null)
            {
                v1Container.LivenessProbe = new V1Probe()
                {
                    PeriodSeconds = v1Container.LivenessProbe.PeriodSeconds,
                    InitialDelaySeconds = v1Container.LivenessProbe.InitialDelaySeconds,
                };
            }

            v1Container.Resources = new V1ResourceRequirements();

            if (applicationContainer.Limits is not null)
            {
                limits.Add(applicationContainer.Limits.Name, new ResourceQuantity(applicationContainer.Limits.Cpu));
                limits.Add(applicationContainer.Limits.Memory, new ResourceQuantity(applicationContainer.Limits.Memory));
                v1Container.Resources.Limits = limits;
            }

            v1Containers.Add(v1Container);
        }

        return v1Containers;
    }
}