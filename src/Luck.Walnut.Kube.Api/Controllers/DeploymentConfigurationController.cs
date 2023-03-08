using Luck.Walnut.Kube.Application.DeploymentConfigurations;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;
using Luck.Walnut.Kube.Query.DeploymentConfigurations;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers;

/// <summary>
/// 应用部署管理
/// </summary>
[Route("api/deployments")]
public class DeploymentConfigurationController : BaseController
{
    #region 部署配置接口

    /// <summary>
    /// 分页查询部署配置
    /// </summary>
    /// <param name="deploymentConfigurationQueryService"></param>
    /// <param name="appId"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{appId}/page/list")]
    public Task<PageBaseResult<DeploymentConfigurationOutputDto>> GetDeploymentConfigurationPageList([FromServices] IDeploymentConfigurationQueryService deploymentConfigurationQueryService, string appId, [FromQuery] DeploymentConfigurationQueryDto query)
        => deploymentConfigurationQueryService.GetDeploymentConfigurationPageListAsync(appId, query);


    /// <summary>
    /// 根据Id查询一个部署配置
    /// </summary>
    /// <param name="deploymentConfigurationQueryService"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Task<DeploymentConfigurationOutputDto?> GetDeploymentConfigurationDetailByIdAsync([FromServices] IDeploymentConfigurationQueryService deploymentConfigurationQueryService, string id)
        => deploymentConfigurationQueryService.GetDeploymentConfigurationDetailByIdAsync(id);

    /// <summary>
    /// 添加部署
    /// </summary>
    /// <param name="deploymentConfigurationApplication"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task CreateDeploymentConfiguration([FromServices] IDeploymentConfigurationApplication deploymentConfigurationApplication, [FromBody] DeploymentConfigurationInputDto input)
        => deploymentConfigurationApplication.CreateDeploymentConfigurationAsync(input);


    /// <summary>
    /// 修改部署
    /// </summary>
    /// <param name="deploymentConfigurationApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public Task UpdateApplicationDeployment([FromServices] IDeploymentConfigurationApplication deploymentConfigurationApplication, string id, [FromBody] DeploymentConfigurationInputDto input)
        => deploymentConfigurationApplication.UpdateDeploymentConfigurationAsync(id, input);

    /// <summary>
    /// 删除部署
    /// </summary>
    /// <param name="deploymentConfigurationApplication"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public Task DeleteApplicationDeployment([FromServices] IDeploymentConfigurationApplication deploymentConfigurationApplication, string id)
        => deploymentConfigurationApplication.DeleteDeploymentConfigurationAsync(id);

    #endregion

    #region K8S部署容器配置接口

    /// <summary>
    /// 添加容器配置
    /// </summary>
    /// <param name="deploymentConfigurationApplication"></param>
    /// <param name="deploymentId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("{deploymentId}/container")]
    public Task CreateDeploymentContainerConfiguration([FromServices] IDeploymentConfigurationApplication deploymentConfigurationApplication, string deploymentId, [FromBody] DeploymentContainerConfigurationInputDto input)
        => deploymentConfigurationApplication.CreateDeploymentContainerConfigurationAsync(deploymentId, input);

    /// <summary>
    /// 修改容器配置
    /// </summary>
    /// <param name="deploymentConfigurationApplication"></param>
    /// <param name="deploymentId"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{deploymentId}/{id}/container")]
    public Task UpdateDeploymentContainerConfiguration([FromServices] IDeploymentConfigurationApplication deploymentConfigurationApplication, string deploymentId, string id, [FromBody] DeploymentContainerConfigurationInputDto input)
        => deploymentConfigurationApplication.UpdateDeploymentContainerConfigurationAsync(deploymentId, id, input);


    /// <summary>
    /// 删除容器配置
    /// </summary>
    /// <param name="deploymentConfigurationApplication"></param>
    /// <param name="deploymentId"></param>
    /// <param name="deploymentContainerId"></param>
    /// <returns></returns>
    [HttpDelete("{deploymentId}/{deploymentContainerId}/container")]
    public Task DeleteDeploymentContainerConfiguration([FromServices] IDeploymentConfigurationApplication deploymentConfigurationApplication, string deploymentId, string deploymentContainerId)
        => deploymentConfigurationApplication.DeleteDeploymentContainerConfigurationAsync(deploymentId, deploymentContainerId);


    /// <summary>
    /// 根据ApplicationDeploymentId查询一组部署容器配置
    /// </summary>
    /// <param name="deploymentContainerConfigurationQueryService"></param>
    /// <param name="deploymentId"></param>
    /// <returns></returns>
    [HttpGet("{deploymentId}/container/list")]
    public Task<List<DeploymentContainerConfigurationOutputDto>> GetDeploymentContainerConfigurationListByDeploymentId([FromServices] IDeploymentContainerConfigurationQueryService deploymentContainerConfigurationQueryService, string deploymentId)
        => deploymentContainerConfigurationQueryService.GetDeploymentContainerConfigurationListByDeploymentIdAsync(deploymentId);

    
    /// <summary>
    /// 根据Id查询一个容器配置
    /// </summary>
    /// <param name="deploymentContainerConfigurationQueryService"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/container")]
    public Task<DeploymentContainerConfigurationOutputDto?> GetDeploymentContainerConfigurationById([FromServices] IDeploymentContainerConfigurationQueryService deploymentContainerConfigurationQueryService, string id)
        => deploymentContainerConfigurationQueryService.GetApplicationContainerByIdFirstOrDefaultAsync(id);
    #endregion
}