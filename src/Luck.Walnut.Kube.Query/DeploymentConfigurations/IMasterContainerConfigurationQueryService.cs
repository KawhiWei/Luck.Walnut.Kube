using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Query.DeploymentConfigurations;

public interface IMasterContainerConfigurationQueryService : IScopedDependency
{
    /// <summary>
    /// 根据ApplicationDeploymentId查询一组部署容器配置
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    Task<List<MasterContainerConfigurationOutputDto>> GetDeploymentContainerConfigurationListByDeploymentIdAsync(string applicationDeploymentId);

    /// <summary>
    /// 根据Id获取一个容器配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<MasterContainerConfigurationOutputDto?> GetApplicationContainerByIdFirstOrDefaultAsync(string id);
}