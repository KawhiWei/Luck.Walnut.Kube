using Luck.Walnut.Kube.Application.Clusters;
using Luck.Walnut.Kube.Dto.Clusteries;
using Luck.Walnut.Kube.Dto.Kubernetes;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers;

/// <summary>
/// 集群管理
/// </summary>
[Route("api/clusters")]
public class ClusterController : BaseController
{
    private readonly IClusterApplication _clusterApplication;
    public ClusterController(IClusterApplication clusterApplication)
    {
        _clusterApplication = clusterApplication;
    }

    [HttpPost]
    public async Task CreateCluster()
    {
        await _clusterApplication.CreateClusterAsync();
    }

    [HttpGet("{id}/cluster/dashboard")]
    public async Task<KubernetesClusterDashboardOutputDto> ClusterDashboard(string id)
    {
        return await _clusterApplication.GetClusterDashboardAsync(id);
    }

    [HttpGet("list")]
    public Task<List<ClusterOutputDto>> GetClusterList()
    {
        return _clusterApplication.GetClusterListAsync();
    }
    
}