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


    public Task<List<ApplicationDeployment>> GetApplicationDeploymentByAppIdListAsync(string appId)
    {
        throw new NotImplementedException();
    }
}