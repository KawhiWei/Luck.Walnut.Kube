using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.AggregateRoots.Services;

namespace Luck.Walnut.Kube.Domain.AggregateRoots
{
    /// <summary>
    /// 应用部署发布基础传输上下文
    /// </summary>
    public abstract class KubernetesPublishBaseContext
    {
        public KubernetesPublishBaseContext(string config)
        {
            ConfigString = config;
        }

        public KubernetesPublishBaseContext(string configString, NameSpace nameSpace)
        {
            ConfigString = configString;
            NameSpace = nameSpace;
        }


        /// <summary>
        /// 
        /// </summary>
        public NameSpace NameSpace { get; private set; } = default!;
        /// <summary>
        /// 集群连接配置
        /// </summary>

        public string ConfigString { get; private set; }

    }
}
