using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class DeploymentConfigurationRepository : EfCoreAggregateRootRepository<DeploymentConfiguration, string>, IDeploymentConfigurationRepository
{
    public DeploymentConfigurationRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(DeploymentConfiguration[] Data, int TotalCount)> GetDeploymentConfigurationPageListAsync(string appId, DeploymentConfigurationQueryDto query)
    {
        var queryable = FindAll(x => x.AppId == appId);
        var totalCount = await queryable.CountAsync();
        var list = await queryable.ToPage(query.PageIndex, query.PageSize).ToArrayAsync();

        return (list, totalCount);

    }


    public Task<DeploymentConfiguration?> FindDeploymentConfigurationByIdAsync(string id)
        => FindAll()
        .Include(x=>x.MasterContainers).FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<DeploymentConfiguration?> FindDeploymentConfigurationByAppIdAndNameAsync(string appId, string name)
        => FindAll().FirstOrDefaultAsync(x => x.Id == name && x.AppId == appId);


    public Task<List<DeploymentConfiguration>> GetDeploymentConfigurationByAppIdListAsync(string appId)
    {
        return FindAll(x => x.AppId == appId).ToListAsync();
    }
}