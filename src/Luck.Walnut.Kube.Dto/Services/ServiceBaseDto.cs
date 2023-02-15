namespace Luck.Walnut.Kube.Dto.Services;

public class ServiceBaseDto
{
    /// <summary>
    /// 服务名称
    /// </summary>
    public string Name { get;  set; } = default!;

    /// <summary>
    /// 部署配置Id
    /// </summary>
    public string ApplicationDeploymentId { get;  set; }= default!;
    
    /// <summary>
    /// 命名空间Id
    /// </summary>
    public string NameSpaceId { get;  set; }= default!;
    
    /// <summary>
    /// 服务端口配置
    /// </summary>
    public List<ServicePortInputDto>? ServicePorts { get; set; } = default!;
}