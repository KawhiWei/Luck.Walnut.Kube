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
    /// 是否初始容器
    /// </summary>
    public bool IsInitContainer { get;  set; }
    /// <summary>
    /// 镜像拉取策略
    /// </summary>

    public string ImagePullPolicy { get; set; } = default!;
    
    /// <summary>
    /// 镜像名称
    /// </summary>
    public string Image { get;  set; } = default!;

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
    public ContainerResourceQuantityDto? Limits { get; set; } = default!;

    /// <summary>
    /// 容器内存资源限制
    /// </summary>
    public ContainerResourceQuantityDto? Requests { get; set; } = default!;

    /// <summary>
    /// 环境变量
    /// </summary>
    public Dictionary<string, string>? Environments { get; set; } = default!;


    /// <summary>
    /// 容器端口配置
    /// </summary>
    public List<ContainerPortConfigurationDto>? ContainerPortConfigurations { get; set; } = default!;
}