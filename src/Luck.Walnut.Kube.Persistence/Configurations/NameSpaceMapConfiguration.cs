using Luck.Walnut.Kube.Domain.AggregateRoots.NameSpaces;

namespace Luck.Walnut.Kube.Persistence.Configurations;

public class NameSpaceMapConfiguration : IEntityTypeConfiguration<NameSpace>
{
    public void Configure(EntityTypeBuilder<NameSpace> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("namespaces");
    }
}