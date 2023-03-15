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


    public Task<List<MasterContainerConfiguration>> GetApplicationContainerListByApplicationDeploymentIdAsync(string deploymentId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deploymentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<MasterContainerConfiguration>> GetListByDeploymentIdAsync(string deploymentId)
    {
        return FindAll(x => x.DeploymentId == deploymentId).ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<MasterContainerConfiguration?> FindMasterContainerByIdFirstOrDefaultAsync(string id)
    {
        return FindAll(x => x.Id == id).FirstOrDefaultAsync();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="deploymentIds"></param>
    /// <returns></returns>
    public Task<List<MasterContainerConfiguration>> GetListByDeploymentIdsAsync(List<string> deploymentIds)
    {
        return FindAll(x => deploymentIds.Contains(x.DeploymentId)).ToListAsync();
    }
}