﻿using Luck.Walnut.Kube.Dto;
using Luck.Walnut.Kube.Dto.InitContainerConfigurations;

namespace Luck.Walnut.Kube.Query.InitContainers
{
    public interface IInitContainerConfigurationQueryService:IScopedDependency
    {
        /// <summary>
        /// 根据Id获取一个容器配置 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InitContainerConfigurationOutputDto?> GetInitContainerConfigurationAsync(string id);

        /// <summary>
        /// 获取初始容器列表
        /// </summary>
        Task<List<InitContainerConfigurationOutputDto>> GetInitContainerConfigurationListAsync();
        
        /// <summary>
        /// 获取初始容器列表
        /// </summary>
        Task<PageBaseResult<InitContainerConfigurationOutputDto>> GetInitContainerConfigurationPageListAsync(InitContainerConfigurationQueryDto query);
    }
}