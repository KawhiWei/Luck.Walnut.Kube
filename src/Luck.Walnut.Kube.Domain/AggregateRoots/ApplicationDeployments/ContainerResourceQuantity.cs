namespace Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

/// <summary>
/// 资源配置
/// </summary>
public class ContainerResourceQuantity
{

    /// <summary>
    /// /
    /// </summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string Memory { get; private set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string Cpu { get; private set; } = default!;
}