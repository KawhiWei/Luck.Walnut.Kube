namespace Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

/// <summary>
/// 资源配置
/// </summary>
public class ContainerResourceQuantity
{
    /// <summary>
    /// /
    /// </summary>
    public string Limit { get; private set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string Request { get; private set; } = default!;
}