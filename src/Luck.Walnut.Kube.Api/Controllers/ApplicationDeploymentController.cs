using Luck.Walnut.Kube.Application.ApplicationDeployments;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;
using Luck.Walnut.Kube.Query.ApplicationDeployments;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers;

/// <summary>
/// 应用部署管理
/// </summary>
[Route("api/application/deployments")]
public class ApplicationDeploymentController : BaseController
{
    #region 部署配置接口

    /// <summary>
    /// 分页查询部署配置
    /// </summary>
    /// <param name="applicationDeploymentQueryService"></param>
    /// <param name="appId"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{appId}/page/list")]
    public Task<PageBaseResult<ApplicationDeploymentOutputDto>> GetApplicationDeploymentPageList([FromServices] IApplicationDeploymentQueryService applicationDeploymentQueryService, string appId, [FromQuery] ApplicationDeploymentQueryDto query)
        => applicationDeploymentQueryService.GetApplicationDeploymentPageListAsync(appId, query);



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
    /// <returns></returns>
    [HttpDelete("{id}")]
    public Task DeleteApplicationDeployment([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id)
        => applicationDeploymentApplication.DeleteApplicationDeploymentAsync(id);

    #endregion

    #region K8S部署容器配置接口

    /// <summary>
    /// 添加容器配置
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("{id}/container")]
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
    [HttpPut("{id}/{applicationContainerId}/container")]
    public Task UpdateApplicationContainer([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id, string applicationContainerId, [FromBody] ApplicationContainerInputDto input)
        => applicationDeploymentApplication.UpdateApplicationContainerAsync(id, applicationContainerId, input);


    /// <summary>
    /// 删除容器配置
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="applicationContainerId"></param>
    /// <returns></returns>
    [HttpDelete("{id}/{applicationContainerId}/container")]
    public Task DeleteApplicationContainer([FromServices] IApplicationDeploymentApplication applicationDeploymentApplication, string id, string applicationContainerId)
        => applicationDeploymentApplication.DeleteApplicationContainerAsync(id, applicationContainerId);


    /// <summary>
    /// 根据ApplicationDeploymentId查询一组部署容器配置
    /// </summary>
    /// <param name="applicationContainerQueryService"></param>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    [HttpGet("{applicationDeploymentId}/container/list")]
    public Task<List<ApplicationContainerOutputDto>> GetApplicationContainerList([FromServices] IApplicationContainerQueryService applicationContainerQueryService, string applicationDeploymentId)
        => applicationContainerQueryService.GetListByApplicationDeploymentIdAsync(applicationDeploymentId);

    #endregion
}