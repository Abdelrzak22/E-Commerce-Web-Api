using E_Commerce.Domain.Contracts;
using E_Commerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_web.Extensions
{
    public static class WebApplicationRegistertion
    {

        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbcontextService = scope.ServiceProvider.GetRequiredService<StoreDbcontext>();
            if (dbcontextService.Database.GetPendingMigrations().Any())
                dbcontextService.Database.Migrate();
            return app;
        }

        public static WebApplication SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var Data = scope.ServiceProvider.GetRequiredService<IDataintializer>();
            Data.Intialize();
            return app; 
        }
    }
}
