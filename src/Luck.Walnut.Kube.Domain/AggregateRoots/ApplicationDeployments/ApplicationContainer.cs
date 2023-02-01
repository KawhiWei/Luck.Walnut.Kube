namespace Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

public class ApplicationContainer : FullEntity
{
    public ApplicationContainer(string containerName, string restartPolicy, string imagePullPolicy, bool isInitContainer)
    {
        ContainerName = containerName;
        RestartPolicy = restartPolicy;
        ImagePullPolicy = imagePullPolicy;
        IsInitContainer = isInitContainer;
    }

    /// <summary>
    /// 容器名称
    /// </summary>
    public string ContainerName { get; private set; }

    /// <summary>
    /// 是否初始容器
    /// </summary>
    public bool IsInitContainer { get; private set; } = default!;


    /// <summary>
    /// 重启策略
    /// </summary>

    public string RestartPolicy { get; private set; } = default!;

    /// <summary>
    /// 镜像拉取策略
    /// </summary>

    public string ImagePullPolicy { get; private set; } = default!;

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


    /// <summary>
    /// 
    /// </summary>
    public ApplicationDeployment ApplicationDeployment { get; } = default!;


    public string ApplicationDeploymentId { get; private set; } = default!;

}