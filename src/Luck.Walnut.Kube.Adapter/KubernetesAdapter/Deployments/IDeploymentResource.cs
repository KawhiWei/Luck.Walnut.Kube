using k8s;
using k8s.Models;

using Luck.Framework.Infrastructure.DependencyInjectionModule;
using Luck.Walnut.Kube.Adapter.Factories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class DeploymentResource : IDeploymentResource
    {
        private readonly IKubernetesClientFactory _kubernetesClientFactory;

        public DeploymentResource(IKubernetesClientFactory kubernetesClientFactory)
        {
            _kubernetesClientFactory = kubernetesClientFactory;
        }

        public Task CreateDeploymentAsync(V1Deployment v1Deployment)
        {


            var client= _kubernetesClientFactory.GetKubernetesClient("");

            client.AppsV1.CreateNamespacedDeployment(v1Deployment, "");
            throw new NotImplementedException();
        }

        public Task DeleteDeploymentAsync(V1Deployment v1Deployment)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDeploymentAsync(V1Deployment v1Deployment)
        {
            throw new NotImplementedException();
        }
    }



}
