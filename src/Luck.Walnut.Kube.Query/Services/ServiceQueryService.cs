using Luck.Walnut.Kube.Domain.AggregateRoots.Services;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.Services;

namespace Luck.Walnut.Kube.Query.Services;

public class ServiceQueryService : IServiceQueryService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly INameSpaceRepository _nameSpaceRepository;
    private readonly IClusterRepository _clusterRepository;

    public ServiceQueryService(IServiceRepository serviceRepository, INameSpaceRepository nameSpaceRepository, IClusterRepository clusterRepository)
    {
        _serviceRepository = serviceRepository;
        _nameSpaceRepository = nameSpaceRepository;
        _clusterRepository = clusterRepository;
    }

    public async Task<PageBaseResult<ServiceOutputDto>> GetServicePageListAsync(ServiceQueryDto query)
    {
        var (data, totalCount) = await _serviceRepository.GetServicePageListAsync(query);
        var nameSpaceIdList = data.Select(x => x.NameSpaceId).ToList();
        var nameSpaceList = await _nameSpaceRepository.GetNameSpaceByIdsListAsync(nameSpaceIdList);
        var clusterList = await _clusterRepository.GetClusterFindByIdListAsync(data.Select(x => x.ClusterId).ToList());
        var result = data.Select(service =>
        {
            var serviceDto = CreateServiceOutputDto(service);
            var nameSpace = nameSpaceList.FirstOrDefault(x => x.Id == service.NameSpaceId);
            if (nameSpace is not null)
            {
                serviceDto.NameSpaceName = nameSpace.Name;
                serviceDto.NameSpaceChineseName = nameSpace.ChineseName;
            }

            var cluster = clusterList.FirstOrDefault(cluster => cluster.Id == service.ClusterId);
            if (cluster is not null)
            {
                serviceDto.ClusterName = cluster.Name;
            }

            return serviceDto;
        }).ToArray();


        return new PageBaseResult<ServiceOutputDto>(totalCount, result);
    }

    public async Task<ServiceOutputDto?> GetServiceByIdAsync(string id)
    {
        var service = await _serviceRepository.FindServiceByIdAsync(id);

        if (service is null)
        {
            return null;
        }

        return CreateServiceOutputDto(service);
    }


    private ServiceOutputDto CreateServiceOutputDto(Service service)
    {
        var serviceDto = new ServiceOutputDto()
        {
            Name = service.Name,
            DeploymentId = service.DeploymentId,
            NameSpaceId = service.NameSpaceId,
            Id = service.Id,
            ClusterId = service.ClusterId,
            IsPublish = service.IsPublish,
        };
        return serviceDto;
    }
}