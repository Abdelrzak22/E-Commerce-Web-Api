
using E_Commerce.Domain.Contracts;
using E_Commerce.Presistence.Data.DataSeed;
using E_Commerce.Presistence.Data.DbContexts;
using E_Commerce.Presistence.Data.IdentityDbcontext;
using E_Commerce.Presistence.Reposatory;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Services;
using E_Commerce.Services.MapperProfiles;
using E_Commerce_web.CustomMiddleWare;
using E_Commerce_web.Extensions;
using E_Commerce_web.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace E_Commerce_web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbcontext>(options =>
            {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

        });
            builder.Services.AddDbContext<StoreIdentityDbcontext>(options =>
            {
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

        });
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddAutoMapper(x => x.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(x => x.AddProfile<BasketProfile>());
            builder.Services.AddTransient<ProductPictureUrl>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection") !);


            });
            builder.Services.AddScoped<IBasketReposatory, BasketReposatory>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();

            builder.Services.AddScoped<IDataintializer,Dataintializer>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResoponeFactoryy.GenerateApiValidation;
                
            });

            var app = builder.Build();

            #region seedDATA and Migrate

            await app.MigrateDatabase();
            await app.MigrateIdentityDatabase();
            await   app.SeedDatabase();


            #endregion

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionHandlerMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
