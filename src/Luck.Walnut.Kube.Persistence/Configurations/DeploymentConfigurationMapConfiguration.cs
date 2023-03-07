using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class DeploymentConfigurationMapConfiguration : IEntityTypeConfiguration<DeploymentConfiguration>
{
    public void Configure(EntityTypeBuilder<DeploymentConfiguration> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(o => o.ApplicationContainers)
            .WithOne()
            .HasForeignKey(x => x.ApplicationDeploymentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.AppId, "appid_unique_index");
        builder.ToTable("deployment_configurations");
    }
}