using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IApplicationDeploymentRepository : IAggregateRootRepository<ApplicationDeployment, string>, IScopedDependency
{
    /// <summary>
    /// 根据Id获取一个应用部署数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ApplicationDeployment?> GetApplicationDeploymentByIdAsync(string id);

    /// <summary>
    /// 根据AppID获取部署列表
    /// </summary>
    /// <param name="appId"></param>
    /// <returns></returns>
    Task<List<ApplicationDeployment>> GetApplicationDeploymentByAppIdListAsync(string appId);
}