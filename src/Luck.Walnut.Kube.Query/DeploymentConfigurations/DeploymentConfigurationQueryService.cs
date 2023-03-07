using IdentityModel.OidcClient;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Query.DeploymentConfigurations;

public class DeploymentConfigurationQueryService : IDeploymentConfigurationQueryService
{

    private readonly IDeploymentConfigurationRepository _applicationDeploymentRepository;

    public DeploymentConfigurationQueryService(IDeploymentConfigurationRepository applicationDeploymentRepository)
    {
        _applicationDeploymentRepository = applicationDeploymentRepository;
    }

    public async Task<PageBaseResult<DeploymentConfigurationOutputDto>> GetDeploymentConfigurationPageListAsync(string appId, DeploymentConfigurationQueryDto query)
    {

        var (Data, TotalCount) = await _applicationDeploymentRepository.GetDeploymentConfigurationPageListAsync(appId, query);
        return new PageBaseResult<DeploymentConfigurationOutputDto>(TotalCount, Data);
    }


}