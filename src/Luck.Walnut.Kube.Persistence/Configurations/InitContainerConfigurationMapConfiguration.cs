using Luck.Walnut.Kube.Domain.AggregateRoots.InitContainerConfigurations;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class InitContainerConfigurationMapConfiguration : IEntityTypeConfiguration<InitContainerConfiguration>
{
    public void Configure(EntityTypeBuilder<InitContainerConfiguration> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.ReadinessProbe).HasJsonConversion();
        builder.Property(e => e.LiveNessProbe).HasJsonConversion();
        builder.Property(e => e.Limits).HasJsonConversion();
        builder.Property(e => e.Requests).HasJsonConversion();
        builder.Property(e => e.Environments).HasJsonConversion();
        builder.Property(e => e.ContainerPortConfigurations).HasJsonConversion();
        builder.ToTable("init_containers");
    }
}