namespace Luck.Walnut.Kube.Dto.ApplicationDeployments;

/// <summary>
/// 资源配置
/// </summary>
public class ContainerResourceQuantityDto
{
    /// <summary>
    /// /
    /// </summary>
    public string Limit { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string Request { get; set; } = default!;
}