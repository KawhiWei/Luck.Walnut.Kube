using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;

public class MasterContainerConfiguration : FullEntity
{
    public MasterContainerConfiguration(string containerName, string restartPolicy, string imagePullPolicy, bool isInitContainer, string image)
    {
        ContainerName = containerName;
        RestartPolicy = restartPolicy;
        ImagePullPolicy = imagePullPolicy;
        IsInitContainer = isInitContainer;
        Image = image;
    }

    /// <summary>
    /// 容器名称
    /// </summary>
    public string ContainerName { get; private set; }

    /// <summary>
    /// 是否初始容器
    /// </summary>
    public bool IsInitContainer { get; private set; }

    /// <summary>
    /// 镜像名称
    /// </summary>
    public string? Image { get; private set; }


    /// <summary>
    /// 重启策略
    /// </summary>

    public string RestartPolicy { get; private set; }

    /// <summary>
    /// 镜像拉取策略
    /// </summary>

    public string ImagePullPolicy { get; private set; }

    /// <summary>
    /// 准备完成探针配置
    /// </summary>
    public ContainerSurviveConfiguration? ReadinessProbe { get; private set; }

    /// <summary>
    /// 存活探针配置
    /// </summary>
    public ContainerSurviveConfiguration? LiveNessProbe { get; private set; }

    /// <summary>
    /// 容器Cpu资源限制
    /// </summary>
    public ContainerResourceQuantity? Limits { get; private set; }

    /// <summary>
    /// 容器内存资源限制
    /// </summary>
    public ContainerResourceQuantity? Requests { get; private set; }

    /// <summary>
    /// 环境变量
    /// </summary>
    public List<KeyValuePair<string, string>> Environments { get; private set; } = default!;

    /// <summary>
    /// 容器端口配置
    /// </summary>
    public ICollection<ContainerPortConfiguration> ContainerPortConfigurations { get; private set; } = new HashSet<ContainerPortConfiguration>();


    /// <summary>
    /// 
    /// </summary>
    public DeploymentConfiguration DeploymentConfiguration { get; } = default!;


    /// <summary>
    /// 
    /// </summary>
    public string ApplicationDeploymentId { get; private set; } = default!;
    
    public MasterContainerConfiguration Update(MasterContainerConfigurationInputDto input)
    {
        ContainerName = input.ContainerName;
        RestartPolicy = input.RestartPolicy;
        ImagePullPolicy = input.ImagePullPolicy;
        IsInitContainer = input.IsInitContainer;
        Image = input.Image;
        return this;
    }

    public MasterContainerConfiguration SetReadinessProbe(ContainerSurviveConfigurationDto readinessProbe)
    {
        ReadinessProbe = new ContainerSurviveConfiguration(readinessProbe.Scheme, readinessProbe.Path, readinessProbe.Port, readinessProbe.InitialDelaySeconds, readinessProbe.PeriodSeconds);

        return this;
    }

    public MasterContainerConfiguration SetLiveNessProbe(ContainerSurviveConfigurationDto liveNessProbe)
    {
        LiveNessProbe = new ContainerSurviveConfiguration(liveNessProbe.Scheme, liveNessProbe.Path, liveNessProbe.Port, liveNessProbe.InitialDelaySeconds, liveNessProbe.PeriodSeconds);
        return this;
    }

    public MasterContainerConfiguration SetLimits(ContainerResourceQuantityDto limits)
    {
        Limits = new ContainerResourceQuantity();
        if (limits.Cpu is not null)
        {
            Limits.SetCpu(limits.Cpu);
        }

        if (limits.Memory is not null)
        {
            Limits.SetMemory(limits.Memory);
        }

        return this;
    }

    public MasterContainerConfiguration SetRequests(ContainerResourceQuantityDto requests)
    {
        Requests = new ContainerResourceQuantity();
        if (requests.Cpu is not null)
        {
            Requests.SetCpu(requests.Cpu);
        }

        if (requests.Memory is not null)
        {
            Requests.SetMemory(requests.Memory);
        }

        return this;
    }

    public MasterContainerConfiguration SetEnvironments(List<KeyValuePair<string,string>>? environments)
    {
        
        
        Environments =environments??new List<KeyValuePair<string, string>>();
        return this;
    }

    public MasterContainerConfiguration SetContainerPortConfigurations()
    {
        return this;
    }
}