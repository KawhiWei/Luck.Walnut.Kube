using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.SideCarPlugins;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.SideCarPlugins;

namespace Luck.Walnut.Kube.Persistence.Repositories
{
    public class SideCarPluginRepository : EfCoreAggregateRootRepository<SideCarPlugin, string>, ISideCarPluginRepository
    {
        public SideCarPluginRepository(ILuckDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(SideCarPlugin[] Data, int TotalCount)> GetInitContainerConfigurationPageListAsync(SideCarPluginQueryDto query)
        {
            var queryable = FindAll();
            var totalCount = await queryable.CountAsync();
            var list = await queryable.ToPage(query.PageIndex, query.PageSize).ToArrayAsync();

            return (list, totalCount);
        }


        public Task<SideCarPlugin?> FindSideCarPluginByNameAsync(string name) => FindAll().FirstOrDefaultAsync(x => x.ContainerName == name);


        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<SideCarPlugin?> FindSideCarPluginByIdAsync(string id) => FindAll().FirstOrDefaultAsync(x => x.Id == id);
    }
}