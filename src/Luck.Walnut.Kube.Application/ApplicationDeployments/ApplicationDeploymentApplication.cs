using Luck.Framework.Exceptions;
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
}