using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.AggregateRoots.Services;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations
{
    public class KubernetesDeploymentPublishContext: KubernetesPublishBaseContext
    {
        public KubernetesDeploymentPublishContext(DeploymentConfiguration deploymentConfiguration, string config, NameSpace nameSpace) : base(config, nameSpace)
        {
            DeploymentConfiguration = deploymentConfiguration;

        }

        /// <summary>
        /// 
        /// </summary>
        public DeploymentConfiguration DeploymentConfiguration { get; private set; }
    }
}
