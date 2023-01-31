using Luck.Walnut.Kube.Domain.AggregateRoots.Clusters;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class ClusterMapConfiguration : IEntityTypeConfiguration<Cluster>
{
    public void Configure(EntityTypeBuilder<Cluster> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("kubernetes_clusters");
    }
}