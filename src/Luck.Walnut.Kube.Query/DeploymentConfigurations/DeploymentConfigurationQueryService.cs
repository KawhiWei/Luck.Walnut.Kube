using IdentityModel.OidcClient;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;
using Microsoft.Extensions.Hosting;

namespace Luck.Walnut.Kube.Query.DeploymentConfigurations;

public class DeploymentConfigurationQueryService : IDeploymentConfigurationQueryService
{
    private readonly IDeploymentConfigurationRepository _deploymentConfigurationRepository;
    private readonly IMasterContainerConfigurationRepository _masterContainerConfigurationRepository;

    public DeploymentConfigurationQueryService(IDeploymentConfigurationRepository applicationDeploymentRepository, IMasterContainerConfigurationRepository masterContainerConfigurationRepository)
    {
        _deploymentConfigurationRepository = applicationDeploymentRepository;
        _masterContainerConfigurationRepository = masterContainerConfigurationRepository;
    }


    public async Task<PageBaseResult<DeploymentConfigurationOutputDto>> GetDeploymentConfigurationPageListAsync(string appId, DeploymentConfigurationQueryDto query)
    {
        var (data, totalCount) = await _deploymentConfigurationRepository.GetDeploymentConfigurationPageListAsync(appId, query);
        var masterContainerList = await _masterContainerConfigurationRepository.GetListByDeploymentIdsAsync(data.Select(x => x.Id).ToList());
        foreach (var item in data)
        {
            var masterContainer = masterContainerList.FirstOrDefault(x => x.DeploymentId == item.Id);
            if (masterContainer is not null)
            {
                item.MasterContainerId = masterContainer.Id;
            }
        }

        return new PageBaseResult<DeploymentConfigurationOutputDto>(totalCount, data);
    }

    public async Task<DeploymentOutputDto?> GetDeploymentConfigurationDetailByIdAsync(string deploymentId, string masterContainerId)
    {
        var deploymentConfiguration = await _deploymentConfigurationRepository.FindDeploymentConfigurationByIdAsync(deploymentId);
        if (deploymentConfiguration is null)

        {
            return null;
        }

        var masterContainerConfigurations = deploymentConfiguration.MasterContainers.Where(x=>x.Id==masterContainerId).Select(x =>
        {
            var deploymentContainerConfiguration = new MasterContainerConfigurationOutputDto { Id = x.Id, ContainerName = x.ContainerName, RestartPolicy = x.RestartPolicy, IsInitContainer = x.IsInitContainer, ImagePullPolicy = x.ImagePullPolicy, Image = x.Image, };
            if (x.ReadinessProbe is not null)
            {
                deploymentContainerConfiguration.ReadinessProbe = new ContainerSurviveConfigurationDto
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
                deploymentContainerConfiguration.LiveNessProbe = new ContainerSurviveConfigurationDto
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
                deploymentContainerConfiguration.Limits = new ContainerResourceQuantityDto
                {
                    Cpu = x.Limits.Cpu,
                    Memory = x.Limits.Memory,
                };
            }

            if (x.Requests is not null)
            {
                deploymentContainerConfiguration.Requests = new ContainerResourceQuantityDto
                {
                    Cpu = x.Requests.Cpu,
                    Memory = x.Requests.Memory,
                };
            }

            deploymentContainerConfiguration.Environments = x.Environments;

            return deploymentContainerConfiguration;
        }).FirstOrDefault();

        var deploymentConfigurationOutputDto = new DeploymentConfigurationOutputDto
        {
            Id = deploymentConfiguration.Id,
            EnvironmentName = deploymentConfiguration.EnvironmentName,
            ApplicationRuntimeType = deploymentConfiguration.ApplicationRuntimeType,
            DeploymentType = deploymentConfiguration.DeploymentType,
            ChineseName = deploymentConfiguration.ChineseName,
            Name = deploymentConfiguration.Name,
            AppId = deploymentConfiguration.AppId,
            KubernetesNameSpaceId = deploymentConfiguration.KubernetesNameSpaceId,
            Replicas = deploymentConfiguration.Replicas,
            MaxUnavailable = deploymentConfiguration.MaxUnavailable,
            ImagePullSecretId = deploymentConfiguration.ImagePullSecretId,
        };

        return new DeploymentOutputDto()
        {
            DeploymentConfiguration = deploymentConfigurationOutputDto,
            MasterContainerConfiguration = masterContainerConfigurations,
        };
    }
}