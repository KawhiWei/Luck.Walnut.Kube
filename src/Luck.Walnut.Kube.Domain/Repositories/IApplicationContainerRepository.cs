using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IApplicationContainerRepository : IEntityRepository<ApplicationContainer, string>, IScopedDependency
{
    /// <summary>
    /// 根据Id获取一个应用部署数据
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    Task<List<ApplicationContainer>> GetApplicationContainerListByApplicationDeploymentIdAsync(string applicationDeploymentId);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    Task<List<ApplicationContainerOutputDto>> GetByApplicationDeploymentIdListAsync(string applicationDeploymentId);
}