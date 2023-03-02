namespace Luck.Walnut.Kube.Dto.ApplicationDeployments;

/// <summary>
/// 资源配置
/// </summary>
public class ContainerResourceQuantityDto
{
    /// <summary>
    /// /
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string Memory { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string Cpu { get; set; } = default!;
}