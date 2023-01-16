using Luck.Framework.Infrastructure;
using Luck.Walnut.Kube.Persistence;

namespace Luck.Walnut.Kube.Api.AppModules;


[DependsOn(
    typeof(DependencyAppModule),
    typeof(EntityFrameworkCoreModule),
    typeof(MigrationModule)
)]
public class AppWebModule: AppModule
{
    public override void ConfigureServices(ConfigureServicesContext context)
    {
        base.ConfigureServices(context);
            
    }
}