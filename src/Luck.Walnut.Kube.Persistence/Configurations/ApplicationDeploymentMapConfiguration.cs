using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class ApplicationDeploymentMapConfiguration : IEntityTypeConfiguration<ApplicationDeployment>
{
    public void Configure(EntityTypeBuilder<ApplicationDeployment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(o => o.ApplicationContainers)
            .WithOne()
            .HasForeignKey(x => x.ApplicationDeploymentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.AppId, "appid_unique_index")
                .IsUnique();
        builder.ToTable("application_deployment");
    }
}