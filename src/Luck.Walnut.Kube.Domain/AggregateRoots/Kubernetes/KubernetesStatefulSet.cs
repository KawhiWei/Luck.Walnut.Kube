namespace Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

public class KubernetesStatefulSet:KubernetesResourceBase
{
    public KubernetesStatefulSet(string name) : base(name)
    {
    }
}