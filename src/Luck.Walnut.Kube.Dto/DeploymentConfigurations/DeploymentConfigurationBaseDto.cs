using Luck.Walnut.Kube.Domain.Shared.Enums;

namespace Luck.Walnut.Kube.Dto.DeploymentConfigurations;

public class DeploymentConfigurationBaseDto
{
    /// <summary>
    /// 部署环境
    /// </summary>
    public string EnvironmentName { get; set; } = default!;

    /// <summary>
    /// 应用运行时类型
    /// </summary>
    public ApplicationRuntimeTypeEnum ApplicationRuntimeType { get; set; }

    /// <summary>
    /// 部署类型
    /// </summary>
    public DeploymentTypeEnum DeploymentType { get; set; }

    /// <summary>
    /// 中文名称
    /// </summary>
    public string ChineseName { get; set; } = default!;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// 应用Id
    /// </summary>
    public string AppId { get; set; } = default!;

    /// <summary>
    /// 命名空间Id
    /// </summary>
    public string KubernetesNameSpaceId { get; set; } = default!;

    /// <summary>
    /// 部署副本数量
    /// </summary>
    public int Replicas { get; set; }

    /// <summary>
    /// 最大不可用
    /// </summary>
    public int MaxUnavailable { get; set; }

    /// <summary>
    /// 镜像拉取证书
    /// </summary>
    public string? ImagePullSecretId { get; set; }
    
}