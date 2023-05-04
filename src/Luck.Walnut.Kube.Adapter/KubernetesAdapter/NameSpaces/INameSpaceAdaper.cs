using k8s;
using k8s.Models;

using Luck.Framework.Infrastructure.DependencyInjectionModule;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;

namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter.NameSpaces
{
    public interface INameSpaceAdaper : IScopedDependency
    {
        /// <summary>
        /// 创建NameSpace
        /// </summary>
        /// <param name="kubernetes"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task CreateNameSpaceAsync(KubernetesNameSpacePublishContext kubernetesNameSpacePublishContext);


        /// <summary>
        /// 删除NameSpace
        /// </summary>
        /// <param name="kubernetes"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task DeleteNameSpaceAsync(KubernetesNameSpacePublishContext kubernetesNameSpacePublishContext);

        /// <summary>
        /// 修改NameSpace
        /// </summary>
        /// <param name="kubernetes"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task UpdateNameSpaceAsync(KubernetesNameSpacePublishContext kubernetesNameSpacePublishContext);

    }
}
