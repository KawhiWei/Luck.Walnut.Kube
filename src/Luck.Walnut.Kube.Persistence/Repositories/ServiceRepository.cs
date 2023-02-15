using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.Repositories;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class ServiceRepository : EfCoreAggregateRootRepository<Service, string>, IServiceRepository
{
    public ServiceRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<NameSpace?> FindNameSpaceByNameAsync(string name)
    {
        return await this.FindAll().FirstOrDefaultAsync(x => x.Name == name);
    }
    
    public async Task<NameSpace?> FindNameSpaceByIdAsync(string id)
    {
        return await this.FindAll().FirstOrDefaultAsync(x => x.Name == id);
    }
}