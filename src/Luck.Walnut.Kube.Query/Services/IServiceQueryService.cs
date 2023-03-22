using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.Services;

namespace Luck.Walnut.Kube.Query.Services;

public interface IServiceQueryService:IScopedDependency
{
    /// <summary>
    /// 分页获取服务
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<PageBaseResult<ServiceOutputDto>> GetServicePageListAsync(ServiceQueryDto query);


    /// <summary>
    /// 根据ID查找一个服务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ServiceOutputDto?> GetServiceByIdAsync(string id);
}