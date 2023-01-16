namespace Luck.Walnut.Kube.Domain.AggregateRoots.Kubernetes;

public class IpAddress
{
    public IpAddress(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public string Name { get; set; }

    public string Address { get; set; }
}