using Luck.Walnut.Kube.Domain.Shared.Enums;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.Services;

public class ServicePort
{
    /// <summary>
    /// 端口类型
    /// </summary>
    public PortTypeEnum PortType { get; private set; } = default!;

    /// <summary>
    /// 服务名称
    /// </summary>
    public string PortName { get; private set; } = default!;

    /// <summary>
    /// 来源端口号
    /// </summary>
    public uint SourcePort { get; private set; } = default!;

    /// <summary>
    /// 目的端口号
    /// </summary>
    public uint TargetPort { get; private set; } = default!;
}