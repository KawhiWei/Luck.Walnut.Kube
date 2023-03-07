using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IDeploymentContainerConfigurationRepository : IEntityRepository<DeploymentContainerConfiguration, string>, IScopedDependency
{
    /// <summary>
    /// 根据Id获取一个应用部署数据
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    Task<List<DeploymentContainerConfiguration>> GetApplicationContainerListByApplicationDeploymentIdAsync(string applicationDeploymentId);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    Task<List<DeploymentContainerConfiguration>> GetListByApplicationDeploymentIdAsync(string applicationDeploymentId);

    /// <summary>
    /// 根据Id获取一个容器配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<DeploymentContainerConfiguration?> FindApplicationContainerByIdFirstOrDefaultAsync(string id);
}