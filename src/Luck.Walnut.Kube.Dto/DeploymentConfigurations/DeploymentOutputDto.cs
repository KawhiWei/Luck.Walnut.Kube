namespace Luck.Walnut.Kube.Dto.DeploymentConfigurations;

public class DeploymentOutputDto 
{
    /// <summary>
    /// 部署配置输入对象
    /// </summary>
    public DeploymentConfigurationOutputDto? DeploymentConfiguration { get; set; }  

    /// <summary>
    /// 主容器配置输入对象
    /// </summary>
    public MasterContainerConfigurationOutputDto? MasterContainerConfiguration { get; set; }  
}