using Luck.Framework.Extensions;

namespace Luck.Walnut.Kube.Dto.ApplicationDeployments;

public class DeploymentConfigurationOutputDto : DeploymentConfigurationBaseDto
{
    /// <summary>
    /// 唯一Id
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// 应用容器配置
    /// </summary>
    public ICollection<DeploymentContainerConfigurationInputDto>? ApplicationContainers { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string ApplicationRuntimeTypeName => ApplicationRuntimeType.ToDescription();

    /// <summary>
    /// 部署名称
    /// </summary>
    public string DeploymentTypeName => DeploymentType.ToDescription();
}