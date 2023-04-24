using k8s.Models;

using Luck.Framework.Exceptions;
using Luck.Framework.UnitOfWorks;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.NameSpaces;

namespace Luck.Walnut.Kube.Application.NameSpaces;

public class NameSpaceApplication : INameSpaceApplication
{
    private readonly INameSpaceRepository _nameSpaceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NameSpaceApplication(INameSpaceRepository nameSpaceRepository, IUnitOfWork unitOfWork)
    {
        _nameSpaceRepository = nameSpaceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateNameSpaceAsync(NameSpaceInputDto input)
    {
        if (await CheckIsExitNameSpaceAsync(input.Name,input.ClusterId))
        {
            throw new BusinessException($"[{input.Name}]已存在，请刷新页面");
        }

        var nameSpace = new NameSpace(input.ChineseName, input.Name, false,input.ClusterId);
        _nameSpaceRepository.Add(nameSpace);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateNameSpaceAsync(string id, NameSpaceInputDto input)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        nameSpace.Update(input)
            .SetIsPublish(false);
        await _unitOfWork.CommitAsync();
    }

    public async Task PublishNameSpaceAsync(string id)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);

        var KubernetesNameSpace=new V1Namespace()
        {
            Metadata=new V1ObjectMeta()
            {
                Name=nameSpace.Name,
            }
        }



        nameSpace.SetIsPublish(true);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteNameSpaceAsync(string id)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        _nameSpaceRepository.Remove(nameSpace);
        await _unitOfWork.CommitAsync();
    }

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
    private async Task<bool> CheckIsExitNameSpaceAsync(string name,string clusterId)
    {
        var nameSpace = await _nameSpaceRepository.FindNameSpaceByNameAndClusterIdAsync(name,clusterId);
        return nameSpace is not null;
    }
}