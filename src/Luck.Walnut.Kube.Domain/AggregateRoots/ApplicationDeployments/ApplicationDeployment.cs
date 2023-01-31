using Luck.Walnut.Kube.Domain.Shared.Enums;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

/// <summary>
/// 应用部署配置
/// </summary>
public class ApplicationDeployment : FullAggregateRoot
{
    public ApplicationDeployment(string environmentName, ApplicationRuntimeTypeEnum applicationRuntimeType, DeploymentTypeEnum deploymentType, string chineseName, string name, string appId, string kubernetesNameSpaceId, int replicas, int maxUnavailable, string imagePullSecrets)
    {
        EnvironmentName = environmentName;
        ApplicationRuntimeType = applicationRuntimeType;
        DeploymentType = deploymentType;
        ChineseName = chineseName;
        Name = name;
        AppId = appId;
        KubernetesNameSpaceId = kubernetesNameSpaceId;
        Replicas = replicas;
        MaxUnavailable = maxUnavailable;
        ImagePullSecrets = imagePullSecrets;
    }

    /// <summary>
    /// 部署环境
    /// </summary>
    public string EnvironmentName { get; private set; }
    
    /// <summary>
    /// 应用运行时类型
    /// </summary>
    public ApplicationRuntimeTypeEnum ApplicationRuntimeType { get; private set; }

    /// <summary>
    /// 部署类型
    /// </summary>
    public DeploymentTypeEnum DeploymentType { get; private set; }

    /// <summary>
    /// 中文名称
    /// </summary>
    public string ChineseName { get; private set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public string AppId { get; private set; }

    /// <summary>
    /// 命名空间Id
    /// </summary>
    public string KubernetesNameSpaceId { get; private set; }

    /// <summary>
    /// 部署副本数量
    /// </summary>
    public int Replicas { get; private set; }

    /// <summary>
    /// 最大不可用
    /// </summary>
    public int MaxUnavailable { get; private set; }

    /// <summary>
    /// 镜像拉取证书
    /// </summary>
    public string ImagePullSecrets { get; private set; }

    /// <summary>
    /// 应用容器配置
    /// </summary>
    public ICollection<ApplicationContainer> ApplicationContainers { get; private set; } = new HashSet<ApplicationContainer>();
}