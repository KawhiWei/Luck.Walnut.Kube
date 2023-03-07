namespace Luck.Walnut.Kube.Dto.ApplicationDeployments;

public class ContainerSurviveConfigurationDto
{
    /// <summary>
    /// 
    /// </summary>
    public string Scheme { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string Path { get; set; } = default!;

    /// <summary>
    /// 端口
    /// </summary>
    public uint Port { get; set; } = default!;

    /// <summary>
    /// 端口
    /// </summary>
    public uint InitialDelaySeconds { get; set; } = default!;

    /// <summary>
    /// 端口
    /// </summary>
    public uint PeriodSeconds { get; set; } = default!;
}