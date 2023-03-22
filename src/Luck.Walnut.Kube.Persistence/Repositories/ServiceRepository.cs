using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.Services;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.Services;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class ServiceRepository : EfCoreAggregateRootRepository<Service, string>, IServiceRepository
{
    public ServiceRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Service?> FindServiceByNameAndNameSpaceIdAsync(string name, string nameSpaceId)
    {
        return await this.FindAll().FirstOrDefaultAsync(x => x.Name == name && x.NameSpaceId == nameSpaceId);
    }

    public async Task<Service?> FindServiceByIdAsync(string id)
    {
        return await this.FindAll().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<(Service[] Data, int TotalCount)> GetServicePageListAsync(ServiceQueryDto query)
    {
        var queryable = this.FindAll();
        var totalCount = await queryable.CountAsync();
        var list = await queryable.ToPage(query.PageIndex, query.PageSize).ToArrayAsync();
        return (list, totalCount);
    }
}