using Luck.Framework.Exceptions;
using Luck.Framework.UnitOfWorks;
using Luck.Walnut.Kube.Adapter.KubernetesAdapter;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.Clusteries;
using Luck.Walnut.Kube.Dto.Kubernetes;


namespace Luck.Walnut.Kube.Application.Clusters;

public class ClusterApplication : IClusterApplication
{
    private readonly IKubernetesResource _kubernetesResource;
    private readonly IClusterRepository _clusterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClusterApplication(IKubernetesResource kubernetesResource, IClusterRepository clusterRepository,
        IUnitOfWork unitOfWork)
    {
        _kubernetesResource = kubernetesResource;
        _clusterRepository = clusterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateClusterAsync(ClusterInputDto input)
    {
        var cluster = new Cluster(input.Name,input.Config, "");
        _clusterRepository.Add(cluster);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateClusterAsync(string id, ClusterInputDto input)
    {
        var cluster = await CheckAndGetClusterIsExistAsync(id);
        cluster.SetName(input.Name).SetConfig(input.Config);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteClusterAsync(string id )
    {
        var cluster = await CheckAndGetClusterIsExistAsync(id);
        _clusterRepository.Remove(cluster);
        await _unitOfWork.CommitAsync();
    }


    public async Task<KubernetesClusterDashboardOutputDto> GetClusterDashboardAsync(string id)
    {
        var cluster = await _clusterRepository.FindAll().FirstOrDefaultAsync(x => x.Id == id);
        if (cluster is null)
            throw new BusinessException("集群不存在");
        var kubernetes = await _kubernetesResource.GetDashboardAsync(cluster.Config);
        return GetKubernetesClusterOutputDto(kubernetes);
    }

    private KubernetesClusterDashboardOutputDto GetKubernetesClusterOutputDto(KubernetesManager kubernetesManager)
    {
        var kubernetesClusterOutputDto = new KubernetesClusterDashboardOutputDto()
        {
            ClusterTotalCpuCapacity = kubernetesManager.GetClusterTotalCpuCapacity(),
            ClusterTotalCpuUsage = kubernetesManager.GetClusterTotalCpuUsage(),
            ClusterTotalMemoryCapacity = kubernetesManager.GetClusterTotalMemoryCapacity(),
            ClusterTotalMemoryUsage = kubernetesManager.GetClusterTotalMemoryUsage(),
            Nodes = GetKubernetesNodeOutputDtos(kubernetesManager.KubernetesNodes),
            ClusterTotalPodCapacity = kubernetesManager.GetClusterTotalPodCapacity(),
            DaemonSetTotal = kubernetesManager.KubernetesNodeDaemonSets.Count,
            DeploymentTotal = kubernetesManager.KubernetesDeployments.Count,
            ClusterTotalPodUsage = kubernetesManager.KubernetesPods.Count,
            JobTotal = kubernetesManager.KubernetesJobs.Count,
            StatefulSetTotal = kubernetesManager.KubernetesStatefulSets.Count,
            NamespaceTotal = kubernetesManager.KubernetesNamespaces.Count,
            ReplicaSetTotal = kubernetesManager.KubernetesReplicaSets.Count,
        };
        return kubernetesClusterOutputDto;
    }

    private List<KubernetesNodeOutputDto> GetKubernetesNodeOutputDtos(List<KubernetesNode> kubernetesNodes)
    {
        return kubernetesNodes.Select(x => new KubernetesNodeOutputDto()
        {
            Name = x.Name,
            KubernetesVersion = x.KubernetesVersion,
            OsImage = x.OsImage,
            OperatingSystem = x.OperatingSystem,
            ContainerRuntimeVersion = x.ContainerRuntimeVersion,
            CapacityResource = new ResourceDto()
            {
                Cpu = x.CapacityResource.Cpu,
                Memory = x.CapacityResource.Memory,
                Pod = x.CapacityResource.Pod,
            },
            AllocatableResource = new ResourceDto()
            {
                Cpu = x.AllocatableResource.Cpu,
                Memory = x.AllocatableResource.Memory,
                Pod = x.AllocatableResource.Pod,
            },
            UsageResource = new ResourceDto()
            {
                Cpu = x.UsageResource.Cpu,
                Memory = x.UsageResource.Memory,
                Pod = x.UsageResource.Pod,
            },
            IpAddresses = x.IpAddresses.Select(a => new IpAddressesOutputDto() { Address = a.Address, Name = a.Name })
                .ToList(),
        }).ToList();
    }
    
    private async Task<Cluster> CheckAndGetClusterIsExistAsync(string id)
    {
        var cluster = await _clusterRepository.GetClusterFindByIdAsync(id);
        if (cluster is null)
        {
            throw new BusinessException("集群不存在，请刷新页面");
        }

        return cluster;
    }
}