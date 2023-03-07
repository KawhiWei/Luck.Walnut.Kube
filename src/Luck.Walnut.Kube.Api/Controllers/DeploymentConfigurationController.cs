using Luck.Walnut.Kube.Application.DeploymentConfigurations;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;
using Luck.Walnut.Kube.Query.ApplicationDeployments;
using Luck.Walnut.Kube.Query.DeploymentConfigurations;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers;

/// <summary>
/// 应用部署管理
/// </summary>
[Route("api/application/deployments")]
public class DeploymentConfigurationController : BaseController
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
    public Task<PageBaseResult<DeploymentConfigurationOutputDto>> GetApplicationDeploymentPageList([FromServices] IDeploymentConfigurationQueryService applicationDeploymentQueryService, string appId, [FromQuery] DeploymentConfigurationQueryDto query)
        => applicationDeploymentQueryService.GetDeploymentConfigurationPageListAsync(appId, query);



    /// <summary>
    /// 添加部署
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task CreateApplicationDeployment([FromServices] IDeploymentConfigurationApplication applicationDeploymentApplication, [FromBody] DeploymentConfigurationInputDto input)
        => applicationDeploymentApplication.CreateDeploymentConfigurationAsync(input);


    /// <summary>
    /// 修改部署
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public Task UpdateApplicationDeployment([FromServices] IDeploymentConfigurationApplication applicationDeploymentApplication, string id, [FromBody] DeploymentConfigurationInputDto input)
        => applicationDeploymentApplication.UpdateDeploymentConfigurationAsync(id, input);

    /// <summary>
    /// 删除部署
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public Task DeleteApplicationDeployment([FromServices] IDeploymentConfigurationApplication applicationDeploymentApplication, string id)
        => applicationDeploymentApplication.DeleteDeploymentConfigurationAsync(id);

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
    public Task CreateApplicationContainer([FromServices] IDeploymentConfigurationApplication applicationDeploymentApplication, string id, [FromBody] DeploymentContainerConfigurationInputDto input)
        => applicationDeploymentApplication.CreateDeploymentContainerAsync(id, input);

    /// <summary>
    /// 修改容器配置
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="applicationContainerId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}/{applicationContainerId}/container")]
    public Task UpdateApplicationContainer([FromServices] IDeploymentConfigurationApplication applicationDeploymentApplication, string id, string applicationContainerId, [FromBody] DeploymentContainerConfigurationInputDto input)
        => applicationDeploymentApplication.UpdateDeploymentContainerAsync(id, applicationContainerId, input);


    /// <summary>
    /// 删除容器配置
    /// </summary>
    /// <param name="applicationDeploymentApplication"></param>
    /// <param name="id"></param>
    /// <param name="applicationContainerId"></param>
    /// <returns></returns>
    [HttpDelete("{id}/{applicationContainerId}/container")]
    public Task DeleteApplicationContainer([FromServices] IDeploymentConfigurationApplication applicationDeploymentApplication, string id, string applicationContainerId)
        => applicationDeploymentApplication.DeleteDeploymentContainerAsync(id, applicationContainerId);


    /// <summary>
    /// 根据ApplicationDeploymentId查询一组部署容器配置
    /// </summary>
    /// <param name="applicationContainerQueryService"></param>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    [HttpGet("{applicationDeploymentId}/container/list")]
    public Task<List<DeploymentContainerConfigurationOutputDto>> GetApplicationContainerList([FromServices] IDeploymentContainerConfigurationQueryService applicationContainerQueryService, string applicationDeploymentId)
        => applicationContainerQueryService.GetListByApplicationDeploymentIdAsync(applicationDeploymentId);

    #endregion
}