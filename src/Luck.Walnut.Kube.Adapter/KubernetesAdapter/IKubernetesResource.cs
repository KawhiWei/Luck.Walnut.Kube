using k8s.Models;

using Luck.Framework.Infrastructure.DependencyInjectionModule;
using Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter;

public interface IKubernetesResource:IScopedDependency
{
     /// <summary>
     /// 获取K8s节点信息
     /// </summary>
     /// <returns></returns>
     Task<KubernetesManager> GetDashboardAsync(string config);
     
     Task GetNameSpaceListAsync(string config);

     Task<object> GetPodListAsync(string config);
     
    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
    /// <param name="nameSpace"></param>
    /// <returns></returns>
     Task<object> GetPodListAsync(string config,string nameSpace);

     Task CreateDeploymentAsync(V1Deployment v1Deployment);

}


