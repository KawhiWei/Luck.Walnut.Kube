
using System.Reflection;
using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;
using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;
using Luck.Walnut.Kube.Domain.AggregateRoots.Services;

namespace Luck.Walnut.Kube.Persistence;

public class KubeWalnutDbContext: LuckDbContextBase
{
    public KubeWalnutDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
    {
    }
    
    public DbSet<Cluster> Clusters => Set<Cluster>();

    public DbSet<MasterContainerConfiguration> ApplicationContainers => Set<MasterContainerConfiguration>();

    public DbSet<DeploymentConfiguration> ApplicationDeployments => Set<DeploymentConfiguration>();

    public DbSet<InitContainerConfiguration> InitContainerConfigurations => Set<InitContainerConfiguration>();

    public DbSet<NameSpace> NameSpaces => Set<NameSpace>();
    
    public DbSet<Service> Services => Set<Service>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("luck.walnut.kube");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}