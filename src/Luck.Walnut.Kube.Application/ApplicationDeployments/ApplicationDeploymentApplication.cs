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

    /// <summary>
    /// 添加部署
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="BusinessException"></exception>
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

    /// <summary>
    /// 修改部署
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    public async Task UpdateApplicationDeploymentAsync(string id, ApplicationDeploymentInputDto input)
    {
        var applicationDeployment = await GetAndCheckApplicationDeploymentAsync(id);
        applicationDeployment.SetApplicationDeployment(input);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 删除部署
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteApplicationDeploymentAsync(string id)
    {
        var applicationDeployment = await GetAndCheckApplicationDeploymentAsync(id);
        _applicationDeploymentRepository.Remove(applicationDeployment);
        await _unitOfWork.CommitAsync();
    }


    #region ApplicationContainer

    /// <summary>
    /// 添加容器配置
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    public async Task CreateApplicationContainerAsync(string id, ApplicationContainerInputDto input)
    {
        var applicationDeployment = await GetAndCheckApplicationDeploymentAsync(id);

        applicationDeployment.AddApplicationContainer(input);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 删除容器配置
    /// </summary>
    /// <param name="id"></param>
    /// <param name="applicationContainerId"></param>
    /// <param name="input"></param>
    public async Task UpdateApplicationContainerAsync(string id, string applicationContainerId, ApplicationContainerInputDto input)
    {
        var applicationDeployment = await GetAndCheckApplicationDeploymentAsync(id);
        applicationDeployment.UpdateApplicationContainer(applicationContainerId, input);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 删除容器配置
    /// </summary>
    /// <param name="id"></param>
    /// <param name="applicationContainerId"></param>
    public async Task DeleteApplicationContainerAsync(string id, string applicationContainerId)
    {
        var applicationDeployment = await GetAndCheckApplicationDeploymentAsync(id);
        applicationDeployment.RemoveContainer(applicationContainerId);
        await _unitOfWork.CommitAsync();
    }

    #endregion


    #region 私有Check方法

    private async Task<ApplicationDeployment> GetAndCheckApplicationDeploymentAsync(string id)
    {
        var cluster = await _applicationDeploymentRepository.GetApplicationDeploymentByIdAsync(id);
        if (cluster is null)
        {
            throw new BusinessException("部署不存在，请刷新页面");
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

    #endregion

    private V1Deployment GetDeployment(ApplicationDeployment applicationDeployment)
    {
        var v1Deployment = new V1Deployment();


        v1Deployment.Metadata.Name = applicationDeployment.Name;
        v1Deployment.Spec.Replicas = applicationDeployment.Replicas;
        v1Deployment.Metadata.NamespaceProperty = applicationDeployment.KubernetesNameSpaceId;

        applicationDeployment
            .ApplicationContainers.Where(x => x.IsInitContainer).ForEach(a =>
            {
                var limits = new Dictionary<string, ResourceQuantity>();

                var v1Container = new V1Container
                {
                    Name = a.ContainerName,

                    Image = ""
                };

                if (a.ReadinessProbe is not null)
                {
                    v1Container.ReadinessProbe = new V1Probe()
                    {
                        PeriodSeconds = v1Container.ReadinessProbe.PeriodSeconds,
                        InitialDelaySeconds = v1Container.ReadinessProbe.InitialDelaySeconds,
                    };
                }

                if (a.LiveNessProbe is not null)
                {
                    v1Container.LivenessProbe = new V1Probe()
                    {
                        PeriodSeconds = v1Container.LivenessProbe.PeriodSeconds,
                        InitialDelaySeconds = v1Container.LivenessProbe.InitialDelaySeconds,
                    };
                }

                v1Container.Resources = new V1ResourceRequirements();

                if (a.Limits is not null)
                {
                    limits.Add(a.Limits.Name, new ResourceQuantity(a.Limits.Cpu));
                    limits.Add(a.Limits.Memory, new ResourceQuantity(a.Limits.Memory));
                    v1Container.Resources.Limits = limits;
                }

                v1Deployment.Spec.Template.Spec.InitContainers.Add(v1Container);
            });

        return v1Deployment;
    }
}