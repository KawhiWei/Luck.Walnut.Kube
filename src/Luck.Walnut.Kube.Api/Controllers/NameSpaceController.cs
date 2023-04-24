using Luck.Walnut.Kube.Application.NameSpaces;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.NameSpaces;
using Luck.Walnut.Kube.Query.NameSpaces;
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
    /// <param name="nameSpaceApplication"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task CreateNameSpace([FromServices] INameSpaceApplication nameSpaceApplication, [FromBody] NameSpaceInputDto input)
        => nameSpaceApplication.CreateNameSpaceAsync(input);


    /// <summary>
    /// 修改集群
    /// </summary>
    /// <param name="nameSpaceApplication"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public Task UpdateNameSpace([FromServices] INameSpaceApplication nameSpaceApplication, string id, [FromBody] NameSpaceInputDto input)
        => nameSpaceApplication.UpdateNameSpaceAsync(id, input);

    /// <summary>
    /// 发布集群到K8S
    /// </summary>
    /// <param name="nameSpaceApplication"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}/publish")]
    public Task PublishNameSpace([FromServices] INameSpaceApplication nameSpaceApplication, string id)
        => nameSpaceApplication.PublishNameSpaceAsync(id);

    /// <summary>
    /// 根据Id获取NameSpace
    /// </summary>
    /// <param name="nameSpaceQueryService"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Task<NameSpaceOutputDto?> GetNameSpaceDetailById([FromServices] INameSpaceQueryService nameSpaceQueryService, string id)
        => nameSpaceQueryService.GetNameSpaceDetailByIdAsync(id);

    /// <summary>
    /// 删除命名空间
    /// </summary>
    /// <param name="nameSpaceApplication"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public Task DeleteNameSpace([FromServices] INameSpaceApplication nameSpaceApplication, string id)
        => nameSpaceApplication.DeleteNameSpaceAsync(id);

    /// <summary>
    /// 分页获取
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<PageBaseResult<NameSpaceOutputDto>> GetNameSpacePageList([FromServices] INameSpaceQueryService nameSpaceQueryService, [FromQuery] NameSpaceQueryDto query)
        => nameSpaceQueryService.GetNameSpacePageListAsync(query);


    /// <summary>
    /// 获取集群列表
    /// </summary>
    /// <param name="nameSpaceQueryService"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public Task<List<NameSpaceOutputDto>> GetNameSpaceIdList([FromServices] INameSpaceQueryService nameSpaceQueryService )
        => nameSpaceQueryService.GetNameSpaceListAsync();
}