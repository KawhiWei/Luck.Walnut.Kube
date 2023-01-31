using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface INameSpaceRepository: IAggregateRootRepository<NameSpace,string>,IScopedDependency
{
    
}