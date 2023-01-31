namespace Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

public class ApplicationContainer : FullEntity
{
    public ApplicationContainer(string containerName, string restartPolicy, string imagePullPolicy)
    {
        ContainerName = containerName;
        RestartPolicy = restartPolicy;
        ImagePullPolicy = imagePullPolicy;
    }

    /// <summary>
    /// 容器名称
    /// </summary>
    public string ContainerName { get; private set; }

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
    public ContainerSurviveConfiguration ReadinessProbe { get; private set; } = default!;

    /// <summary>
    /// 存活探针配置
    /// </summary>
    public ContainerSurviveConfiguration LiveNessProbe { get; private set; } = default!;

    /// <summary>
    /// 容器Cpu资源限制
    /// </summary>
    public ContainerResourceQuantity CpuContainerResourceQuantity { get; private set; } = default!;

    /// <summary>
    /// 容器内存资源限制
    /// </summary>
    public ContainerResourceQuantity MemoryContainerResourceQuantity { get; private set; } = default!;

    /// <summary>
    /// 环境变量
    /// </summary>
    public EnvironmentConfiguration Environments { get; private set; } = default!;


    /// <summary>
    /// 容器端口配置
    /// </summary>
    public ICollection<ContainerPortConfiguration> ContainerPortConfigurations = new HashSet<ContainerPortConfiguration>();
}