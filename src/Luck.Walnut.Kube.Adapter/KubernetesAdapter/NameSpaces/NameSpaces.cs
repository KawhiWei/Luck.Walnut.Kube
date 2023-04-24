using k8s;
using k8s.Models;

using Luck.Framework.Infrastructure.DependencyInjectionModule;
using Luck.Walnut.Kube.Adapter.Factories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter.NameSpaces
{
    public class NameSpaces : INameSpaces
    {
        private readonly IKubernetesClientFactory _kubernetesClientFactory;

        public Task CreateNameSpaceAsync(string config, V1Namespace v1Namespace)
        {
            var client = _kubernetesClientFactory.GetKubernetesClient(config);
            client.CoreV1.CreateNamespaceAsync(v1Namespace);


            throw new NotImplementedException();
        }
    }

    public interface INameSpaces : IScopedDependency
    {
        Task CreateNameSpaceAsync(string config,V1Namespace v1Namespace);


    }
}
