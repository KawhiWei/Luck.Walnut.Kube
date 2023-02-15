using Luck.Walnut.Kube.Application.Clusters;
using Luck.Walnut.Kube.Application.NameSpaces;
using Luck.Walnut.Kube.Dto.Clusteries;
using Luck.Walnut.Kube.Dto.NameSpaces;
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
    /// 删除命名空间
    /// </summary>
    /// <param name="nameSpaceApplication"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public Task DeleteNameSpace([FromServices] INameSpaceApplication nameSpaceApplication, string id)
        => nameSpaceApplication.DeleteNameSpaceAsync(id);
}