using Luck.Walnut.Kube.Dto.Services;

namespace Luck.Walnut.Kube.Application.Services;

public interface IServiceApplication: IScopedDependency
{
    
    /// <summary>
    /// 添加服务配置
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateServiceAsync(ServiceInputDto input);

    /// <summary>
    /// 修改服务配置
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateServiceAsync(string id,ServiceInputDto input);
    
    /// <summary>
    /// 删除服务配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteServiceAsync(string id);
    
    /// <summary>
    /// 发布服务配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task PublishServiceAsync(string id);
    
    /// <summary>
    /// 发布服务配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task OffPublishServiceAsync(string id);
}