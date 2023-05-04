using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.Services;


    /// <summary>
    /// 应用部署发布基础传输上下文
    /// </summary>
    public class KubernetesServicePublishContext: KubernetesPublishBaseContext
    {
        public KubernetesServicePublishContext(Service service, string configString, NameSpace nameSpace):base(configString, nameSpace)
        {
            Service = service;

        }

        /// <summary>
        /// 
        /// </summary>
        public Service Service { get; private set; }
    }

