using Luck.Walnut.Kube.Dto.Clusteries;
using Luck.Walnut.Kube.Dto.Kubernetes;

namespace Luck.Walnut.Kube.Application.Clusters;

public interface IClusterApplication : IScopedDependency
{
    Task CreateClusterAsync(ClusterInputDto input);

    Task UpdateClusterAsync(string id,ClusterInputDto input);

    Task DeleteClusterAsync(string id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<KubernetesClusterDashboardOutputDto> GetClusterDashboardAsync(string id);
}