using k8s.Models;
using Luck.Framework.Exceptions;
using Luck.Framework.UnitOfWorks;
using Luck.Walnut.Kube.Adapter.Factories;
using Luck.Walnut.Kube.Adapter.KubernetesAdapter.NameSpaces;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Domain.Shared.Enums;
using Luck.Walnut.Kube.Dto.NameSpaces;
using Luck.Walnut.Kube.Infrastructure;
using Luck.Walnut.Kube.Query.Clusters;

namespace Luck.Walnut.Kube.Application.NameSpaces;

public class NameSpaceApplication : INameSpaceApplication
{
    private readonly INameSpaceRepository _nameSpaceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IKubernetesClientFactory _kubernetesClientFactory;
    private readonly IClusterQueryService _clusterQueryService;
    private readonly INameSpaceAdaper _nameSpaceAdaper;

    public NameSpaceApplication(INameSpaceRepository nameSpaceRepository, IUnitOfWork unitOfWork, IKubernetesClientFactory kubernetesClientFactory, IClusterQueryService clusterQueryService, INameSpaceAdaper nameSpaceAdaper)
    {
        _nameSpaceRepository = nameSpaceRepository;
        _unitOfWork = unitOfWork;
        _kubernetesClientFactory = kubernetesClientFactory;
        _clusterQueryService = clusterQueryService;
        _nameSpaceAdaper = nameSpaceAdaper;
    }

    public async Task CreateNameSpaceAsync(NameSpaceInputDto input)
    {
        if (await CheckIsExitNameSpaceAsync(input.Name, input.ClusterId))
        {
            throw new BusinessException($"[{input.Name}]已存在，请刷新页面");
        }

        var nameSpace = new NameSpace(input.ChineseName, input.Name, input.ClusterId);
        _nameSpaceRepository.Add(nameSpace);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateNameSpaceAsync(string id, NameSpaceInputDto input)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        nameSpace.Update(input).SetOnline(OnlineStatusEnum.Offline);
        await _unitOfWork.CommitAsync();
    }

    public async Task OnlineNameSpaceAsync(string id)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        var cluster=await  _clusterQueryService.GetClusterFindByIdAsync(nameSpace.ClusterId);
        var client=  _kubernetesClientFactory.GetKubernetesClient(cluster.Config);
        await _nameSpaceAdaper.CreateNameSpaceAsync(client, nameSpace);
        nameSpace.SetOnline(OnlineStatusEnum.Online);
        await _unitOfWork.CommitAsync();
    }

    public async Task OnffNameSpaceAsync(string id)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        var cluster = await _clusterQueryService.GetClusterFindByIdAsync(nameSpace.ClusterId);
        var client = _kubernetesClientFactory.GetKubernetesClient(cluster.Config);
        await _nameSpaceAdaper.DeleteNameSpaceAsync(client, nameSpace.Name);
        nameSpace.SetOnline(OnlineStatusEnum.Offline);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteNameSpaceAsync(string id)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        _nameSpaceRepository.Remove(nameSpace);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 根据一个Id获取一个NameSpace，并检查是否存在
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    private async Task<NameSpace> GetAndCheckNameSpaceAsync(string id)
    {
        var nameSpace = await _nameSpaceRepository.FindNameSpaceByIdAsync(id);
        if (nameSpace is null)
        {
            throw new BusinessException($"NameSpace不存在，请刷新页面");
        }

        return nameSpace;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="clusterId"></param>
    /// <returns></returns>
    private async Task<bool> CheckIsExitNameSpaceAsync(string name, string clusterId)
    {
        var nameSpace = await _nameSpaceRepository.FindNameSpaceByNameAndClusterIdAsync(name, clusterId);
        return nameSpace is not null;
    }

}