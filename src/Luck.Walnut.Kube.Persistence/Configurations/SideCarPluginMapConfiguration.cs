using Luck.Walnut.Kube.Domain.AggregateRoots.SideCarPlugins;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class SideCarPluginMapConfiguration : IEntityTypeConfiguration<SideCarPlugin>
{
    public void Configure(EntityTypeBuilder<SideCarPlugin> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.ReadinessProbe).HasJsonConversion();
        builder.Property(e => e.LiveNessProbe).HasJsonConversion();
        builder.Property(e => e.Limits).HasJsonConversion();
        builder.Property(e => e.Requests).HasJsonConversion();
        builder.Property(e => e.Environments).HasJsonConversion();
        builder.Property(e => e.ContainerPortConfigurations).HasJsonConversion();
        builder.ToTable("sidecar_plugins");
    }
}