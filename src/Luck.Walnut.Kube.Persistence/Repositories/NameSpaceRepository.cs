using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.Repositories;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class NameSpaceRepository : EfCoreAggregateRootRepository<NameSpace, string>, INameSpaceRepository
{
    public NameSpaceRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<NameSpace?> FIndNameSpaceByNameAsync(string name)
    {
        return await this.FindAll().FirstOrDefaultAsync(x => x.Name == name);
    }
    
    public async Task<NameSpace?> FIndNameSpaceByIdAsync(string id)
    {
        return await this.FindAll().FirstOrDefaultAsync(x => x.Name == id);
    }
}