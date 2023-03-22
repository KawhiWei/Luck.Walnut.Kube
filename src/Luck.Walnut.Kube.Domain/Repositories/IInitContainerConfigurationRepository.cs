﻿using Luck.DDD.Domain.Repositories;
using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luck.Walnut.Kube.Dto.InitContainerConfigurations;

namespace Luck.Walnut.Kube.Domain.Repositories
{
    public interface IInitContainerConfigurationRepository : IAggregateRootRepository<InitContainerConfiguration, string>, IScopedDependency
    {

        /// <summary>
        /// 分页查询默认容器列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<(InitContainerConfiguration[] Data, int TotalCount)> GetInitContainerConfigurationPageListAsync(InitContainerConfigurationQueryDto query);
        
        /// <summary>
        /// 根据Name获取一个默认容器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<InitContainerConfiguration?> FindInitContainerConfigurationByNameAsync(string name);


        /// <summary>
        /// 根据Id查询一个默认容器配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InitContainerConfiguration?> FindInitContainerConfigurationByIdAsync(string id);
    }
}