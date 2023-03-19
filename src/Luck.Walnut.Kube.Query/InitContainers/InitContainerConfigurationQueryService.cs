using Luck.Framework.Exceptions;
using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.ContainerDtoBases;
using Luck.Walnut.Kube.Dto.InitContainerConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Luck.Walnut.Kube.Query.InitContainers
{
    public class InitContainerConfigurationQueryService : IInitContainerConfigurationQueryService
    {
        private readonly IInitContainerConfigurationRepository _initContainerConfigurationRepository;

        public InitContainerConfigurationQueryService(IInitContainerConfigurationRepository initContainerConfigurationRepository)
        {
            _initContainerConfigurationRepository = initContainerConfigurationRepository;
        }

        public async Task<InitContainerConfigurationOutputDto?> GetInitContainerConfigurationAsync(string id)
        {
            var initContainer = await _initContainerConfigurationRepository.FindInitContainerConfigurationByIdAsync(id);
            return initContainer is null ? null : CreateInitContainerConfigurationOutputDto(initContainer);
        }

        public async Task<List<InitContainerConfigurationOutputDto>> GetInitContainerConfigurationListAsync()
        {
            var initContainerList = await _initContainerConfigurationRepository.FindAll().ToListAsync();
            return initContainerList.Select(CreateInitContainerConfigurationOutputDto).ToList();
        }

        public async Task<PageBaseResult<InitContainerConfigurationOutputDto>> GetInitContainerConfigurationPageListAsync(InitContainerConfigurationQueryDto query)
        {
            var (data, totalCount) = await _initContainerConfigurationRepository.GetInitContainerConfigurationPageListAsync(query);

            var initContainerConfigurationOutputDtoList = data.Select(CreateInitContainerConfigurationOutputDto).ToArray();


            return new PageBaseResult<InitContainerConfigurationOutputDto>(totalCount, initContainerConfigurationOutputDtoList);
        }

        private InitContainerConfigurationOutputDto CreateInitContainerConfigurationOutputDto(InitContainerConfiguration initContainer)
        {
            var initContainerDto = new InitContainerConfigurationOutputDto
            {
                Id = initContainer.Id,
                ContainerName = initContainer.ContainerName,
                RestartPolicy = initContainer.RestartPolicy,
                IsInitContainer = initContainer.IsInitContainer,
                ImagePullPolicy = initContainer.ImagePullPolicy,
                Image = initContainer.Image,
            };

            if (initContainer.ReadinessProbe is not null)
            {
                initContainerDto.ReadinessProbe = new ContainerSurviveConfigurationDto
                {
                    Scheme = initContainer.ReadinessProbe.Scheme,
                    Path = initContainer.ReadinessProbe.Path,
                    Port = initContainer.ReadinessProbe.Port,
                    InitialDelaySeconds = initContainer.ReadinessProbe.InitialDelaySeconds,
                    PeriodSeconds = initContainer.ReadinessProbe.PeriodSeconds,
                };
            }

            if (initContainer.LiveNessProbe is not null)
            {
                initContainerDto.LiveNessProbe = new ContainerSurviveConfigurationDto
                {
                    Scheme = initContainer.LiveNessProbe.Scheme,
                    Path = initContainer.LiveNessProbe.Path,
                    Port = initContainer.LiveNessProbe.Port,
                    InitialDelaySeconds = initContainer.LiveNessProbe.InitialDelaySeconds,
                    PeriodSeconds = initContainer.LiveNessProbe.PeriodSeconds,
                };
            }

            if (initContainer.Limits is not null)
            {
                initContainerDto.Limits = new ContainerResourceQuantityDto
                {
                    Cpu = initContainer.Limits.Cpu,
                    Memory = initContainer.Limits.Memory,
                };
            }

            if (initContainer.Requests is not null)
            {
                initContainerDto.Requests = new ContainerResourceQuantityDto
                {
                    Cpu = initContainer.Requests.Cpu,
                    Memory = initContainer.Requests.Memory,
                };
            }

            initContainerDto.Environments = initContainer.Environments;
            return initContainerDto;
        }
    }
}