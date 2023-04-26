using k8s;
using k8s.Models;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Infrastructure;
using System.Text.Json;
using System.Xml.Linq;


namespace Luck.Walnut.Kube.Adapter.KubernetesAdapter.NameSpaces
{
    public class NameSpaceAdaper : INameSpaceAdaper
    {

        public async Task CreateNameSpaceAsync(IKubernetes kubernetes, NameSpace nameSpace)
        {


            await kubernetes.CoreV1.CreateNamespaceAsync(GeV1Namespace(nameSpace));
        }

        public async Task UpdateNameSpaceAsync(IKubernetes kubernetes, V1Namespace v1Namespace)
        {
            await kubernetes.CoreV1.CreateNamespaceAsync(v1Namespace);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="kubernetes"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task UpdateNameSpaceAsync(IKubernetes kubernetes, NameSpace nameSpace)
        {
            var v1Namespace = await kubernetes.CoreV1.ReadNamespaceAsync(nameSpace.Name);
            await kubernetes.CoreV1.PatchNamespaceAsync(GetPatchNameSpaceV1Namespace(nameSpace, v1Namespace), nameSpace.Name);
        }



        public async Task DeleteNameSpaceAsync(IKubernetes kubernetes, string name)
        {
            await kubernetes.CoreV1.DeleteNamespaceAsync(name);
        }

        /// <summary>
        /// 转换为K8s对象
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        private V1Namespace GeV1Namespace(NameSpace nameSpace)
        {
            var labels = Constants.GetKubeDefalutLabels();
            return new V1Namespace()
            {
                Metadata = new V1ObjectMeta()
                {
                    Name = nameSpace.Name,
                    Labels = labels
                }
            };

        }



        /// <summary>
        /// 转换为K8s对象
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        private V1Patch GetPatchNameSpaceV1Namespace(NameSpace nameSpace,V1Namespace oldV1Namespace)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
            
            var old = JsonSerializer.SerializeToDocument(oldV1Namespace, options);
            var expected = JsonSerializer.SerializeToDocument(oldV1Namespace);
            //var patch = old.CreatePatch(expected);

            return new V1Patch(expected, V1Patch.PatchType.JsonPatch);
            //var daemonSet = await client.AppsV1.ReadNamespacedDaemonSetAsync(name, @namespace);
            //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
            //var old = JsonSerializer.SerializeToDocument(daemonSet, options);
            //var now = DateTimeOffset.Now.ToUnixTimeSeconds();
            //var restart = new Dictionary<string, string>
            //{
            //    ["date"] = now.ToString()
            //};

            //daemonSet.Spec.Template.Metadata.Annotations = restart;

            //var expected = JsonSerializer.SerializeToDocument(daemonSet);

            //var patch = old.CreatePatch(expected);
        }
    }
}
