namespace Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

public class KubernetesNamespace:KubernetesResourceBase
{
    public KubernetesNamespace(string name) : base(name)
    {
    }
}