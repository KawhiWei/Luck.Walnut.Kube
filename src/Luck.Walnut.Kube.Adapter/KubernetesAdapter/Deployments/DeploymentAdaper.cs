using k8s;
using k8s.Models;

using Luck.Framework.Extensions;
using Luck.Walnut.Kube.Adapter.Factories;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Infrastructure;

using RazorEngine.Templating;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter.Deployments;

public class DeploymentAdaper : IDeploymentAdaper
{
    private readonly IKubernetesCommonParamsBuild _kubernetesCommonParamsBuild;

    public DeploymentAdaper(IKubernetesCommonParamsBuild kubernetesCommonParamsBuild)
    {
        _kubernetesCommonParamsBuild = kubernetesCommonParamsBuild;
    }

    public async Task CreateDeploymentAsync(IKubernetes kubernetes, DeploymentConfiguration deployment, List<InitContainerConfiguration> initContainerConfigurations)
    {
        var v1Deployment= GetV1Deployment(deployment, initContainerConfigurations);
        await kubernetes.AppsV1.CreateNamespacedDeploymentAsync(v1Deployment,"");
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

    #region 创建V1Deployment

    /// <summary>
    /// 创建V1Deployment对象
    /// </summary>
    /// <param name="deployment"></param>
    /// <param name="initContainerConfigurations"></param>
    /// <returns></returns>
    private V1Deployment GetV1Deployment(DeploymentConfiguration deployment, List<InitContainerConfiguration> initContainerConfigurations)
    {

        var masterContainers = deployment.MasterContainers.Select(masterContainer => _kubernetesCommonParamsBuild.CreateV1ContainerForMasterContainerConfiguration(masterContainer)).ToList();

        var v1PodSpec = _kubernetesCommonParamsBuild.CreateV1PodSpec(containers: masterContainers);

        var deploymentV1ObjectMeta = _kubernetesCommonParamsBuild.CreateV1ObjectMeta();

        var podTemplateV1ObjectMeta = _kubernetesCommonParamsBuild.CreateV1ObjectMeta();

        var v1PodTemplateSpec= _kubernetesCommonParamsBuild.CreateV1PodTemplateSpec(podTemplateV1ObjectMeta, v1PodSpec);

        var v1DeploymentSpec = CreateV1DeploymentSpec(deployment.Replicas, v1PodTemplateSpec);

        var v1Deployment = new V1Deployment()
        {

            Metadata = deploymentV1ObjectMeta,
            Spec = v1DeploymentSpec,
        };
        return v1Deployment;
    }

    /// <summary>
    /// 创建V1DeploymentSpec
    /// </summary>
    /// <param name="name"></param>
    /// <param name="replicas"></param>
    /// <param name="revisionHistoryLimit"></param>
    /// <param name="v1PodTemplateSpec"></param>
    /// <returns></returns>
    private V1DeploymentSpec CreateV1DeploymentSpec(int replicas, V1PodTemplateSpec v1PodTemplateSpec, V1DeploymentStrategy? v1DeploymentStrategy=null, V1LabelSelector? v1LabelSelector=null)
    {
        return new V1DeploymentSpec()
        {
            Replicas = replicas,
            Strategy= v1DeploymentStrategy,
            Selector = v1LabelSelector,
            Template = v1PodTemplateSpec
        };
    }
    #endregion









}