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
        var applicationDeployment = new ApplicationDeployment(input.EnvironmentName,
            input.ApplicationRuntimeType, input.DeploymentType, input.ChineseName, input.Name, input.AppId,
            input.KubernetesNameSpaceId, input.Replicas, input.MaxUnavailable, input.ImagePullSecretId);
        _applicationDeploymentRepository.Add(applicationDeployment);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateApplicationDeploymentAsync(string id, ApplicationDeploymentInputDto input)
    {
        var applicationDeployment = await CheckIsExitApplicationDeploymentAsync(id);

        applicationDeployment.SetApplicationDeployment(input);
        await _unitOfWork.CommitAsync();
    }

    public Task DeleteApplicationDeploymentAsync(string id)
    {
        throw new NotImplementedException();
    }

    private async Task<ApplicationDeployment> CheckIsExitApplicationDeploymentAsync(string id)
    {
        var cluster = await _applicationDeploymentRepository.GetApplicationDeploymentByIdAsync(id);
        if (cluster is null)
        {
            throw new BusinessException("集群不存在，请刷新页面");
        }

        return cluster;
    }
}