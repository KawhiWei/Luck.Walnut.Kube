using Luck.Framework.Exceptions;
using Luck.Framework.UnitOfWorks;
using Luck.Walnut.Kube.Domain.AggregateRoots.Services;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.Services;

namespace Luck.Walnut.Kube.Application.Services;

public class ServiceApplication : IServiceApplication
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ServiceApplication(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateServiceAsync(ServiceInputDto input)
    {
        if (await CheckIsExitNameSpaceAsync(input.Name, input.NameSpaceId))
        {
            throw new BusinessException($"[{input.Name}]已存在，请刷新页面");
        }

        var service = new Service(input.Name, input.DeploymentId, input.NameSpaceId, input.ClusterId, input.AppId);
        _serviceRepository.Add(service);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateServiceAsync(string id, ServiceInputDto input)
    {
        var service = await GetAndCheckServiceAsync(id);
        service.SetIsPublish(false)
            .Update(input)
            .SetServicePorts(input);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteServiceAsync(string id)
    {
        var service = await GetAndCheckServiceAsync(id);
        service.RemoveCheck();
        _serviceRepository.Remove(service);
        await _unitOfWork.CommitAsync();
    }

    public async Task PublishServiceAsync(string id)
    {
        var service = await GetAndCheckServiceAsync(id);
        // Todo 对接K8sApi
        service.SetIsPublish(true);

        await _unitOfWork.CommitAsync();
    }

    public async Task OffPublishServiceAsync(string id)
    {
        var service = await GetAndCheckServiceAsync(id);
        // Todo 对接K8sApi
        service.SetIsPublish(false);
        await _unitOfWork.CommitAsync();
    }

    private async Task<bool> CheckIsExitNameSpaceAsync(string name, string nameSpaceId)
    {
        var nameSpace = await _serviceRepository.FindServiceByNameAndNameSpaceIdAsync(name, nameSpaceId);
        return nameSpace is not null;
    }

    private async Task<Service> GetAndCheckServiceAsync(string id)
    {
        var nameSpace = await _serviceRepository.FindServiceByIdAsync(id);
        if (nameSpace is null)
        {
            throw new BusinessException($"Service不存在，请刷新页面");
        }

        return nameSpace;
    }
}