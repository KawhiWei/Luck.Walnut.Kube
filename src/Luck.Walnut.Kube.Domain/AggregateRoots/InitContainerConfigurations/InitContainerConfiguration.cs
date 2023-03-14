using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations
{
    /// <summary>
    /// 初始容器配置
    /// </summary>
    public class InitContainerConfiguration : FullAggregateRoot
    {
        public InitContainerConfiguration(string containerName, bool isInitContainer, string? image, string restartPolicy, string imagePullPolicy)
        {
            ContainerName = containerName;
            IsInitContainer = isInitContainer;
            Image = image;
            RestartPolicy = restartPolicy;
            ImagePullPolicy = imagePullPolicy;
        }


        /// <summary>
        /// 容器名称
        /// </summary>
        public string ContainerName { get; private set; }

        /// <summary>
        /// 是否初始容器
        /// </summary>
        public bool IsInitContainer { get; private set; }

        /// <summary>
        /// 镜像名称
        /// </summary>
        public string? Image { get; private set; }


        /// <summary>
        /// 重启策略
        /// </summary>

        public string RestartPolicy { get; private set; }

        /// <summary>
        /// 镜像拉取策略
        /// </summary>

        public string ImagePullPolicy { get; private set; }

        /// <summary>
        /// 准备完成探针配置
        /// </summary>
        public ContainerSurviveConfiguration? ReadinessProbe { get; private set; }

        /// <summary>
        /// 存活探针配置
        /// </summary>
        public ContainerSurviveConfiguration? LiveNessProbe { get; private set; }

        /// <summary>
        /// 容器Cpu资源限制
        /// </summary>
        public ContainerResourceQuantity? Limits { get; private set; }

        /// <summary>
        /// 容器内存资源限制
        /// </summary>
        public ContainerResourceQuantity? Requests { get; private set; }

        /// <summary>
        /// 环境变量
        /// </summary>
        public List<KeyValuePair<string, string>> Environments { get; private set; } = default!;

        /// <summary>
        /// 容器端口配置
        /// </summary>
        public ICollection<ContainerPortConfiguration> ContainerPortConfigurations { get; private set; } = new HashSet<ContainerPortConfiguration>();

    }
}
