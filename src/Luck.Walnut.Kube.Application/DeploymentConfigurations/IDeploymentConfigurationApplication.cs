using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Application.DeploymentConfigurations;

public interface IDeploymentConfigurationApplication : IScopedDependency
{
    Task CreateDeploymentConfigurationAsync(DeploymentConfigurationInputDto input);

    Task UpdateDeploymentConfigurationAsync(string id, DeploymentConfigurationInputDto input);

    Task DeleteDeploymentConfigurationAsync(string id);


    Task CreateDeploymentContainerAsync(string id, DeploymentContainerConfigurationInputDto input);


    Task UpdateDeploymentContainerAsync(string id, string applicationContainerId, DeploymentContainerConfigurationInputDto input);


    Task DeleteDeploymentContainerAsync(string id, string applicationContainerId);
}
