using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;

namespace Luck.Walnut.Kube.Domain.Repositories;

public interface IClusterRepository: IAggregateRootRepository<Cluster,string>,IScopedDependency
{
    
}