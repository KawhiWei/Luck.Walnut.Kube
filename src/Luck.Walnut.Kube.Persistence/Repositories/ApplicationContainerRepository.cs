using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class ApplicationContainerRepository : EfCoreEntityRepository<ApplicationContainer, string>, IApplicationContainerRepository
{
    public ApplicationContainerRepository(ILuckDbContext dbContext) : base(dbContext)
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
    public Task<List<ApplicationContainerOutputDto>> GetListByApplicationDeploymentIdAsync(string applicationDeploymentId)
    {
        return FindAll(x => x.ApplicationDeploymentId == applicationDeploymentId).Select(x => new ApplicationContainerOutputDto
        {
            Id = x.Id,
            ContainerName = x.ContainerName,
            RestartPolicy = x.RestartPolicy,
            ImagePullPolicy = x.ImagePullPolicy,
            Image = x.Image
        }).ToListAsync();
    }
}