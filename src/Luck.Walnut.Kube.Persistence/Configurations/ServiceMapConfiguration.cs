using Luck.Walnut.Kube.Domain.AggregateRoots.Services;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class ServiceMapConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ServicePorts).HasJsonConversion().HasColumnName("service_ports");
        builder.ToTable("services");
    }
}