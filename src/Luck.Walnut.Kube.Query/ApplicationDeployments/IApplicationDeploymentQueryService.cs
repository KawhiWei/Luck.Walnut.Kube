

using Luck.Walnut.Kube.Dto.ApplicationDeployments;
using Luck.Walnut.Kube.Dto;

namespace Luck.Walnut.Kube.Query.ApplicationDeployments;

public interface IApplicationDeploymentQueryService:IScopedDependency
{

    /// <summary>
    /// ∑÷“≥≤È—Ø≤ø ≈‰÷√
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<PageBaseResult<ApplicationDeploymentOutputDto>> GetApplicationDeploymentPageListAsync(string appId, ApplicationDeploymentQueryDto query);

}