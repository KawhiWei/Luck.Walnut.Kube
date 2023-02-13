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
        if (await CheckIsExitNameSpaceAsync(input.Name))
        {
            throw new BusinessException($"[{input.Name}]已存在，请刷新页面");
        }

        var nameSpace = new NameSpace(input.ChineseName, input.Name, false);
        _nameSpaceRepository.Add(nameSpace);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateNameSpaceAsync(string id, NameSpaceInputDto input)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
        nameSpace.Update(input.ChineseName, input.Name);
        await _unitOfWork.CommitAsync();
    }

    public async Task PublishNameSpaceAsync(string id)
    {
        var nameSpace = await GetAndCheckNameSpaceAsync(id);
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
        var nameSpace = await _nameSpaceRepository.FIndNameSpaceByIdAsync(id);
        if (nameSpace is null)
        {
            throw new BusinessException($"NameSpace不存在，请刷新页面");
        }

        return nameSpace;
    }

    private async Task<bool> CheckIsExitNameSpaceAsync(string name)
    {
        var nameSpace = await _nameSpaceRepository.FIndNameSpaceByNameAsync(name);
        if (nameSpace is null)
        {
            return false;
        }

        return true;
    }
}