namespace Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

public class KubernetesResourceBase
{
    public KubernetesResourceBase(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}