using Luck.EntityFrameworkCore.DbContexts;
using Luck.EntityFrameworkCore.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luck.Walnut.Kube.Persistence.Repositories
{
    public class InitContainerConfigurationRepository : EfCoreAggregateRootRepository<InitContainerConfiguration, string>, IInitContainerConfigurationRepository
    {
        public InitContainerConfigurationRepository(ILuckDbContext dbContext) : base (dbContext)
        {
        }

        public Task<InitContainerConfiguration?> FindInitContainerConfigurationByNameAsunc(string name) => FindAll().FirstOrDefaultAsync(x => x.ContainerName == name);
        
    }
}
