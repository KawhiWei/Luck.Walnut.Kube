using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.ApplicationDeployments;

namespace Luck.Walnut.Kube.Persistence.Repositories;

public class DeploymentConfigurationRepository : EfCoreAggregateRootRepository<DeploymentConfiguration, string>, IDeploymentConfigurationRepository
{
    public DeploymentConfigurationRepository(ILuckDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(DeploymentConfigurationOutputDto[] Data, int TotalCount)> GetDeploymentConfigurationPageListAsync(string appId, DeploymentConfigurationQueryDto query)
    {
        var queryable = FindAll(x => x.AppId == appId).Select(x => new DeploymentConfigurationOutputDto
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


    public Task<DeploymentConfiguration?> FindApplicationDeploymentByIdAsync(string id)
        => FindAll()
        .Include(x=>x.).FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<DeploymentConfiguration?> FindDeploymentConfigurationByAppIdAndNameAsync(string appId, string name)
        => FindAll().FirstOrDefaultAsync(x => x.Id == name && x.AppId == appId);


    public Task<List<DeploymentConfiguration>> GetDeploymentConfigurationByAppIdListAsync(string appId)
    {
        throw new NotImplementedException();
    }
}