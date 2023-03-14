using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Query.DeploymentConfigurations;

public class MasterContainerConfigurationQueryService : IMasterContainerConfigurationQueryService
{
    private readonly IMasterContainerConfigurationRepository _applicationContainerRepository;

    public MasterContainerConfigurationQueryService(IMasterContainerConfigurationRepository applicationContainerRepository)
    {
        _applicationContainerRepository = applicationContainerRepository;
    }

    public async Task<List<MasterContainerConfigurationOutputDto>> GetDeploymentContainerConfigurationListByDeploymentIdAsync(string applicationDeploymentId)
    {
        var applicationContainerList = await _applicationContainerRepository.GetListByApplicationDeploymentIdAsync(applicationDeploymentId);
        return applicationContainerList.Select(x =>
            {
                var applicationContainerOutputDto = new MasterContainerConfigurationOutputDto
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

    public async Task<MasterContainerConfigurationOutputDto?> GetApplicationContainerByIdFirstOrDefaultAsync(string id)
    {
        var deploymentContainerConfiguration = await _applicationContainerRepository.FindApplicationContainerByIdFirstOrDefaultAsync(id);
        if (deploymentContainerConfiguration is null)
        {
            return null;
        }

        var deploymentContainerConfigurationDto = new MasterContainerConfigurationOutputDto
        {
            Id = deploymentContainerConfiguration.Id,
            ContainerName = deploymentContainerConfiguration.ContainerName,
            RestartPolicy = deploymentContainerConfiguration.RestartPolicy,
            IsInitContainer = deploymentContainerConfiguration.IsInitContainer,
            ImagePullPolicy = deploymentContainerConfiguration.ImagePullPolicy,
            Image = deploymentContainerConfiguration.Image,
        };

        if (deploymentContainerConfiguration.ReadinessProbe is not null)
        {
            deploymentContainerConfigurationDto.ReadinessProbe = new ContainerSurviveConfigurationDto
            {
                Scheme = deploymentContainerConfiguration.ReadinessProbe.Scheme,
                Path = deploymentContainerConfiguration.ReadinessProbe.Path,
                Port = deploymentContainerConfiguration.ReadinessProbe.Port,
                InitialDelaySeconds = deploymentContainerConfiguration.ReadinessProbe.InitialDelaySeconds,
                PeriodSeconds = deploymentContainerConfiguration.ReadinessProbe.PeriodSeconds,
            };
        }

        if (deploymentContainerConfiguration.LiveNessProbe is not null)
        {
            deploymentContainerConfigurationDto.LiveNessProbe = new ContainerSurviveConfigurationDto
            {
                Scheme = deploymentContainerConfiguration.LiveNessProbe.Scheme,
                Path = deploymentContainerConfiguration.LiveNessProbe.Path,
                Port = deploymentContainerConfiguration.LiveNessProbe.Port,
                InitialDelaySeconds = deploymentContainerConfiguration.LiveNessProbe.InitialDelaySeconds,
                PeriodSeconds = deploymentContainerConfiguration.LiveNessProbe.PeriodSeconds,
            };
        }

        if (deploymentContainerConfiguration.Limits is not null)
        {
            deploymentContainerConfigurationDto.Limits = new ContainerResourceQuantityDto
            {
                Cpu = deploymentContainerConfiguration.Limits.Cpu,
                Memory = deploymentContainerConfiguration.Limits.Memory,
            };
        }

        if (deploymentContainerConfiguration.Requests is not null)
        {
            deploymentContainerConfigurationDto.Requests = new ContainerResourceQuantityDto
            {
                Cpu = deploymentContainerConfiguration.Requests.Cpu,
                Memory = deploymentContainerConfiguration.Requests.Memory,
            };
        }

        deploymentContainerConfigurationDto.Environments = deploymentContainerConfiguration.Environments;


        return deploymentContainerConfigurationDto;
    }
}