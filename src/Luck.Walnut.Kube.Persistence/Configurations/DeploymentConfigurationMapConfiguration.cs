using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class DeploymentConfigurationMapConfiguration : IEntityTypeConfiguration<DeploymentConfiguration>
{
    public void Configure(EntityTypeBuilder<DeploymentConfiguration> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(o => o.MasterContainers)
            .WithOne()
            .HasForeignKey(x => x.DeploymentId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(e => e.InitContainers).HasJsonConversion();
        builder.Property(e => e.Strategy).HasJsonConversion();
        builder.HasIndex(x => x.AppId, "appid_unique_index");
        builder.ToTable("deployment_configurations");
    }
}