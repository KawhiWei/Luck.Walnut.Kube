using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Domain.Repositories;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class ClusterRepository: EfCoreAggregateRootRepository<Cluster, string>,IClusterRepository
{
    public ClusterRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<object> GetAllClusterListAsync()
    {
       return  await this.FindAll().ToListAsync();
    }
    
}