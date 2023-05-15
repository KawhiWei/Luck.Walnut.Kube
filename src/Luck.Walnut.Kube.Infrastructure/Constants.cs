namespace Luck.Walnut.Kube.Infrastructure
{
    public class Constants
    {
        public static IDictionary<string, string> GetKubeDefalutLabels()
        {
            var dic = new Dictionary<string, string>
            {
                //{ "toyar-paas", "true" },
                { "app.kubernetes.io/created-by", "toyar-paas" },
            };
            return dic;
        }
    }
}
