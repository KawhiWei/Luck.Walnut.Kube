namespace Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;

/// <summary>
/// 命名空间
/// </summary>
public class NameSpace : FullAggregateRoot
{
    public NameSpace(string chineseName, string name, bool isPublish, string clusterId)
    {
        ChineseName = chineseName;
        Name = name;
        IsPublish = isPublish;
        ClusterId = clusterId;
    }

    /// <summary>
    /// 中文名称
    /// </summary>
    public string ChineseName { get; private set; }

    /// <summary>
    /// 明明空间名称
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// 集群Id
    /// </summary>
    public string ClusterId{ get; private set; } 

    /// <summary>
    /// 是否发布
    /// </summary>
    public bool IsPublish { get; private set; }

    public NameSpace Update(string chineseName, string name)
    {
        ChineseName = chineseName;
        Name = name;
        return this;
    }

    public NameSpace SetIsPublish(bool isPublish)
    {
        IsPublish = isPublish;
        return this;
    }
}