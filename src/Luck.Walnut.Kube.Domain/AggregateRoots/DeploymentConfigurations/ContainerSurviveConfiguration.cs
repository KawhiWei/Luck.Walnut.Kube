using System.Text.Json.Serialization;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;

/// <summary>
/// 探针配置
/// </summary>
public class ContainerSurviveConfiguration
{
    [JsonConstructor]//这个特性 可以写私有，标识你要用哪个构造函数
    public ContainerSurviveConfiguration(string? scheme, string? path, uint? port, uint? initialDelaySeconds, uint? periodSeconds)
    {
        Scheme = scheme;
        Path = path;
        Port = port;
        InitialDelaySeconds = initialDelaySeconds;
        PeriodSeconds = periodSeconds;
    }

    /// <summary>
    /// 
    /// </summary>
    public string? Scheme { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Path { get; private set; }

    /// <summary>
    /// 端口
    /// </summary>
    public uint? Port { get; private set; }

    /// <summary>
    /// 端口
    /// </summary>
    public uint? InitialDelaySeconds { get; private set; }

    /// <summary>
    /// 端口
    /// </summary>
    public uint? PeriodSeconds { get; private set; }
}