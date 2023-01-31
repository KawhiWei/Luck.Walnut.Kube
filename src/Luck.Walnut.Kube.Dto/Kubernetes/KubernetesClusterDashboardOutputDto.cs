namespace Luck.Walnut.Kube.Dto.Kubernetes;

public class KubernetesClusterDashboardOutputDto
{
    
    /// <summary>
    /// 集群总CPU核心数
    /// </summary>
    public double ClusterTotalCpuCapacity { get; set; }
    
    /// <summary>
    /// 集群Cpu总使用量
    /// </summary>
    public  double ClusterTotalCpuUsage { get; set; }
    
    /// <summary>
    /// 集群总内存
    /// </summary>
    public  double ClusterTotalMemoryCapacity { get; set; }
    
    /// <summary>
    /// 集群内存总使用量
    /// </summary>
    public  double ClusterTotalMemoryUsage { get; set; }

    /// <summary>
    /// 集群可部署pod数量
    /// </summary>
    public  int ClusterTotalPodCapacity { get; set; }

    /// <summary>
    /// 集群已部署pod数量 
    /// </summary>
    public int ClusterTotalPodUsage { get; set; }
    
    public int DaemonSetTotal { get; set; }
    public int DeploymentTotal { get; set; }

    
    
    public int JobTotal { get; set; }
    
    public int NamespaceTotal { get; set; }
    
    public int ReplicaSetTotal { get; set; }
    
    public int StatefulSetTotal { get; set; }
    
    /// <summary>
    /// 节点列表
    /// </summary>
    public List<KubernetesNodeOutputDto> Nodes { get; set; } = new();
}