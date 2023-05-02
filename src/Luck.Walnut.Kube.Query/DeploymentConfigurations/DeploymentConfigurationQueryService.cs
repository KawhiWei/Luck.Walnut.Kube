using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.ContainerDtoBases;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Query.DeploymentConfigurations;

public class DeploymentConfigurationQueryService : IDeploymentConfigurationQueryService
{
    private readonly IDeploymentConfigurationRepository _deploymentConfigurationRepository;
    private readonly IMasterContainerConfigurationRepository _masterContainerConfigurationRepository;
    private readonly INameSpaceRepository _nameSpaceRepository;
    private readonly IClusterRepository _clusterRepository;

    public DeploymentConfigurationQueryService(IDeploymentConfigurationRepository applicationDeploymentRepository, IMasterContainerConfigurationRepository masterContainerConfigurationRepository, INameSpaceRepository nameSpaceRepository, IClusterRepository clusterRepository)
    {
        _deploymentConfigurationRepository = applicationDeploymentRepository;
        _masterContainerConfigurationRepository = masterContainerConfigurationRepository;
        _nameSpaceRepository = nameSpaceRepository;
        _clusterRepository = clusterRepository;
    }


    public async Task<PageBaseResult<DeploymentConfigurationOutputDto>> GetDeploymentConfigurationPageListAsync(string appId, DeploymentConfigurationQueryDto query)
    {
        var (data, totalCount) = await _deploymentConfigurationRepository.GetDeploymentConfigurationPageListAsync(appId, query);
        var masterContainerList = await _masterContainerConfigurationRepository.GetListByDeploymentIdsAsync(data.Select(x => x.Id).ToList());
        var nameSpaceList = await _nameSpaceRepository.GetNameSpaceByIdsListAsync(data.Select(x => x.NameSpaceId).ToList());
        var clusterList = await _clusterRepository.GetClusterFindByIdListAsync(data.Select(x => x.ClusterId).ToList());

        var result = data.Select(deploymentConfiguration =>
        {
            var deploymentConfigurationOutputDto = CreateDeploymentConfigurationOutputDto(deploymentConfiguration);
            var masterContainer = masterContainerList.FirstOrDefault(x => x.DeploymentId == deploymentConfigurationOutputDto.Id);
            if (masterContainer is not null)
            {
                deploymentConfigurationOutputDto.MasterContainerId = masterContainer.Id;
            }

            var nameSpace = nameSpaceList.FirstOrDefault(x => x.Id == deploymentConfigurationOutputDto.NameSpaceId);
            if (nameSpace is not null)
            {
                deploymentConfigurationOutputDto.NameSpaceName = nameSpace.Name;
            }

            var cluster = clusterList.FirstOrDefault(cluster => cluster.Id == deploymentConfigurationOutputDto.ClusterId);
            if (cluster is not null)
            {
                deploymentConfigurationOutputDto.ClusterName = cluster.Name;
            }

            return deploymentConfigurationOutputDto;
        }).ToArray();

        return new PageBaseResult<DeploymentConfigurationOutputDto>(totalCount, result);
    }

    public async Task<DeploymentOutputDto?> GetDeploymentConfigurationDetailByIdAsync(string deploymentId, string masterContainerId)
    {
        var deploymentConfiguration = await _deploymentConfigurationRepository.FindDeploymentConfigurationByIdAsync(deploymentId);
        if (deploymentConfiguration is null)

        {
            return null;
        }

        var masterContainerConfigurations = deploymentConfiguration.MasterContainers.Where(x => x.Id == masterContainerId).Select(x =>
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


        return new DeploymentOutputDto()
        {
            DeploymentConfiguration = CreateDeploymentConfigurationOutputDto(deploymentConfiguration),
            MasterContainerConfiguration = masterContainerConfigurations,
        };
    }


    public async Task<List<DeploymentConfigurationOutputDto>> GetDeploymentConfigurationByAppIdListAsync(string appId)
    {
        var deploymentConfigurationList = await _deploymentConfigurationRepository.GetDeploymentConfigurationByAppIdListAsync(appId);

        return deploymentConfigurationList.Select(CreateDeploymentConfigurationOutputDto).ToList();
    }

    private DeploymentConfigurationOutputDto CreateDeploymentConfigurationOutputDto(DeploymentConfiguration deploymentConfiguration)
    {
        var deploymentConfigurationOutputDto = new DeploymentConfigurationOutputDto
        {
            Id = deploymentConfiguration.Id,
            EnvironmentName = deploymentConfiguration.EnvironmentName,
            ApplicationRuntimeType = deploymentConfiguration.ApplicationRuntimeType,
            DeploymentType = deploymentConfiguration.DeploymentType,
            ChineseName = deploymentConfiguration.ChineseName,
            Name = deploymentConfiguration.Name,
            AppId = deploymentConfiguration.AppId,
            NameSpaceId = deploymentConfiguration.NameSpaceId,
            Replicas = deploymentConfiguration.Replicas,
            ImagePullSecretId = deploymentConfiguration.ImagePullSecretId,
            ClusterId = deploymentConfiguration.ClusterId,
            InitContainers = deploymentConfiguration.InitContainers.ToList()
        };

        return deploymentConfigurationOutputDto;
    }
}