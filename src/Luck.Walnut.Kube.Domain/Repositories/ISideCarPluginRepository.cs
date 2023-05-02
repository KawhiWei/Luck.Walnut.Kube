using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.SideCarPlugins;
using Luck.Walnut.Kube.Dto.SideCarPlugins;

namespace Luck.Walnut.Kube.Domain.Repositories
{
    public interface ISideCarPluginRepository : IAggregateRootRepository<SideCarPlugin, string>, IScopedDependency
    {

        /// <summary>
        /// 分页查询默认容器列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<(SideCarPlugin[] Data, int TotalCount)> GetInitContainerConfigurationPageListAsync(SideCarPluginQueryDto query);
        
        /// <summary>
        /// 根据Name获取一个默认容器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<SideCarPlugin?> FindSideCarPluginByNameAsync(string name);


        /// <summary>
        /// 根据Id查询一个默认容器配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SideCarPlugin?> FindSideCarPluginByIdAsync(string id);
    }
}