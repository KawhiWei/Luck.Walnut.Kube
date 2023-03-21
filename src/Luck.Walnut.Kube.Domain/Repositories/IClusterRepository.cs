using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Dto.Clusteries;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IClusterRepository : IAggregateRootRepository<Cluster, string>, IScopedDependency
{
    /// <summary>
    /// 获取所有集群列表
    /// </summary>
    /// <returns></returns>
    Task<List<ClusterOutputDto>> GetClusterOutputDtoListAsync();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<(Cluster[] Data, int TotalCount)> GetClusterPageListAsync(ClusterQueryDto query);
    
    /// <summary>
    /// 查询一个集群信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Cluster?> GetClusterFindByIdAsync(string id);

    /// <summary>
    /// 根据Id查询集群列表
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    Task<List<Cluster>> GetClusterFindByIdListAsync(List<string> idList);
}