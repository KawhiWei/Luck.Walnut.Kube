using IdentityModel.OidcClient;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;
using Microsoft.Extensions.Hosting;

namespace Luck.Walnut.Kube.Query.DeploymentConfigurations;

public class DeploymentConfigurationQueryService : IDeploymentConfigurationQueryService
{

    private readonly IDeploymentConfigurationRepository _deploymentConfigurationRepository;

    public DeploymentConfigurationQueryService(IDeploymentConfigurationRepository applicationDeploymentRepository)
    {
        _deploymentConfigurationRepository = applicationDeploymentRepository;
    }

    public async Task<DeploymentConfigurationOutputDto?> GetApplicationDeploymentDetailByIdAsync(string id)
    {
        var deploymentConfiguration = await _deploymentConfigurationRepository.FindApplicationDeploymentByIdAsync(id);
        if (deploymentConfiguration is null)

        {
            return null;
        }

        var deploymentContainerConfigurations = deploymentConfiguration.DeploymentContainers.Select(x =>
        {

            var deploymentContainerConfiguration = new DeploymentContainerConfigurationOutputDto { Id = x.Id, ContainerName = x.ContainerName, RestartPolicy = x.RestartPolicy, IsInitContainer = x.IsInitContainer, ImagePullPolicy = x.ImagePullPolicy, Image = x.Image, };
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
            if (x.Environments is not null)
            {
                deploymentContainerConfiguration.Environments = x.Environments;
            }
            return deploymentContainerConfiguration;
        }).ToList();

        return new DeploymentConfigurationOutputDto
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
            DeploymentContainerConfigurations = deploymentContainerConfigurations

        };
    }

    public async Task<PageBaseResult<DeploymentConfigurationOutputDto>> GetDeploymentConfigurationPageListAsync(string appId, DeploymentConfigurationQueryDto query)
    {

        var (Data, TotalCount) = await _deploymentConfigurationRepository.GetDeploymentConfigurationPageListAsync(appId, query);
        return new PageBaseResult<DeploymentConfigurationOutputDto>(TotalCount, Data);
    }


}