namespace Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;

/// <summary>
/// 命名空间
/// </summary>
public class NameSpace: FullAggregateRoot
{
    /// <summary>
    /// 中文名称
    /// </summary>
    public string ChineseName { get; private set; } = default!;

    /// <summary>
    /// 明明空间名称
    /// </summary>
    public string Name { get; private set; } = default!;
}