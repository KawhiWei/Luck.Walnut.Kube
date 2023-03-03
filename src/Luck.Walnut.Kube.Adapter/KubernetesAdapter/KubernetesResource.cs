using k8s;
using k8s.Models;
using Luck.Framework.Extensions;
using Luck.KubeWalnut.Adapter.Constants;
using Luck.Walnut.Kube.Adapter.Factories;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter;

public class KubernetesResource : IKubernetesResource
{
    private readonly IKubernetesClientFactory _kubernetesClientFactory;

    private const double TransferNumber = 1_073_741_824;

    public KubernetesResource(IKubernetesClientFactory kubernetesClientFactory)
    {
        _kubernetesClientFactory = kubernetesClientFactory;
    }

    public async Task<KubernetesManager> GetDashboardAsync(string config)
    {
        var client = GetClient(config);
        var v1NodeList = await client.CoreV1.ListNodeAsync();
        var nodeMetricsList = await client.GetKubernetesNodesMetricsAsync();
        var v1DaemonSetList = await client.AppsV1.ListDaemonSetForAllNamespacesAsync();
        var v1Pod = await client.CoreV1.ListPodForAllNamespacesAsync();
        var nameSpace = await client.CoreV1.ListNamespaceAsync();

        // client.CoreV1.pod
        
        // client.AppsV1.CreateNamespacedDeployment()

        // var test = new Dictionary<string, ResourceQuantity>();
        // test.Add("cpu",new ResourceQuantity(""));

        //v1DaemonSetList.Items.First().Spec.Template.Spec.InitContainers

        var jobs = await client.BatchV1.ListJobForAllNamespacesAsync();

        var replicaSetList = await client.AppsV1.ListReplicaSetForAllNamespacesAsync();

        var statefulSetList = await client.AppsV1.ListStatefulSetForAllNamespacesAsync();

        var v1DeploymentList = await client.AppsV1.ListDeploymentForAllNamespacesAsync();

        var kubernetesNodes = GetKubernetesNodeList(v1NodeList.Items, nodeMetricsList);
        var kubernetesNodeDaemonSets = GetKubernetesDaemonSetList(v1DaemonSetList.Items);

        var kubernetesPods = GetKubernetesPodList(v1Pod.Items);

        var kubernetesDeployments = GetKubernetesDeploymentListList(v1DeploymentList.Items);


        var kubernetesJobList = jobs.Items.Select(job =>
        {
            var kubernetesJob = new KubernetesJob(job.Metadata.Name);
            return kubernetesJob;
        }).ToList();

        var kubernetesNamespaces = nameSpace.Items.Select(v1Namespace =>
        {
            var kubernetesNamespace = new KubernetesNamespace(v1Namespace.Metadata.Name);
            return kubernetesNamespace;
        }).ToList();


        var kubernetesReplicaSets = replicaSetList.Items.Select(replicaSet =>
        {
            KubernetesReplicaSet kubernetesReplicaSet = new(replicaSet.Metadata.Name);
            return kubernetesReplicaSet;
        }).ToList();


        var kubernetesStatefulSets = statefulSetList.Items.Select(statefulSet =>
        {
            KubernetesStatefulSet kubernetesStatefulSet = new(statefulSet.Metadata.Name);
            return kubernetesStatefulSet;
        }).ToList();

        var kubernetesManager = new KubernetesManager(kubernetesNodes, kubernetesNodeDaemonSets, kubernetesPods,
            kubernetesJobList, kubernetesNamespaces, kubernetesReplicaSets, kubernetesStatefulSets, kubernetesDeployments);

        return kubernetesManager;
    }

    public async Task GetNameSpaceListAsync(string config)
    {
        var client = GetClient(config);


        var result = await client.CoreV1.CreateNamespaceAsync(new V1Namespace()
        {
        });
        var nameSpace = await client.CoreV1.ListNamespaceAsync();
        foreach (var item in nameSpace.Items)
        {
            Console.WriteLine(item.Metadata.Name);
        }
    }

    public async Task<object> GetPodListAsync(string config)
    {
        var client = GetClient(config);

        await Task.CompletedTask;
        return "";
    }

    public async Task<object> GetPodListAsync(string config, string nameSpace)
    {
        await Task.CompletedTask;
        return "";
    }

    public Task CreateDeploymentAsync(V1Deployment v1Deployment)
    {

        throw new NotImplementedException();
    }


    #region 私有方法

    private List<KubernetesPod> GetKubernetesPodList(IList<V1Pod> v1Pods)
    {
        return v1Pods.Select(v1Pod =>
        {
            var kubernetesPod = new KubernetesPod(v1Pod.Metadata.Name, v1Pod.Metadata.NamespaceProperty,
                v1Pod.Metadata.GenerateName, v1Pod.Spec.NodeName, v1Pod.Status.PodIP, v1Pod.Spec.RestartPolicy,
                v1Pod.Status.Phase,
                v1Pod.Spec.SchedulerName, v1Pod.Status.StartTime);
            return kubernetesPod;
        }).ToList();
    }


    private List<KubernetesNode> GetKubernetesNodeList(IList<V1Node> v1Nodes, NodeMetricsList nodeMetricsList)
    {
        return v1Nodes.Select(v1Node =>
        {
            var capacityResource = CreateResource(v1Node.Status.Capacity) ??
                                   new Resource(0, 0, 0);

            var allocatableResource = CreateResource(v1Node.Status.Allocatable) ??
                                      new Resource(0, 0, 0);

            var usageResource = new Resource(0, 0, 0);
            var metric =
                nodeMetricsList.Items.FirstOrDefault(nodeMetrics => nodeMetrics.Metadata.Name == v1Node.Metadata.Name);
            if (metric is not null)
            {
                usageResource = CreateResource(metric.Usage) ??
                                new Resource(0, 0, 0);
            }

            var ipAddresses =
                v1Node.Status.Addresses.Select(x => new IpAddress(x.Type, x.Address)).ToList();

            var kubernetesNode = new KubernetesNode(v1Node.Metadata.Name,
                v1Node.Status.NodeInfo.KubeProxyVersion, v1Node.Status.NodeInfo.OsImage,
                v1Node.Status.NodeInfo.OperatingSystem, v1Node.Status.NodeInfo.ContainerRuntimeVersion, ipAddresses,
                capacityResource, allocatableResource, usageResource);


            return kubernetesNode;
        }).ToList();
    }

    private List<KubernetesDaemonSet> GetKubernetesDaemonSetList(IList<V1DaemonSet> v1DaemonSets)
    {
        return v1DaemonSets.Select(v1DaemonSet =>
        {
            var kubernetesDaemonSet =
                new KubernetesDaemonSet(v1DaemonSet.Metadata.Name, v1DaemonSet.Status.CurrentNumberScheduled,
                    v1DaemonSet.Status.DesiredNumberScheduled, v1DaemonSet.Status.NumberAvailable ?? 0,
                    v1DaemonSet.Status.NumberReady);
            return kubernetesDaemonSet;
        }).ToList();
    }


    private List<KubernetesDeployment> GetKubernetesDeploymentListList(IList<V1Deployment> v1Deployments)
    {
        return v1Deployments.Select(v1Deployment =>
        {
            // v1Deployment.Spec.Template.Spec.Containers.ForEach(a=>a.Resources.Limits)

            var kubernetesDaemonSet =
                new KubernetesDeployment(v1Deployment.Metadata.Name, v1Deployment.Status.Replicas, v1Deployment.Status.ReadyReplicas, v1Deployment.Status.AvailableReplicas);
            return kubernetesDaemonSet;
        }).ToList();
    }

    private IKubernetes GetClient(string config)
    {
        return _kubernetesClientFactory.GetKubernetesClient(config);
    }

    private ResourceQuantity? GetResourceQuantity(string key, IDictionary<string, ResourceQuantity> allocatable)
    {
        if (allocatable.TryGetValue(key, out var resourceQuantity))
        {
            return resourceQuantity;
        }

        return null;
    }


    private Resource? CreateResource(
        IDictionary<string, ResourceQuantity> resourceQuantities)
    {
        var cpuResourceQuantity = GetResourceQuantity(KubernetesConstants.Cpu, resourceQuantities);
        var memoryResourceQuantity = GetResourceQuantity(KubernetesConstants.Memory, resourceQuantities);
        var podResourceQuantity = GetResourceQuantity(KubernetesConstants.Pod, resourceQuantities);

        var cpu = cpuResourceQuantity == null ? 0 : Math.Round(cpuResourceQuantity.ToDouble() * 100) / 100;
        var memory = memoryResourceQuantity == null
            ? 0
            : Math.Round(memoryResourceQuantity.ToDouble() / TransferNumber * 100) / 100;

        var pod = podResourceQuantity?.ToInt32() ?? 0;
        return new Resource(cpu, memory, pod);
    }

    #endregion
}