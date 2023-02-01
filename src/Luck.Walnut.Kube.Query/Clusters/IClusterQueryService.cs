using Luck.Framework.Infrastructure.DependencyInjectionModule;
using Luck.Walnut.Kube.Dto.Clusteries;

namespace Luck.Walnut.Kube.Query.Clusters;

public interface IClusterQueryService : IScopedDependency
{
    /// <summary>
    /// 获取k8s集群列表
    /// </summary>
    /// <returns></returns>
    Task<List<ClusterOutputDto>> GetClusterOutputDtoListAsync();

    /// <summary>
    /// 根据Id获取一个集群信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ClusterOutputDto> GetClusterFindByIdAsync(string id);
}