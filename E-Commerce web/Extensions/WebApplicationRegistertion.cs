using E_Commerce.Domain.Contracts;
using E_Commerce.Presistence.Data.DbContexts;
using E_Commerce.Presistence.Data.IdentityDbcontext;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_web.Extensions
{
    public static class WebApplicationRegistertion
    {

        public async static Task< WebApplication> MigrateDatabase(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbcontextService = scope.ServiceProvider.GetRequiredService<StoreDbcontext>();
            var pendingMigrations = await dbcontextService.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
               await dbcontextService.Database.MigrateAsync();
            return app;
        }
        public async static Task<WebApplication> MigrateIdentityDatabase(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbcontextService = scope.ServiceProvider.GetRequiredService<StoreIdentityDbcontext>();
            var pendingMigrations = await dbcontextService.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await dbcontextService.Database.MigrateAsync();
            return app;
        }

        public async static Task< WebApplication> SeedDatabase(this WebApplication app)
        {
          await  using var scope = app.Services.CreateAsyncScope();
            var Data = scope.ServiceProvider.GetRequiredService<IDataintializer>();
          await  Data.Intializeasync();
            return app; 
        }
    }
}
