using Luck.Walnut.Kube.Dto.NameSpaces;

namespace Luck.Walnut.Kube.Application.NameSpaces;

public interface INameSpaceApplication:IScopedDependency
{
    Task CreateNameSpaceAsync(NameSpaceInputDto input);
}