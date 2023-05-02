using Luck.Walnut.Kube.Application.SideCarPlugins;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.SideCarPlugins;
using Luck.Walnut.Kube.Query.SideCarPlugins;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers
{
    [Route("api/init/containers")]
    public class SideCarPluginController : BaseController
    {
        /// <summary>
        /// 创建默认容器
        /// </summary>
        /// <param name="initContainerConfigurationApplication"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task CreateSideCarPlugin([FromServices] ISideCarPluginApplication sideCarPluginApplication, [FromBody] SideCarPluginInputDto input) =>
            sideCarPluginApplication.CreateSideCarPluginAsync(input);


        /// <summary>
        /// 修改默认容器配置
        /// </summary>
        /// <param name="initContainerConfigurationApplication"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task UpdateSideCarPlugin([FromServices] ISideCarPluginApplication sideCarPluginApplication, string id, [FromBody] SideCarPluginInputDto input) =>
            sideCarPluginApplication.UpdateSideCarPluginAsync(id, input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initContainerConfigurationApplication"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task DeleteSideCarPlugin([FromServices] ISideCarPluginApplication sideCarPluginApplication, string id) =>
            sideCarPluginApplication.DeleteSideCarPluginAsync(id);


        /// <summary>
        /// 根据Id获取一个容器配置
        /// </summary>
        /// <param name="initContainerConfigurationQueryService"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<SideCarPluginOutputDto?> GetSideCarPlugin([FromServices] ISideCarPluginQueryService sideCarPluginQueryService, string id) =>
            sideCarPluginQueryService.GetSideCarPluginAsync(id);


        /// <summary>
        /// 获取初始容器列表
        /// </summary>
        /// <param name="initContainerConfigurationQueryService"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public Task<List<SideCarPluginOutputDto>> GetSideCarPluginList([FromServices] ISideCarPluginQueryService sideCarPluginQueryService) =>
            sideCarPluginQueryService.GetSideCarPluginListAsync();

        /// <summary>
        /// 分页获取初始容器列表
        /// </summary>
        /// <param name="sideCarPluginQueryService"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("page/list")]
        public Task<PageBaseResult<SideCarPluginOutputDto>> GetSideCarPluginPageList([FromServices] ISideCarPluginQueryService sideCarPluginQueryService, [FromQuery] SideCarPluginQueryDto query) =>
            sideCarPluginQueryService.GetSideCarPluginPageListAsync(query);
    }
}