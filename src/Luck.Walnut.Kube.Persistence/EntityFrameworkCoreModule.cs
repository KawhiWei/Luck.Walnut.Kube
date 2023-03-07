using Luck.EntityFrameworkCore;
using Luck.EntityFrameworkCore.DbContextDrivenProvides;
using Microsoft.Extensions.DependencyInjection;

namespace Luck.Walnut.Kube.Persistence;

public class EntityFrameworkCoreModule: EntityFrameworkCoreBaseModule
{
    public override void AddDbContextWithUnitOfWork(IServiceCollection services)
    {
        services.AddLuckDbContext<KubeWalnutDbContext>(x =>
        {
            x.ConnnectionString = "User ID=postgres;Password=wzw0126..;Host=39.101.165.187;Port=8832;Database=luck.walnut.kube";
            x.Type = DataBaseType.PostgreSQL;
        });
    }

    public override void AddDbDriven(IServiceCollection service)
    {
        service.AddPostgreSQLDriven();
    }
}
