using Luck.Walnut.Kube.Dto.Clusteries;
using Luck.Walnut.Kube.Dto.Kubernetes;

namespace Luck.Walnut.Kube.Application.Clusters;

public interface IClusterApplication:IScopedDependency
{
    Task CreateClusterAsync();


    Task<KubernetesClusterDashboardOutputDto> GetClusterDashboardAsync(string id);

    Task<List<ClusterOutputDto>> GetClusterListAsync();
}