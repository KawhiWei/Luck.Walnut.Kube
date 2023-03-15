using Luck.Framework.Exceptions;
using Luck.Walnut.Kube.Domain.Shared.Enums;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;

/// <summary>
/// 应用部署配置
/// </summary>
public class DeploymentConfiguration : FullAggregateRoot
{
    public DeploymentConfiguration(string environmentName, ApplicationRuntimeTypeEnum applicationRuntimeType, DeploymentTypeEnum deploymentType, string chineseName, string name, string appId,
        string kubernetesNameSpaceId, int replicas, int maxUnavailable, string? imagePullSecretId, bool isPublish)
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
        ImagePullSecretId = imagePullSecretId;
        IsPublish = isPublish;
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
    public string? ImagePullSecretId { get; private set; }

    /// <summary>
    /// 是否发布
    /// </summary>
    public bool IsPublish { get; private set; }

    /// <summary>
    /// 主应用容器配置
    /// </summary>
    public ICollection<MasterContainerConfiguration> MasterContainers { get; private set; } = new HashSet<MasterContainerConfiguration>();

    /// <summary>
    /// 初始容器配置列表
    /// </summary>
    public ICollection<string> InitContainers { get; private set; } = new HashSet<string>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public DeploymentConfiguration SetDeploymentInfo(DeploymentConfigurationInputDto input)
    {
        EnvironmentName = input.EnvironmentName;
        ApplicationRuntimeType = input.ApplicationRuntimeType;
        DeploymentType = input.DeploymentType;
        ChineseName = input.ChineseName;
        AppId = input.AppId;
        KubernetesNameSpaceId = input.KubernetesNameSpaceId;
        Replicas = input.Replicas;
        MaxUnavailable = input.MaxUnavailable;
        ImagePullSecretId = input.ImagePullSecretId;
        Name=input.Name;
        return this;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public DeploymentConfiguration SetInitContainers(List<string>? initContainers)
    {
        if(initContainers is null)
        {
            return this;
        }

        InitContainers = initContainers;
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationContainerId"></param>
    /// <returns></returns>
    public DeploymentConfiguration RemoveDeploymentContainerConfiguration(string applicationContainerId)
    {
        var applicationContainer = MasterContainers.FirstOrDefault(x => x.Id == applicationContainerId);
        if (applicationContainer is null)
        {
            throw new BusinessException($"容器配置不存在，请刷新页面");
        }

        MasterContainers.Remove(applicationContainer);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public DeploymentConfiguration AddMasterContainerConfiguration(MasterContainerConfigurationInputDto input)
    {
        if (CheckApplicationContainerName(input.ContainerName))
        {
            throw new BusinessException($"【{input.ContainerName}】已存在");
        }

        var applicationContainer = new MasterContainerConfiguration(input.ContainerName,
            input.RestartPolicy, input.ImagePullPolicy, input.IsInitContainer, input.Image??"");
        if (input.Limits is not null)
        {
            applicationContainer.SetLimits(input.Limits);
        }

        if (input.Requests is not null)
        {
            applicationContainer.SetRequests(input.Requests);
        }

        if (input.LiveNessProbe is not null)
        {
            applicationContainer.SetLiveNessProbe(input.LiveNessProbe);
        }

        if (input.ReadinessProbe is not null)
        {
            applicationContainer.SetReadinessProbe(input.ReadinessProbe);
        }
        
        applicationContainer.SetEnvironments(input.Environments ?? new List<KeyValuePair<string, string>>());
        MasterContainers.Add(applicationContainer);

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="currentId"></param>
    /// <param name="isUpdate"></param>
    /// <returns></returns>
    private bool CheckApplicationContainerName(string containerName, string currentId = "", bool isUpdate = false)
    {
        if (isUpdate)
        {
            return MasterContainers.Any(x => x.ContainerName == containerName && x.Id != currentId);
        }

        return MasterContainers.Any(x => x.ContainerName == containerName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationContainerId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public DeploymentConfiguration UpdateMasterContainerConfiguration(string applicationContainerId, MasterContainerConfigurationInputDto input)
    {
        var applicationContainer = MasterContainers.FirstOrDefault(x => x.Id == applicationContainerId);
        if (applicationContainer is null)
        {
            throw new BusinessException($"容器配置不存在，请刷新页面");
        }

        if (CheckApplicationContainerName(input.ContainerName, applicationContainerId, true))
        {
            throw new BusinessException($"【{input.ContainerName}】已存在");
        }

        applicationContainer.Update(input);
        return this;
    }
}