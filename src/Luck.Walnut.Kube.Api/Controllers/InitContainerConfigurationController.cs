using Luck.Walnut.Kube.Application.InitContainers;
using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.InitContainerConfigurations;
using Luck.Walnut.Kube.Query.InitContainers;
using Microsoft.AspNetCore.Mvc;

namespace Luck.Walnut.Kube.Api.Controllers
{
    [Route("api/init/containers")]
    public class InitContainerConfigurationController : BaseController
    {
        /// <summary>
        /// 创建默认容器
        /// </summary>
        /// <param name="initContainerConfigurationApplication"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task CreateInitContainerConfiguration([FromServices] IInitContainerConfigurationApplication initContainerConfigurationApplication, [FromBody] InitContainerConfigurationInputDto input) =>
            initContainerConfigurationApplication.CreateInitContainerConfigurationAsync(input);


        /// <summary>
        /// 修改默认容器配置
        /// </summary>
        /// <param name="initContainerConfigurationApplication"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task UpdateInitContainerConfiguration([FromServices] IInitContainerConfigurationApplication initContainerConfigurationApplication, string id, [FromBody] InitContainerConfigurationInputDto input) =>
            initContainerConfigurationApplication.UpdateInitContainerConfigurationAsync(id, input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initContainerConfigurationApplication"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task DeleteInitContainerConfiguration([FromServices] IInitContainerConfigurationApplication initContainerConfigurationApplication, string id) =>
            initContainerConfigurationApplication.DeleteInitContainerConfigurationAsync(id);


        /// <summary>
        /// 根据Id获取一个容器配置
        /// </summary>
        /// <param name="initContainerConfigurationQueryService"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<InitContainerConfigurationOutputDto?> GetInitContainerConfiguration([FromServices] IInitContainerConfigurationQueryService initContainerConfigurationQueryService, string id) =>
            initContainerConfigurationQueryService.GetInitContainerConfigurationAsync(id);


        /// <summary>
        /// 获取初始容器列表
        /// </summary>
        /// <param name="initContainerConfigurationQueryService"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public Task<List<InitContainerConfigurationOutputDto>> GetInitContainerConfigurationList([FromServices] IInitContainerConfigurationQueryService initContainerConfigurationQueryService) =>
            initContainerConfigurationQueryService.GetInitContainerConfigurationListAsync();

        /// <summary>
        /// 分页获取初始容器列表
        /// </summary>
        /// <param name="initContainerConfigurationQueryService"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("page/list")]
        public Task<PageBaseResult<InitContainerConfigurationOutputDto>> GetInitContainerConfigurationPageList([FromServices] IInitContainerConfigurationQueryService initContainerConfigurationQueryService, [FromQuery] InitContainerConfigurationQueryDto query) =>
            initContainerConfigurationQueryService.GetInitContainerConfigurationPageListAsync(query);
    }
}