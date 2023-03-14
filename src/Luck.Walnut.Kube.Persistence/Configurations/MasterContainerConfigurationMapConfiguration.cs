using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class MasterContainerConfigurationMapConfiguration : IEntityTypeConfiguration<MasterContainerConfiguration>
{
    public void Configure(EntityTypeBuilder<MasterContainerConfiguration> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.ReadinessProbe).HasJsonConversion();
        builder.Property(e => e.LiveNessProbe).HasJsonConversion();
        builder.Property(e => e.Limits).HasJsonConversion();
        builder.Property(e => e.Requests).HasJsonConversion();
        builder.Property(e => e.Environments).HasJsonConversion();
        builder.Property(e => e.ContainerPortConfigurations).HasJsonConversion();
        builder.ToTable("master_container_configurations");
    }
}
