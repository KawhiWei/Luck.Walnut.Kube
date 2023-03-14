using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Application.DeploymentConfigurations;

public interface IDeploymentConfigurationApplication : IScopedDependency
{
    Task CreateDeploymentConfigurationAsync(DeploymentConfigurationInputDto input);

    Task UpdateDeploymentConfigurationAsync(string id, DeploymentConfigurationInputDto input);

    Task DeleteDeploymentConfigurationAsync(string id);


    Task CreateDeploymentContainerConfigurationAsync(string deploymentConfigurationId, MasterContainerConfigurationInputDto input);


    Task UpdateDeploymentContainerConfigurationAsync(string deploymentConfigurationId, string deploymentContainerConfigurationId, MasterContainerConfigurationInputDto input);


    Task DeleteDeploymentContainerConfigurationAsync(string deploymentConfigurationId, string deploymentContainerConfigurationId);
}
