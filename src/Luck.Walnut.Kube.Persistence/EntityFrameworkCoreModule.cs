using Luck.EntityFrameworkCore;
using Luck.EntityFrameworkCore.DbContextDrivenProvides;
using Microsoft.Extensions.DependencyInjection;

namespace Luck.Walnut.Kube.Persistence;

public class EntityFrameworkCoreModule: EntityFrameworkCoreBaseModule
{
    protected override void AddDbContextWithUnitOfWork(IServiceCollection services)
    {
        services.AddLuckDbContext<WalnutKubeDbContext>(x =>
        {
            x.ConnectionString = "User ID=postgres;Password=wzw0126..;Host=39.101.165.187;Port=8832;Database=luck.walnut.kube";
            x.Type = DataBaseType.PostgreSQL;
        });
    }

    protected override void AddDbDriven(IServiceCollection service)
    {
        service.AddPostgreSQLDriven();
    }
}
