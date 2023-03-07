
using System.Reflection;
using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;

namespace Luck.Walnut.Kube.Persistence;

public class KubeWalnutDbContext: LuckDbContextBase
{
    public KubeWalnutDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
    {
    }
    
    public DbSet<Cluster> Clusters => Set<Cluster>();

    public DbSet<DeploymentContainerConfiguration> ApplicationContainers => Set<DeploymentContainerConfiguration>();

    public DbSet<DeploymentConfiguration> ApplicationDeployments => Set<DeploymentConfiguration>();

    public DbSet<NameSpace> NameSpaces => Set<NameSpace>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("luck.walnut.kube");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}