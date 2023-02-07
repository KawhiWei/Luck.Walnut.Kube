using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

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


public class AppConfigurationRepository : EfCoreEntityRepository<ApplicationContainer, string>, IApplicationContainerRepository
{
    public AppConfigurationRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }


    public Task<List<ApplicationContainer>> GetApplicationContainerListByApplicationDeploymentIdAsync(string applicationDeploymentId)
    {
        throw new NotImplementedException();

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationDeploymentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<ApplicationContainerOutputDto>> GetByApplicationDeploymentIdListAsync(string applicationDeploymentId)
    {

        return FindAll(x => x.ApplicationDeploymentId == applicationDeploymentId).Select(x => new ApplicationContainerOutputDto
        {
            Id = x.Id,
            ContainerName = x.ContainerName,
            RestartPolicy = x.RestartPolicy,
            ImagePullPolicy = x.ImagePullPolicy,
            Image=x.Image
        }).ToListAsync();
    }

}

