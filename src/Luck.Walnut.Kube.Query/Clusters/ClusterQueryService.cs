using Luck.Framework.Exceptions;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.Clusteries;

namespace Luck.Walnut.Kube.Query.Clusters;

public class ClusterQueryService : IClusterQueryService
{
    private readonly IClusterRepository _clusterRepository;

    public ClusterQueryService(IClusterRepository clusterRepository)
    {
        _clusterRepository = clusterRepository;
    }

    public Task<List<ClusterOutputDto>> GetClusterOutputDtoListAsync()
    {
        return _clusterRepository.GetClusterOutputDtoListAsync();
    }

    public async Task<ClusterOutputDto> GetClusterFindByIdAsync(string id)
    {
        var cluster = await CheckClusterIsExistAsync(id);
        return new ClusterOutputDto()
        {
            Id = cluster.Id,
            Name = cluster.Name,
            Config = cluster.Config
        };
    }

    public async Task<PageBaseResult<ClusterOutputDto>> GetClusterPageListAsync(ClusterQueryDto query)
    {
        var (data, totalCount) = await _clusterRepository.GetClusterPageListAsync(query);
        var result = data.Select(x => new ClusterOutputDto
        {
            Name = x.Name,
            Config = x.Config,
        });
        return new PageBaseResult<ClusterOutputDto>(totalCount, result.ToArray());
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