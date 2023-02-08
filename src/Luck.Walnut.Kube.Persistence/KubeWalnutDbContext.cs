
using System.Reflection;
using Luck.EntityFrameworkCore.DbContexts;
using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;
using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;
using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;

namespace Luck.Walnut.Kube.Persistence;

public class KubeWalnutDbContext: LuckDbContextBase
{
    public KubeWalnutDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
    {
    }
    
    public DbSet<Cluster> Clusters => Set<Cluster>();

    public DbSet<ApplicationContainer> ApplicationContainers => Set<ApplicationContainer>();

    public DbSet<ApplicationDeployment> ApplicationDeployments => Set<ApplicationDeployment>();

    public DbSet<NameSpace> NameSpaces => Set<NameSpace>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("luck.walnut.kube");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}