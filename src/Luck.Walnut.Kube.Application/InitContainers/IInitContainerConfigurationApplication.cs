using Luck.Walnut.Kube.Dto.InitContainerConfigurations;

namespace Luck.Walnut.Kube.Application.InitContainers
{
    public interface IInitContainerConfigurationApplication:IScopedDependency
    {
        /// <summary>
        /// 创建默认容器 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateInitContainerConfigurationAsync(InitContainerConfigurationInputDto input);

        /// <summary>
        /// 修改容器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        Task UpdateInitContainerConfigurationAsync(string id, InitContainerConfigurationInputDto input);

        /// <summary>
        /// 删除容器
        /// </summary>
        /// <param name="id"></param>
        Task DeleteInitContainerConfigurationAsync(string id);
    }
}
