using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.Services;
using Luck.Walnut.Kube.Dto.Services;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IServiceRepository : IAggregateRootRepository<Service, string>, IScopedDependency
{
    Task<Service?> FindServiceByNameAndNameSpaceIdAsync(string name, string nameSpaceId);


    Task<Service?> FindServiceByIdAsync(string id);
    
    
    Task<(Service[] Data,int TotalCount)> GetServicePageListAsync( ServiceQueryDto query);
}