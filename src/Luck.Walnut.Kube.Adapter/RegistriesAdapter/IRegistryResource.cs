using Luck.Walnut.Kube.Domain.Shared.Enums;

namespace Luck.Walnut.Kube.Adapter.RegistriesAdapter;

public interface IRegistryResource
{
    RegistryTypeEnum RegistryType { get; }
}