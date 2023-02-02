using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Domain.Repositories;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class ApplicationDeploymentRepository : EfCoreAggregateRootRepository<ApplicationDeployment, string>, IApplicationDeploymentRepository
{
    public ApplicationDeploymentRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }

    public Task<ApplicationDeployment?> GetApplicationDeploymentByIdAsync(string id)
        => FindAll().FirstOrDefaultAsync(x => x.Id == id);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<ApplicationDeployment?> GetApplicationDeploymentByAppIdAndNameAsync(string appId,string name)
        => FindAll().FirstOrDefaultAsync(x => x.Id == name && x.AppId==appId);


    public Task<List<ApplicationDeployment>> GetApplicationDeploymentByAppIdListAsync(string appId)
    {
        throw new NotImplementedException();
    }
}