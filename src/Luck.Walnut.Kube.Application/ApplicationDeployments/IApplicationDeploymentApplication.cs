using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Application.ApplicationDeployments;

public interface IApplicationDeploymentApplication : IScopedDependency
{
    Task CreateApplicationDeploymentAsync(ApplicationDeploymentInputDto input);

    Task UpdateApplicationDeploymentAsync(string id, ApplicationDeploymentInputDto input);

    Task DeleteApplicationDeploymentAsync(string id);


    Task CreateApplicationContainerAsync(string id, ApplicationContainerInputDto input);


    Task UpdateApplicationContainerAsync(string id, string applicationContainerId, ApplicationContainerInputDto input);


    Task DeleteApplicationContainerAsync(string id, string applicationContainerId);
}
