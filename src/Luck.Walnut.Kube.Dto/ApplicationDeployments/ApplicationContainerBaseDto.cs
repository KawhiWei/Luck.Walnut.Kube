namespace Luck.Walnut.Kube.Dto.ApplicationDeployments;

public class ApplicationContainerBaseDto
{
    /// <summary>
    /// 容器名称
    /// </summary>
    public string ContainerName { get; set; } = default!;

    /// <summary>
    /// 重启策略
    /// </summary>

    public string RestartPolicy { get; set; } = default!;

    /// <summary>
    /// 镜像拉取策略
    /// </summary>

    public string ImagePullPolicy { get; set; } = default!;

    /// <summary>
    /// 准备完成探针配置
    /// </summary>
    public ContainerSurviveConfigurationDto? ReadinessProbe { get; set; } = default!;

    /// <summary>
    /// 存活探针配置
    /// </summary>
    public ContainerSurviveConfigurationDto? LiveNessProbe { get; set; } = default!;

    /// <summary>
    /// 容器Cpu资源限制
    /// </summary>
    public ContainerResourceQuantityDto? CpuContainerResourceQuantity { get; set; } = default!;

    /// <summary>
    /// 容器内存资源限制
    /// </summary>
    public ContainerResourceQuantityDto? MemoryContainerResourceQuantity { get; set; } = default!;

    /// <summary>
    /// 环境变量
    /// </summary>
    public Dictionary<string, string>? Environments { get; set; } = default!;


    /// <summary>
    /// 容器端口配置
    /// </summary>
    public ICollection<ContainerPortConfigurationDto>? ContainerPortConfigurations { get; set; } = default!;
}