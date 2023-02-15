using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.AggregateRoots.Services;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IServiceRepository: IAggregateRootRepository<Service,string>,IScopedDependency
{

    Task<NameSpace?> FindServiceByNameAsync(string name);


    Task<NameSpace?> FindServiceByIdAsync(string id);
}