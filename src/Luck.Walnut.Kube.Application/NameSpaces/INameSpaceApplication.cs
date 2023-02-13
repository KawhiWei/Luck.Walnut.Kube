using Luck.Walnut.Kube.Dto.NameSpaces;

namespace Luck.Walnut.Kube.Application.NameSpaces;

public interface INameSpaceApplication : IScopedDependency
{
    /// <summary>
    /// 创建名称空间
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateNameSpaceAsync(NameSpaceInputDto input);

    /// <summary>
    /// 修改名称空间
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateNameSpaceAsync(string id, NameSpaceInputDto input);

    /// <summary>
    /// 发布名称空间
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task PublishNameSpaceAsync(string id);

    /// <summary>
    /// 删除名称空间
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteNameSpaceAsync(string id);
}