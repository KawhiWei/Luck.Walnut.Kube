using Luck.Walnut.Kube.Domain.Shared.Enums;
using MediatR;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.Services;

/// <summary>
/// 服务配置
/// </summary>
public class NameSpace : FullAggregateRoot
{
    public Service(string name, string applicationDeploymentId)
    {
        Name = name;
        ApplicationDeploymentId = applicationDeploymentId;
    }

    /// <summary>
    /// 服务名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 部署配置Id
    /// </summary>
    public string ApplicationDeploymentId { get; private set; }

    /// <summary>
    /// 服务端口配置
    /// </summary>
    public ICollection<ServicePort> ServicePorts { get; private set; } = new HashSet<ServicePort>();
}

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