using Luck.Framework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Luck.Walnut.Kube.Persistence;

public class MigrationModule: AppModule
{

    public override void ApplicationInitialization(ApplicationContext context)
    {

        //drop schema "luck.walnut.kube" cascade;
        var moduleDbContext = context.ServiceProvider.GetService<WalnutKubeDbContext>();
        moduleDbContext?.Database.EnsureCreated();
    }

}