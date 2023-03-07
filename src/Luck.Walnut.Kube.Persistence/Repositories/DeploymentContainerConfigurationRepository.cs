using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class DeploymentContainerConfigurationRepository : EfCoreEntityRepository<DeploymentContainerConfiguration, string>, IDeploymentContainerConfigurationRepository
{
    public DeploymentContainerConfigurationRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public Task<List<DeploymentContainerConfiguration>> GetApplicationContainerListByApplicationDeploymentIdAsync(string applicationDeploymentId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<DeploymentContainerConfiguration>> GetListByApplicationDeploymentIdAsync(string applicationDeploymentId)
    {
        return FindAll(x => x.ApplicationDeploymentId == applicationDeploymentId).ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    public Task<DeploymentContainerConfiguration?> FindApplicationContainerByIdFirstOrDefaultAsync(string id)
    {
        return FindAll(x => x.Id == id).FirstOrDefaultAsync();
    }

}