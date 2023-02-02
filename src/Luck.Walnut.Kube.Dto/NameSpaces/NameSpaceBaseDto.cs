namespace Luck.Walnut.Kube.Dto.NameSpaces;

public class NameSpaceBaseDto
{
    /// <summary>
    /// 中文名称
    /// </summary>
    public string ChineseName { get;  set; } = default!;

    /// <summary>
    /// 明明空间名称
    /// </summary>
    public string Name { get;  set; } = default!;
}

public class NameSpaceInputDto:NameSpaceBaseDto
{
}