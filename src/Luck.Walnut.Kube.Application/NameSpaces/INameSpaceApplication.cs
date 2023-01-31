namespace Luck.Walnut.Kube.Application.NameSpaces;

public interface INameSpaceApplication:IScopedDependency
{
    Task CreateNameSpaceAsync();
}