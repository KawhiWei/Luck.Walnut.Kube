using k8s;
using k8s.Models;
using Luck.Framework.Extensions;
using Luck.Walnut.Kube.Adapter.Factories;
using Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter.Deployments;

public class DeploymentResource : IDeploymentResource
{
    private readonly IKubernetesClientFactory _kubernetesClientFactory;

    public DeploymentResource(IKubernetesClientFactory kubernetesClientFactory)
    {
        _kubernetesClientFactory = kubernetesClientFactory;
    }

    public Task CreateDeploymentAsync(V1Deployment v1Deployment)
    {
        var client = _kubernetesClientFactory.GetKubernetesClient("");

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


    public async Task<List<KubernetesDeployment>> GetDeploymentListAsync(IKubernetes kubernetes,
        string nameSpace = "")
    {
        V1DeploymentList v1DeploymentList;
        if (nameSpace.IsNotNull())
        {
            v1DeploymentList = await kubernetes.AppsV1.ListNamespacedDeploymentAsync(nameSpace);
            return GetKubernetesDeploymentListList(v1DeploymentList.Items).ToList();    
        }
        v1DeploymentList = await kubernetes.AppsV1.ListDeploymentForAllNamespacesAsync();
        return GetKubernetesDeploymentListList(v1DeploymentList.Items).ToList();
    }


    private List<KubernetesDeployment> GetKubernetesDeploymentListList(IList<V1Deployment> v1Deployments)
    {
        return v1Deployments.Select(v1Deployment =>
        {
            var kubernetesDaemonSet =
                new KubernetesDeployment(v1Deployment.Metadata.Name, v1Deployment.Status.Replicas,
                    v1Deployment.Status.ReadyReplicas, v1Deployment.Status.AvailableReplicas);
            return kubernetesDaemonSet;
        }).ToList();
    }

}