namespace Luck.Walnut.Kube.Infrastructure
{
    public class Constants
    {
        public static IDictionary<string, string> GetKubeDefalutLabels()
        {
            var dic = new Dictionary<string, string>
            {
                { "luck-walnu-kube", "true" }
            };
            return dic;
        }
    }
}
