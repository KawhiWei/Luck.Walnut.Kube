using Luck.Walnut.Kube.Application.Clusters;
using Luck.Walnut.Kube.Dto.Clusteries;
using Luck.Walnut.Kube.Dto.Kubernetes;
using Luck.Walnut.Kube.Query.Clusters;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers;

/// <summary>
/// 集群管理
/// </summary>
[Route("api/clusters")]
public class ClusterController : BaseController
{
    
    /// <summary>
    /// 添加一个资源
    /// </summary>
    /// <param name="clusterApplication"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task CreateCluster([FromServices] IClusterApplication clusterApplication, [FromBody] ClusterInputDto input)
        => clusterApplication.CreateClusterAsync(input);

    

    /// <summary>
    /// 修改集群
    /// </summary>
    /// <param name="clusterApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public Task UpdateCluster([FromServices] IClusterApplication clusterApplication,string id, [FromBody] ClusterInputDto input)
        => clusterApplication.CreateClusterAsync(input);


    /// <summary>
    /// 获取集群资源仪表盘
    /// </summary>
    /// <param name="clusterApplication"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/cluster/resource/dashboard")]
    public Task<KubernetesClusterDashboardOutputDto> ClusterDashboard([FromServices] IClusterApplication clusterApplication, string id)
        => clusterApplication.GetClusterDashboardAsync(id);

    /// <summary>
    /// 获取所有集群列表
    /// </summary>
    /// <param name="clusterQueryService"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public Task<List<ClusterOutputDto>> GetClusterList([FromServices] IClusterQueryService clusterQueryService)
        => clusterQueryService.GetClusterOutputDtoListAsync();

    /// <summary>
    /// 根据ID获取一个集群信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="clusterQueryService"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Task<ClusterOutputDto> GetClusterFindById(string id, [FromServices] IClusterQueryService clusterQueryService)
        => clusterQueryService.GetClusterFindByIdAsync(id);
}