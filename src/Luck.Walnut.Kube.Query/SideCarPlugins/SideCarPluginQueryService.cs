using Luck.Walnut.Kube.Domain.AggregateRoots.SideCarPlugins;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.ContainerDtoBases;
using Luck.Walnut.Kube.Dto.SideCarPlugins;
using Microsoft.EntityFrameworkCore;

namespace Luck.Walnut.Kube.Query.SideCarPlugins
{
    public class SideCarPluginQueryService : ISideCarPluginQueryService
    {
        private readonly ISideCarPluginRepository _sideCarPluginRepository;

        public SideCarPluginQueryService(ISideCarPluginRepository sideCarPluginRepository)
        {
            _sideCarPluginRepository = sideCarPluginRepository;
        }

        public async Task<SideCarPluginOutputDto?> GetSideCarPluginAsync(string id)
        {
            var sideCarPlugin = await _sideCarPluginRepository.FindSideCarPluginByIdAsync(id);
            return sideCarPlugin is null ? null : SideCarPluginOutputDto(sideCarPlugin);
        }

        public async Task<List<SideCarPluginOutputDto>> GetSideCarPluginListAsync()
        {
            var sideCarPluginList = await _sideCarPluginRepository.FindAll().ToListAsync();
            return sideCarPluginList.Select(SideCarPluginOutputDto).ToList();
        }

        public async Task<PageBaseResult<SideCarPluginOutputDto>> GetSideCarPluginPageListAsync(SideCarPluginQueryDto query)
        {
            var (data, totalCount) = await _sideCarPluginRepository.GetInitContainerConfigurationPageListAsync(query);

            var sideCarPluginOutputDtoList = data.Select(SideCarPluginOutputDto).ToArray();


            return new PageBaseResult<SideCarPluginOutputDto>(totalCount, sideCarPluginOutputDtoList);
        }

        private SideCarPluginOutputDto SideCarPluginOutputDto(SideCarPlugin sideCarPlugin)
        {
            var sideCarPluginDto = new SideCarPluginOutputDto
            {
                Id = sideCarPlugin.Id,
                ContainerName = sideCarPlugin.ContainerName,
                RestartPolicy = sideCarPlugin.RestartPolicy,
                ImagePullPolicy = sideCarPlugin.ImagePullPolicy,
                Image = sideCarPlugin.Image,
            };

            if (sideCarPlugin.ReadinessProbe is not null)
            {
                sideCarPluginDto.ReadinessProbe = new ContainerSurviveConfigurationDto
                {
                    Scheme = sideCarPlugin.ReadinessProbe.Scheme,
                    Path = sideCarPlugin.ReadinessProbe.Path,
                    Port = sideCarPlugin.ReadinessProbe.Port,
                    InitialDelaySeconds = sideCarPlugin.ReadinessProbe.InitialDelaySeconds,
                    PeriodSeconds = sideCarPlugin.ReadinessProbe.PeriodSeconds,
                };
            }

            if (sideCarPlugin.LiveNessProbe is not null)
            {
                sideCarPluginDto.LiveNessProbe = new ContainerSurviveConfigurationDto
                {
                    Scheme = sideCarPlugin.LiveNessProbe.Scheme,
                    Path = sideCarPlugin.LiveNessProbe.Path,
                    Port = sideCarPlugin.LiveNessProbe.Port,
                    InitialDelaySeconds = sideCarPlugin.LiveNessProbe.InitialDelaySeconds,
                    PeriodSeconds = sideCarPlugin.LiveNessProbe.PeriodSeconds,
                };
            }

            if (sideCarPlugin.Limits is not null)
            {
                sideCarPluginDto.Limits = new ContainerResourceQuantityDto
                {
                    Cpu = sideCarPlugin.Limits.Cpu,
                    Memory = sideCarPlugin.Limits.Memory,
                };
            }

            if (sideCarPlugin.Requests is not null)
            {
                sideCarPluginDto.Requests = new ContainerResourceQuantityDto
                {
                    Cpu = sideCarPlugin.Requests.Cpu,
                    Memory = sideCarPlugin.Requests.Memory,
                };
            }

            sideCarPluginDto.Environments = sideCarPlugin.Environments;
            return sideCarPluginDto;
        }
    }
}