using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class ApplicationDeploymentRepository : EfCoreAggregateRootRepository<ApplicationDeployment, string>, IApplicationDeploymentRepository
{
    public ApplicationDeploymentRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(ApplicationDeploymentOutputDto[] Data, int TotalCount)> GetApplicationDeploymentPageListAsync(string appId, ApplicationDeploymentQueryDto query)
    {
        var queryable = FindAll(x => x.AppId == appId).Select(x => new ApplicationDeploymentOutputDto
        {
            Id= x.Id,
            AppId = x.AppId,
            ApplicationRuntimeType = x.ApplicationRuntimeType,
            DeploymentType = x.DeploymentType,
            ChineseName = x.ChineseName,
            Name = x.Name,
            KubernetesNameSpaceId = x.KubernetesNameSpaceId,
            Replicas = x.Replicas,
            MaxUnavailable = x.MaxUnavailable,
            ImagePullSecretId = x.ImagePullSecretId,

        });
        var totalCount = await queryable.CountAsync();
        var list = await queryable.ToPage(query.PageIndex, query.PageSize).ToArrayAsync();

        return (list, totalCount);

    }


    public Task<ApplicationDeployment?> GetApplicationDeploymentByIdAsync(string id)
        => FindAll().FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<ApplicationDeployment?> GetApplicationDeploymentByAppIdAndNameAsync(string appId, string name)
        => FindAll().FirstOrDefaultAsync(x => x.Id == name && x.AppId == appId);


    public Task<List<ApplicationDeployment>> GetApplicationDeploymentByAppIdListAsync(string appId)
    {
        throw new NotImplementedException();
    }
}