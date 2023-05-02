using Luck.Walnut.Kube.Dto.SideCarPlugins;

namespace Luck.Walnut.Kube.Application.SideCarPlugins
{
    public interface ISideCarPluginApplication:IScopedDependency
    {
        /// <summary>
        /// 创建默认容器 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateSideCarPluginAsync(SideCarPluginInputDto input);

        /// <summary>
        /// 修改容器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        Task UpdateSideCarPluginAsync(string id, SideCarPluginInputDto input);

        /// <summary>
        /// 删除容器
        /// </summary>
        /// <param name="id"></param>
        Task DeleteSideCarPluginAsync(string id);
    }
}
