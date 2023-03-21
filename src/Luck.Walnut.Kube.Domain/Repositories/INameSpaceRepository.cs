using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Dto.NameSpaces;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface INameSpaceRepository: IAggregateRootRepository<NameSpace,string>,IScopedDependency
{

    /// <summary>
    /// 分页查询命名空间列表
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<(NameSpace[] Data, int TotalCount)> GetNameSpacePageListAsync(NameSpaceQueryDto query);
    
    /// <summary>
    /// 根据名称和集群id查询是否存在命名空间
    /// </summary>
    /// <param name="name"></param>
    /// <param name="clusterId"></param>
    /// <returns></returns>
    Task<NameSpace?> FindNameSpaceByNameAndClusterIdAsync(string name,string clusterId);
    /// <summary>
    /// 根据id 查询一个命名空间
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<NameSpace?> FindNameSpaceByIdAsync(string id);
}