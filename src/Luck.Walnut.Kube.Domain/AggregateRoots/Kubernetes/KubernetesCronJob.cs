namespace Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

public class KubernetesCronJob:KubernetesResourceBase
{
    public KubernetesCronJob(string name) : base(name)
    {
    }
}