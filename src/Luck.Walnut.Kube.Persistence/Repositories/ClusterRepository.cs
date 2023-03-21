using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.Clusteries;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class ClusterRepository : EfCoreAggregateRootRepository<Cluster, string>, IClusterRepository
{
    public ClusterRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<(Cluster[] Data, int TotalCount)> GetClusterPageListAsync(ClusterQueryDto query)
    {
        var queryable = FindAll();
        var totalCount = await queryable.CountAsync();
        var list = await queryable.ToPage(query.PageIndex, query.PageSize).ToArrayAsync();
        return (list, totalCount);
    }


    public async Task<List<ClusterOutputDto>> GetClusterOutputDtoListAsync()
    {
        return await FindAll().Select(x => new ClusterOutputDto()
        {
            Id = x.Id,
            Name = x.Name,
        }).ToListAsync();
    }

    public async Task<Cluster?> GetClusterFindByIdAsync(string id)
    {
        return await FindAll().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Cluster>> GetClusterFindByIdListAsync(List<string> idList)
    {
        return await FindAll(x => idList.Contains(x.Id)).ToListAsync();
    }
}