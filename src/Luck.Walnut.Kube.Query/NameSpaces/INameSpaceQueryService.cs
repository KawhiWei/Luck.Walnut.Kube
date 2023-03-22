using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.NameSpaces;

namespace Luck.Walnut.Kube.Query.NameSpaces;

public interface INameSpaceQueryService:IScopedDependency
{
    /// <summary>
    /// 分页获取命名空间列表
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<PageBaseResult<NameSpaceOutputDto>> GetNameSpacePageListAsync(NameSpaceQueryDto query);
    
    /// <summary>
    /// 根据Id查询一个明明空间详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<NameSpaceOutputDto?> GetNameSpaceDetailByIdAsync(string id);


    /// <summary>
    /// 根据集群Id获取NameSpace列表
    /// </summary>
    /// <param name="clusterId"></param>
    /// <returns></returns>
    Task<List<NameSpaceOutputDto>> GetNameSpaceByClusterIdListAsync(string clusterId);
}