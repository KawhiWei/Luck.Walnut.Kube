using MediatR;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.Services;

/// <summary>
/// 服务配置
/// </summary>
public class Service : FullAggregateRoot
{
    public Service(string name, string applicationDeploymentId, bool isPublish)
    {
        Name = name;
        ApplicationDeploymentId = applicationDeploymentId;
        IsPublish = isPublish;
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
    /// 是否发布
    /// </summary>
    public bool IsPublish { get; private set; } = default!;

    /// <summary>
    /// 服务端口配置
    /// </summary>
    public ICollection<ServicePort> ServicePorts { get; private set; } = new HashSet<ServicePort>();
}
