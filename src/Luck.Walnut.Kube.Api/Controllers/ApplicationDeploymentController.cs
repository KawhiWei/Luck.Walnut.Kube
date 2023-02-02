using Luck.Walnut.Kube.Application.ApplicationDeployments;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers;

/// <summary>
/// 应用部署管理
/// </summary>
[Route("api/application/deployments")]
public class ApplicationDeploymentController : BaseController
{
    /// <summary>
    /// 添加部署
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task CreateCluster([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, [FromBody] ApplicationDeploymentInputDto input)
        => applicationDeploymentApplication.CreateApplicationDeploymentAsync(input);


    /// <summary>
    /// 修改部署
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public Task UpdateCluster([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id, [FromBody] ApplicationDeploymentInputDto input)
        => applicationDeploymentApplication.UpdateApplicationDeploymentAsync(id, input);
}