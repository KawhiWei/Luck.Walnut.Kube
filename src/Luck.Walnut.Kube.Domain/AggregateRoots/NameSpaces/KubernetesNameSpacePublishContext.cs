using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces
{
    public class KubernetesNameSpacePublishContext : KubernetesPublishBaseContext
    {
        public KubernetesNameSpacePublishContext(string configString, NameSpace nameSpace) : base(configString, nameSpace)
        {
        }
    }
}
