using Luck.Framework.Exceptions;
using Luck.Framework.UnitOfWorks;
using Luck.Walnut.Kube.Adapter.KubernetesAdapter.NameSpaces;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Domain.Shared.Enums;
using Luck.Walnut.Kube.Dto.Clusteries;
using Luck.Walnut.Kube.Dto.NameSpaces;
using Luck.Walnut.Kube.Query.Clusters;

namespace Luck.Walnut.Kube.Application.NameSpaces;

public class NameSpaceApplication : INameSpaceApplication
{
    private readonly INameSpaceRepository _nameSpaceRepository;
    private readonly IUnitOfWork _unitOfWork;


    private readonly IClusterQueryService _clusterQueryService;
    private readonly INameSpaceAdaper _nameSpaceAdaper;

    public NameSpaceApplication(INameSpaceRepository nameSpaceRepository, IUnitOfWork unitOfWork, IClusterQueryService clusterQueryService, INameSpaceAdaper nameSpaceAdaper)
    {
        _nameSpaceRepository = nameSpaceRepository;
        _unitOfWork = unitOfWork;
        _clusterQueryService = clusterQueryService;
        _nameSpaceAdaper = nameSpaceAdaper;
    }


    /// <summary>
    /// 创建命名空间
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task CreateNameSpaceAsync(NameSpaceInputDto input)
    {
        if (await CheckIsExitNameSpaceNameAsync(input.Name, input.ClusterId))
        {
            throw new BusinessException($"[{input.Name}]已存在，请刷新页面");
        }

        var nameSpace = new NameSpace(input.ChineseName, input.Name, input.ClusterId);
        _nameSpaceRepository.Add(nameSpace);
        await _unitOfWork.CommitAsync();
    }


    /// <summary>
    /// 修改命名空间
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>

    public async Task UpdateNameSpaceAsync(string id, NameSpaceInputDto input)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        nameSpace.Update(input).SetOnline(OnlineStatusEnum.Offline);
        await _unitOfWork.CommitAsync();
    }


    /// <summary>
    /// 上线命名空间
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task OnlineNameSpaceAsync(string id)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        var cluster = await _clusterQueryService.GetClusterFindByIdAsync(nameSpace.ClusterId);
        await _nameSpaceAdaper.CreateNameSpaceAsync(CreateKubernetesNameSpacePublishContext(nameSpace, cluster));
        nameSpace.SetOnline(OnlineStatusEnum.Online);
        await _unitOfWork.CommitAsync();
    }


    /// <summary>
    /// 下线命名空间
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task OffLineNameSpaceAsync(string id)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        var cluster = await _clusterQueryService.GetClusterFindByIdAsync(nameSpace.ClusterId);
        await _nameSpaceAdaper.DeleteNameSpaceAsync(CreateKubernetesNameSpacePublishContext(nameSpace, cluster));
        nameSpace.SetOnline(OnlineStatusEnum.Offline);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 删除命名空间
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// 检查命名空间名称是否存在
    /// </summary>
    /// <param name="name"></param>
    /// <param name="clusterId"></param>
    /// <returns></returns>
    private async Task<bool> CheckIsExitNameSpaceNameAsync(string name, string clusterId)
    {
        var nameSpace = await _nameSpaceRepository.FindNameSpaceByNameAndClusterIdAsync(name, clusterId);
        return nameSpace is not null;
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="nameSpace"></param>
    /// <param name="cluster"></param>
    /// <returns></returns>
    private KubernetesNameSpacePublishContext CreateKubernetesNameSpacePublishContext(NameSpace nameSpace, ClusterOutputDto cluster)
    {
        return new KubernetesNameSpacePublishContext(cluster.Config, nameSpace);
    }

}