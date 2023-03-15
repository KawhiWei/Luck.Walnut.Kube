using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Application.DeploymentConfigurations;

public interface IDeploymentConfigurationApplication : IScopedDependency
{
    Task CreateDeploymentConfigurationAsync(DeploymentInputDto input);

    Task UpdateDeploymentConfigurationAsync(string id, DeploymentConfigurationInputDto input);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="masterContainerId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateDeploymentConfigurationAsync(string id,string masterContainerId, DeploymentInputDto input);

    Task DeleteDeploymentConfigurationAsync(string id);


    Task CreateDeploymentContainerConfigurationAsync(string deploymentConfigurationId, MasterContainerConfigurationInputDto input);


    Task UpdateDeploymentContainerConfigurationAsync(string deploymentConfigurationId, string deploymentContainerConfigurationId, MasterContainerConfigurationInputDto input);


    Task DeleteDeploymentContainerConfigurationAsync(string deploymentConfigurationId, string deploymentContainerConfigurationId);
}
