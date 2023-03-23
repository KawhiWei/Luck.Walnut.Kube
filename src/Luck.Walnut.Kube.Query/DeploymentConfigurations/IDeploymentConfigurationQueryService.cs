

using Luck.Walnut.Kube.Dto.DeploymentConfigurations;
using Luck.Walnut.Kube.Dto;

namespace Luck.Walnut.Kube.Query.DeploymentConfigurations;

public interface IDeploymentConfigurationQueryService:IScopedDependency
{

    /// <summary>
    /// ��ҳ��ѯ��������
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<PageBaseResult<DeploymentConfigurationOutputDto>> GetDeploymentConfigurationPageListAsync(string appId, DeploymentConfigurationQueryDto query);


    Task<DeploymentOutputDto?> GetDeploymentConfigurationDetailByIdAsync(string deploymentId,string masterContainerId);

    /// <summary>
    /// 根据AppId查询部署列表
    /// </summary>
    /// <param name="appId"></param>
    /// <returns></returns>
    Task<List<DeploymentConfigurationOutputDto>> GetDeploymentConfigurationByAppIdListAsync(string appId);
}