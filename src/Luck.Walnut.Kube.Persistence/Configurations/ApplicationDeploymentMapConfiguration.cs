using Luck.Walnut.Kube.Domain.AggregateRoots.ApplicationDeployments;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class ApplicationDeploymentMapConfiguration : IEntityTypeConfiguration<ApplicationDeployment>
{
    public void Configure(EntityTypeBuilder<ApplicationDeployment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("application_deployment");
    }
}