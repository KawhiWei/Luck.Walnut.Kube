using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Query.ApplicationDeployments;

public class ApplicationContainerQueryService : IApplicationContainerQueryService
{
    private readonly IApplicationContainerRepository _applicationContainerRepository;

    public ApplicationContainerQueryService(IApplicationContainerRepository applicationContainerRepository)
    {
        _applicationContainerRepository = applicationContainerRepository;
    }

    public Task<List<ApplicationContainerOutputDto>> GetListByApplicationDeploymentIdAsync(string applicationDeploymentId)
        => _applicationContainerRepository.GetListByApplicationDeploymentIdAsync(applicationDeploymentId);
}