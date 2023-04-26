using k8s.Models;
using Luck.Framework.Infrastructure.DependencyInjectionModule;
using k8s;
using Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter.Deployments
{
    public interface IDeploymentResource : IScopedDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1Deployment"></param>
        /// <returns></returns>
        Task CreateDeploymentAsync(V1Deployment v1Deployment);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1Deployment"></param>
        /// <returns></returns>

        Task UpdateDeploymentAsync(V1Deployment v1Deployment);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1Deployment"></param>
        /// <returns></returns>
        Task DeleteDeploymentAsync(V1Deployment v1Deployment);


        /// <summary>
        /// 获取Deployment
        /// </summary>
        /// <param name="kubernetes"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task<List<KubernetesDeployment>> GetDeploymentListAsync(IKubernetes kubernetes, string nameSpace = "");
    }
}
