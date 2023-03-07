using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IDeploymentConfigurationRepository : IAggregateRootRepository<DeploymentConfiguration, string>, IScopedDependency
{

    /// <summary>
    /// 分页查询部署配置
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<(DeploymentConfigurationOutputDto[] Data, int TotalCount)> GetDeploymentConfigurationPageListAsync(string appId, DeploymentConfigurationQueryDto query);

    /// <summary>
    /// 根据Id获取一个应用部署数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<DeploymentConfiguration?> FindApplicationDeploymentByIdAsync(string id);


    /// <summary>
    /// 根据Name获取一个应用部署数据
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<DeploymentConfiguration?> FindDeploymentConfigurationByAppIdAndNameAsync(string appId, string name);
    /// <summary>
    /// 根据AppID获取部署列表
    /// </summary>
    /// <param name="appId"></param>
    /// <returns></returns>
    Task<List<DeploymentConfiguration>> GetDeploymentConfigurationByAppIdListAsync(string appId);
}