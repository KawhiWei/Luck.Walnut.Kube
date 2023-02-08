using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Query.ApplicationDeployments;

public interface IApplicationContainerQueryService : IScopedDependency
{
    /// <summary>
    /// 根据ApplicationDeploymentId查询一组部署容器配置
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    Task<List<ApplicationContainerOutputDto>> GetListByApplicationDeploymentIdAsync(string applicationDeploymentId);
}