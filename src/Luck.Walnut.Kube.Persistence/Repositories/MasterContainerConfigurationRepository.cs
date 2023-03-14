using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class MasterContainerConfigurationRepository : EfCoreEntityRepository<MasterContainerConfiguration, string>, IMasterContainerConfigurationRepository
{
    public MasterContainerConfigurationRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public Task<List<MasterContainerConfiguration>> GetApplicationContainerListByApplicationDeploymentIdAsync(string applicationDeploymentId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<MasterContainerConfiguration>> GetListByApplicationDeploymentIdAsync(string applicationDeploymentId)
    {
        return FindAll(x => x.ApplicationDeploymentId == applicationDeploymentId).ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    public Task<MasterContainerConfiguration?> FindApplicationContainerByIdFirstOrDefaultAsync(string id)
    {
        return FindAll(x => x.Id == id).FirstOrDefaultAsync();
    }

}