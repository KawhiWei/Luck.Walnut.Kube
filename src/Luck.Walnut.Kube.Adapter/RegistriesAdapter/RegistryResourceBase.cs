using Luck.Walnut.Kube.Domain.Shared.Enums;

namespace Luck.Walnut.Kube.Adapter.RegistriesAdapter;

public abstract class RegistryResourceBase : IRegistryResource
{
    public virtual RegistryTypeEnum RegistryType => RegistryTypeEnum.None;


    public abstract Task<object> GetRepoListAsync();
    
    
    public abstract Task<object> CreateRepoAsync();
    
    
    public abstract Task<object> UpdateRepoAsync();

}