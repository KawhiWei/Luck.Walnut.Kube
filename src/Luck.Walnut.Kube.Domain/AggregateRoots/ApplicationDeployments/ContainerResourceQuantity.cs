namespace Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

/// <summary>
/// 资源配置
/// </summary>
public class ContainerResourceQuantity
{
    public ContainerResourceQuantity(string name, string memory, string cpu)
    {
        Name = name;
        Memory = memory;
        Cpu = cpu;
    }

    /// <summary>
    /// /
    /// </summary>
    public string Name { get; private set; } 

    /// <summary>
    /// 
    /// </summary>
    public string Memory { get; private set; } 
    /// <summary>
    /// 
    /// </summary>
    public string Cpu { get; private set; }
}