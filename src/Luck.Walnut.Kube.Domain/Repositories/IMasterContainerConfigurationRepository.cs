using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IMasterContainerConfigurationRepository : IEntityRepository<MasterContainerConfiguration, string>, IScopedDependency
{
    /// <summary>
    /// 根据Id获取一个应用部署数据
    /// </summary>
    /// <param name="deploymentId"></param>
    /// <returns></returns>
    Task<List<MasterContainerConfiguration>> GetApplicationContainerListByApplicationDeploymentIdAsync(string deploymentId);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="deploymentId"></param>
    /// <returns></returns>
    Task<List<MasterContainerConfiguration>> GetListByDeploymentIdAsync(string deploymentId);

    /// <summary>
    /// 根据Id获取一个容器配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<MasterContainerConfiguration?> FindMasterContainerByIdFirstOrDefaultAsync(string id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deploymentIds"></param>
    /// <returns></returns>
    Task<List<MasterContainerConfiguration>> GetListByDeploymentIdsAsync(List<string> deploymentIds);
}