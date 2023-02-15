using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.AggregateRoots.Services;
using Luck.Walnut.Kube.Domain.Repositories;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class ServiceRepository : EfCoreAggregateRootRepository<Service, string>, IServiceRepository
{
    public ServiceRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Service?> FindServiceByNameAndNameSpaceIdAsync(string name,string nameSpaceId)
    {
        return await this.FindAll().FirstOrDefaultAsync(x => x.Name == name && x.NameSpaceId==nameSpaceId);
    }

    public async Task<Service?> FindServiceByIdAsync(string id)
    {
        return await this.FindAll().FirstOrDefaultAsync(x => x.Name == id);
    }
}