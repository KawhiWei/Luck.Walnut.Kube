using Luck.Framework.Exceptions;
using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.InitContainerConfigurations;
using Luck.Framework.UnitOfWorks;

namespace Luck.Walnut.Kube.Application.InitContainers
{
    public class InitContainerConfigurationApplication : IInitContainerConfigurationApplication
    {
        private readonly IInitContainerConfigurationRepository _initContainerConfigurationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InitContainerConfigurationApplication(IInitContainerConfigurationRepository initContainerConfigurationRepository, IUnitOfWork unitOfWork)
        {
            _initContainerConfigurationRepository = initContainerConfigurationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateInitContainerConfigurationAsync(InitContainerConfigurationInputDto input)
        {
            if (await CheckIsExitInitContainerConfigurationAsync(input.ContainerName))
            {
                throw new BusinessException($"[{input.ContainerName}]已存在,请刷新界面");
            }

            var initContainer = new InitContainerConfiguration(input.ContainerName, input.IsInitContainer, input.Image, input.RestartPolicy, input.ImagePullPolicy);
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
        public async Task UpdateInitContainerConfigurationAsync(string id, InitContainerConfigurationInputDto input)
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
        public async Task DeleteInitContainerConfigurationAsync(string id)
        {
            var initContainer = await CheckAndGetInitContainerConfigurationAsync(id);
            _initContainerConfigurationRepository.Remove(initContainer);
            await _unitOfWork.CommitAsync();
        }


        private async Task<bool> CheckIsExitInitContainerConfigurationAsync(string name)
        {
            var container = await _initContainerConfigurationRepository.FindInitContainerConfigurationByNameAsync(name);
            return container is not null;
        }

        private async Task<InitContainerConfiguration> CheckAndGetInitContainerConfigurationAsync(string id)
        {
            var initContainer = await _initContainerConfigurationRepository.FindInitContainerConfigurationByIdAsync(id);

            if (initContainer is null)
            {
                throw new BusinessException("初始容器不存在，请刷新页面");
            }

            return initContainer;
        }
    }
}