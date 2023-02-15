using Luck.Walnut.Kube.Application.Clusters;
using Luck.Walnut.Kube.Dto.Clusteries;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers;

/// <summary>
/// 命名空间管理
/// </summary>
[Route("api/namespaces")]
public class NameSpaceController : BaseController
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
    
}