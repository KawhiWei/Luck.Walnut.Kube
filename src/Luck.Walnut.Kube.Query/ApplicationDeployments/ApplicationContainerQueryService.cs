using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Query.ApplicationDeployments;

public class ApplicationContainerQueryService : IApplicationContainerQueryService
{
    private readonly IApplicationContainerRepository _applicationContainerRepository;

    public ApplicationContainerQueryService(IApplicationContainerRepository applicationContainerRepository)
    {
        _applicationContainerRepository = applicationContainerRepository;
    }

    public async Task<List<ApplicationContainerOutputDto>> GetListByApplicationDeploymentIdAsync(string applicationDeploymentId)
    {
        var applicationContainerList = await _applicationContainerRepository.GetListByApplicationDeploymentIdAsync(applicationDeploymentId);
        return applicationContainerList.Select(x =>
            {
                var applicationContainerOutputDto = new ApplicationContainerOutputDto
                {
                    Id = x.Id,
                    ContainerName = x.ContainerName,
                    RestartPolicy = x.RestartPolicy,
                    IsInitContainer = x.IsInitContainer,
                    ImagePullPolicy = x.ImagePullPolicy,
                    Environments = x.Environments,
                };
                if (x.ReadinessProbe is not null)
                {
                    applicationContainerOutputDto.ReadinessProbe = new ContainerSurviveConfigurationDto()
                    {
                        Scheme = x.ReadinessProbe.Scheme,
                        Path = x.ReadinessProbe.Path,
                        Port = x.ReadinessProbe.Port,
                        InitialDelaySeconds = x.ReadinessProbe.InitialDelaySeconds,
                        PeriodSeconds = x.ReadinessProbe.PeriodSeconds,
                    };
                }

                if (x.LiveNessProbe is not null)
                {
                    applicationContainerOutputDto.LiveNessProbe = new ContainerSurviveConfigurationDto()
                    {
                        Scheme = x.LiveNessProbe.Scheme,
                        Path = x.LiveNessProbe.Path,
                        Port = x.LiveNessProbe.Port,
                        InitialDelaySeconds = x.LiveNessProbe.InitialDelaySeconds,
                        PeriodSeconds = x.LiveNessProbe.PeriodSeconds,
                    };
                }

                if (x.Limits is not null)
                {
                    applicationContainerOutputDto.Limits = new ContainerResourceQuantityDto();
                    if (x.Limits is not null)
                    {
                        applicationContainerOutputDto.Limits.Cpu = x.Limits.Cpu;
                        applicationContainerOutputDto.Limits.Memory = x.Limits.Memory;
                    }
                }

                if (x.Requests is not null)
                {
                    applicationContainerOutputDto.Requests = new ContainerResourceQuantityDto();
                    if (x.Requests is not null)
                    {
                        applicationContainerOutputDto.Requests.Cpu = x.Requests.Cpu;
                        applicationContainerOutputDto.Requests.Memory = x.Requests.Memory;
                    }
                }

                applicationContainerOutputDto.ContainerPortConfigurations = new List<ContainerPortConfigurationDto>();
                applicationContainerOutputDto.ContainerPortConfigurations.AddRange(
                    x.ContainerPortConfigurations.Select(s => new ContainerPortConfigurationDto()
                    {
                        Name = s.Name,
                        ContainerPort = s.ContainerPort,
                        Protocol = s.Protocol,
                    })
                );


                return applicationContainerOutputDto;
            }
        ).ToList();
    }
}