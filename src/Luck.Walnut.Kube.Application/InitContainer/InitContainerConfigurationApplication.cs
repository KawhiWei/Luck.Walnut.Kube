using Luck.Framework.Exceptions;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.InitContainerConfiguration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luck.Walnut.Kube.Application.InitContainer
{
    public class InitContainerConfigurationApplication : IInitContainerConfigurationApplication
    {
        private readonly IInitContainerConfigurationRepository _initContainerConfigurationRepository;

        public InitContainerConfigurationApplication(IInitContainerConfigurationRepository initContainerConfigurationRepository)
        {
            _initContainerConfigurationRepository = initContainerConfigurationRepository;
        }

        public async Task CreateInitContainerConfigurationAsync(InitContainerConfigurationInputDto input)
        {
            if (await CheckIsExitInitContainerConfigurationAsync(input.ContainerName)) throw new BusinessException($"[{input.ContainerName}]已存在,请刷新界面");

            var initContainer = new InitContainerConfiguration(input.ContainerName, input.IsInitContainer, input.Image, input.RestartPolicy, input.ImagePullPolicy);
            //if (input.ReadinessProbe is not null)
            //{
            //    initContainer.ReadinessProbe = new ContainerSurviveConfiguration(input.ReadinessProbe?.Scheme, input.ReadinessProbe?.Path, input.ReadinessProbe?.Port, input.ReadinessProbe?.InitialDelaySeconds, input.ReadinessProbe?.PeriodSeconds);
            //}
            


            //initContainer
        }


        private async Task<bool> CheckIsExitInitContainerConfigurationAsync(string name)
        {
            var container = await _initContainerConfigurationRepository.FindInitContainerConfigurationByNameAsunc(name);
            if (container == null) return false;
            return true;
        }


    }
}
