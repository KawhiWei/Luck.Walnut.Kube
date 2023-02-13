using IdentityModel.OidcClient;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Query.ApplicationDeployments;

public class ApplicationDeploymentQueryService : IApplicationDeploymentQueryService
{

    private readonly IApplicationDeploymentRepository _applicationDeploymentRepository;

    public ApplicationDeploymentQueryService(IApplicationDeploymentRepository applicationDeploymentRepository)
    {
        _applicationDeploymentRepository = applicationDeploymentRepository;
    }

    public async Task<PageBaseResult<ApplicationDeploymentOutputDto>> GetApplicationDeploymentPageListAsync(string appId, ApplicationDeploymentQueryDto query)
    {

        var (Data, TotalCount) = await _applicationDeploymentRepository.GetApplicationDeploymentPageListAsync(appId, query);
        return new PageBaseResult<ApplicationDeploymentOutputDto>(TotalCount, Data);
    }


}