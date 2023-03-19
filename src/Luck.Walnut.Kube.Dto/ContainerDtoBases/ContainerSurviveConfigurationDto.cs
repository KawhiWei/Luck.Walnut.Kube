namespace Luck.Walnut.Kube.Dto.ContainerDtoBases;

public class ContainerSurviveConfigurationDto
{
    /// <summary>
    /// 
    /// </summary>
    public string? Scheme { get; set; } 

    /// <summary>
    /// 
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 端口
    /// </summary>
    public uint? Port { get; set; } 

    /// <summary>
    /// 端口
    /// </summary>
    public uint? InitialDelaySeconds { get; set; } 

    /// <summary>
    /// 端口
    /// </summary>
    public uint? PeriodSeconds { get; set; }
}