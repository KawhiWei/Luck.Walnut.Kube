namespace Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

public class KubernetesDeployment : KubernetesResourceBase
{
    public KubernetesDeployment(string name, int? replicas, int? readyReplicas, int? availableReplicas) : base(name)
    {
        Replicas = replicas;
        ReadyReplicas = readyReplicas;
        AvailableReplicas = availableReplicas;
    }

    /// <summary>
    /// 副本
    /// </summary>
    public int? Replicas { get; private set; }  

    /// <summary>
    /// 准备好了副本
    /// </summary>
    public int? ReadyReplicas { get; private set; }  
    
    /// <summary>
    /// 可用副本
    /// </summary>
    public int? AvailableReplicas { get; private set; } 
    
}