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
    public Task CreateApplicationDeployment([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, [FromBody] ApplicationDeploymentInputDto input)
        => applicationDeploymentApplication.CreateApplicationDeploymentAsync(input);


    /// <summary>
    /// 修改部署
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public Task UpdateApplicationDeployment([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id, [FromBody] ApplicationDeploymentInputDto input)
        => applicationDeploymentApplication.UpdateApplicationDeploymentAsync(id, input);

    /// <summary>
    /// 删除部署
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public Task DeleteApplicationDeployment([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id)
        => applicationDeploymentApplication.DeleteApplicationDeploymentAsync(id);

    /// <summary>
    /// 添加容器配置
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public Task CreateApplicationContainer([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id, [FromBody] ApplicationContainerInputDto input)
        => applicationDeploymentApplication.CreateApplicationContainerAsync(id, input);

    /// <summary>
    /// 修改容器配置
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="applicationContainerId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}/{applicationContainerId}")]
    public Task UpdateApplicationContainer([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id, string applicationContainerId, [FromBody] ApplicationContainerInputDto input)
        => applicationDeploymentApplication.UpdateApplicationContainerAsync(id, applicationContainerId, input);


    /// <summary>
    /// 删除容器配置
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpDelete("{id}/{applicationContainerId}")]
    public Task DeleteApplicationContainer([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id, string applicationContainerId)
        => applicationDeploymentApplication.DeleteApplicationContainerAsync(id, applicationContainerId);
}