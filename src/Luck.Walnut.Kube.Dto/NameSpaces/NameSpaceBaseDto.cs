using Luck.Walnut.Kube.Domain.Shared.Enums;

namespace Luck.Walnut.Kube.Dto.NameSpaces;

public class NameSpaceBaseDto
{
    /// <summary>
    /// 中文名称
    /// </summary>
    public string ChineseName { get; set; } = default!;

    /// <summary>
    /// 明明空间名称
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// 集群Id
    /// </summary>
    public string ClusterId { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public OnlineStatusEnum OnlineStatus { get;  set; }
}