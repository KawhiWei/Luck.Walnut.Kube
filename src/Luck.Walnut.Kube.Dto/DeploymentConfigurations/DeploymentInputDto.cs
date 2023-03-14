namespace Luck.Walnut.Kube.Dto.DeploymentConfigurations;

/// <summary>
/// 输入对象
/// </summary>
public class DeploymentInputDto
{
    /// <summary>
    /// 部署配置输入对象
    /// </summary>
    public DeploymentConfigurationInputDto DeploymentConfiguration { get; set; } = default!;

    /// <summary>
    /// 主容器配置输入对象
    /// </summary>
    public MasterContainerConfigurationInputDto MasterContainerConfiguration { get; set; } = default!;
}
