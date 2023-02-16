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
        var cluster = new Cluster(input.Name, @"apiVersion: v1
clusters:
- cluster:
    certificate-authority-data: LS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tCk1JSUREekNDQWZlZ0F3SUJBZ0lVSHJqaENXVlRzdE1iWTZ6RlNqYTk2RHYwai9Fd0RRWUpLb1pJaHZjTkFRRUwKQlFBd0Z6RVZNQk1HQTFVRUF3d01NVEF1TVRVeUxqRTRNeTR4TUI0WERUSXlNRGN5TmpFeU5EWTBORm9YRFRNeQpNRGN5TXpFeU5EWTBORm93RnpFVk1CTUdBMVVFQXd3TU1UQXVNVFV5TGpFNE15NHhNSUlCSWpBTkJna3Foa2lHCjl3MEJBUUVGQUFPQ0FROEFNSUlCQ2dLQ0FRRUF3ak92dWpzeWdzTVZQQk0vOHptZ0x4Uzc1UXVOemMrWTlzL2gKenJOcTBxSStnZjBNdEN0OGZIRlM2WWpiVGxIYmxOMGxUVlE5QjlJT1FXSCsyRUcwY3N4U3l4bThsUkJ4cHQ5cAp5am1NdE14eVl2RDU2TEdwb05zOGNZa2NEaC9La3ZuYS9qLzh0ZEdFb3VXZEczcmlHWGhyU0tmV0pTbVRxOVlXCiswL3NHME05bTlCeldQdmZ4ejRvWXVHK25IcWxrY2EzZ1VRMStkTzdabzE1MkhodWQ1QlBkMjB3Ym51TWFPM0kKaDA5U2ZGMHRFczROazQ1RVlzZDlEZ3h3NDV4SWVmOHNLRUZDOElIQ1BsZTVEQ1VxNkE0elR3MWVNQnpUaElHbAo0b2xUTU8xd3BkTEVIZ2tRV0dTWGtKUGt5OFdQRXRlNm5ONGoyc292Wis2VkFRZ3ZjUUlEQVFBQm8xTXdVVEFkCkJnTlZIUTRFRmdRVVNzNWhnZ1dna1Q1UEdKZzhQZUM2WmkydExpNHdId1lEVlIwakJCZ3dGb0FVU3M1aGdnV2cKa1Q1UEdKZzhQZUM2WmkydExpNHdEd1lEVlIwVEFRSC9CQVV3QXdFQi96QU5CZ2txaGtpRzl3MEJBUXNGQUFPQwpBUUVBUmQ5VEppTDFKYXBreTN0QUVqYkt0WDZ3ZjN5dFF0dzV4cTIrME1TcnhZejl0OVEvbk0rL2dJUE5xZmFyCjltbkNkUGxCczg1SzU3Ym1lTHRwNXRpcWxjcHgyOTJidlc2aHc4dGx2ME5zTG1kN1VCYXZQSUswVUN5TG4xTjEKaGp0cVY3S0VpYlBCajlZenF4QWc0Z05ianFLWTRlL2VKRGJWTUhWTzMrQkVpRzhLZXE4QUdTdGhSY1FpWHltbQpKVXZENElBWFZqWFA2VWY4S1ZYd2tManRaU29BaXR0ZENmKytpV3RUUHI2SmZzaUlCeCt3Q2g2cnlVV0pCQTZvCnh2aVBYWGczT2wvMGl3akd2Q25DTUtqVXJSeTRDVXJLOUdwbm1USENSYW5zQ21EdmNkenZLTnREclltU3c3eU0KUkdUM3dUdDJWNEVqdXJMc25OUXlzcjI1SlE9PQotLS0tLUVORCBDRVJUSUZJQ0FURS0tLS0tCg==
    server: https://47.100.213.49:8325
  name: microk8s-cluster
contexts:
- context:
    cluster: microk8s-cluster
    user: admin
  name: microk8s
current-context: microk8s
kind: Config
preferences: {}
users:
- name: admin
  user:
    token: allQK2FaT3JyeGorM3VRMUJEOVpFeHBmbWtPR1BheTBxbkdGR2w0R1Vudz0K", "");
        _clusterRepository.Add(cluster);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateClusterAsync(string id, ClusterInputDto input)
    {
        var cluster = await CheckClusterIsExistAsync(id);
        cluster.SetName(input.Name).SetConfig(input.Config);
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


    private async Task<Cluster> CheckClusterIsExistAsync(string id)
    {
        var cluster = await _clusterRepository.GetClusterFindByIdAsync(id);
        if (cluster is null)
        {
            throw new BusinessException("集群不存在，请刷新页面");
        }

        return cluster;
    }
}