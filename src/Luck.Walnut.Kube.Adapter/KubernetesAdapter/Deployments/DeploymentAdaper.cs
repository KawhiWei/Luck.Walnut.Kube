using k8s;
using k8s.Models;

using Luck.Framework.Extensions;
using Luck.Walnut.Kube.Adapter.Factories;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;
using Luck.Walnut.Kube.Infrastructure;

using RazorEngine.Templating;

namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter.Deployments;

public class DeploymentAdaper : IDeploymentAdaper
{
    private readonly IKubernetesClientFactory _kubernetesClientFactory;

    public DeploymentAdaper(IKubernetesClientFactory kubernetesClientFactory)
    {
        _kubernetesClientFactory = kubernetesClientFactory;
    }

    public Task CreateDeploymentAsync(IKubernetes kubernetes, DeploymentConfiguration deployment, List<InitContainerConfiguration> initContainerConfigurations)
    {
        //var client = _kubernetesClientFactory.GetKubernetesClient("");

        //client.AppsV1.CreateNamespacedDeployment(v1Deployment, "");

        //kubernetes.AppsV1.createdep

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

    private V1Deployment GetV1Deployment(DeploymentConfiguration deployment, List<InitContainerConfiguration> initContainerConfigurations)
    {
        var labels = Constants.GetKubeDefalutLabels();
        return new V1Deployment()
        {

            Metadata = GetV1ObjectMeta(deployment.Name, labels),
            Spec = new V1DeploymentSpec()
        };
    }

    private V1ObjectMeta GetV1ObjectMeta(string name, IDictionary<string, string> keyValuePairs)
    {
        return new V1ObjectMeta() { Labels = keyValuePairs, Name = name };
    }

    private V1DeploymentSpec GetV1DeploymentSpec(string name, int replicas, int revisionHistoryLimit, V1PodTemplateSpec v1PodTemplateSpec)
    {
        return new V1DeploymentSpec()
        {
            Replicas = replicas,
            RevisionHistoryLimit = revisionHistoryLimit,
            Selector = new V1LabelSelector() { MatchLabels = Constants.GetKubeDefalutLabels() },
            Template = v1PodTemplateSpec
        };
    }

    private V1PodTemplateSpec GetV1PodTemplateSpec(string name, IDictionary<string, string> keyValuePairs, IList<V1Container> containers, IList<V1Container> initContainers)
    {
        return new()
        {
            Metadata = GetV1ObjectMeta("", keyValuePairs),
            
            Spec = new V1PodSpec()
            {
                Containers = containers,
                InitContainers = initContainers,
                //ImagePullSecrets = 
            }
        };
    }

    private V1Container GetV1Container()
    {
        return new V1Container()
        {
            //Name = 
        };
    }





}