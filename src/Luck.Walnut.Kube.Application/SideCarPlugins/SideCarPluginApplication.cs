using Luck.Framework.Exceptions;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.SideCarPlugins;
using Luck.Framework.UnitOfWorks;
using Luck.Walnut.Kube.Domain.AggregateRoots.SideCarPlugins;

namespace Luck.Walnut.Kube.Application.SideCarPlugins
{
    public class SideCarPluginApplication : ISideCarPluginApplication
    {
        private readonly ISideCarPluginRepository _initContainerConfigurationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SideCarPluginApplication(ISideCarPluginRepository initContainerConfigurationRepository, IUnitOfWork unitOfWork)
        {
            _initContainerConfigurationRepository = initContainerConfigurationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateSideCarPluginAsync(SideCarPluginInputDto input)
        {
            if (await CheckIsExitInitContainerConfigurationAsync(input.ContainerName))
            {
                throw new BusinessException($"[{input.ContainerName}]已存在,请刷新界面");
            }

            var initContainer = new SideCarPlugin(input.ContainerName, input.Image, input.RestartPolicy, input.ImagePullPolicy);
            initContainer.SetEnvironments(input.Environments)
                .SetReadinessProbe(input.ReadinessProbe)
                .SetLiveNessProbe(input.LiveNessProbe)
                .SetLimits(input.Limits)
                .SetRequests(input.Requests)
                .SetContainerPortConfigurations(input.ContainerPortConfigurations);
            _initContainerConfigurationRepository.Add(initContainer);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <exception cref="BusinessException"></exception>
        public async Task UpdateSideCarPluginAsync(string id, SideCarPluginInputDto input)
        {
            var initContainer = await CheckAndGetInitContainerConfigurationAsync(id);
            initContainer.Update(input)
                .SetEnvironments(input.Environments)
                .SetReadinessProbe(input.ReadinessProbe)
                .SetLiveNessProbe(input.LiveNessProbe)
                .SetLimits(input.Limits)
                .SetRequests(input.Requests)
                .SetContainerPortConfigurations(input.ContainerPortConfigurations);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// 删除容器
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteSideCarPluginAsync(string id)
        {
            var initContainer = await CheckAndGetInitContainerConfigurationAsync(id);
            _initContainerConfigurationRepository.Remove(initContainer);
            await _unitOfWork.CommitAsync();
        }


        private async Task<bool> CheckIsExitInitContainerConfigurationAsync(string name)
        {
            var container = await _initContainerConfigurationRepository.FindSideCarPluginByNameAsync(name);
            return container is not null;
        }

        private async Task<SideCarPlugin> CheckAndGetInitContainerConfigurationAsync(string id)
        {
            var initContainer = await _initContainerConfigurationRepository.FindSideCarPluginByIdAsync(id);

            if (initContainer is null)
            {
                throw new BusinessException("初始容器不存在，请刷新页面");
            }

            return initContainer;
        }
    }
}