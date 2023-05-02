using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.SideCarPlugins;

namespace Luck.Walnut.Kube.Query.SideCarPlugins
{
    public interface ISideCarPluginQueryService:IScopedDependency
    {
        /// <summary>
        /// 根据Id获取一个容器配置 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SideCarPluginOutputDto?> GetSideCarPluginAsync(string id);

        /// <summary>
        /// 获取初始容器列表
        /// </summary>
        Task<List<SideCarPluginOutputDto>> GetSideCarPluginListAsync();
        
        /// <summary>
        /// 获取初始容器列表
        /// </summary>
        Task<PageBaseResult<SideCarPluginOutputDto>> GetSideCarPluginPageListAsync(SideCarPluginQueryDto query);
    }
}