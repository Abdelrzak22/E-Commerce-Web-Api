using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModules;
using E_Commerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Presistence.Data.DataSeed
{
    public class Dataintializer : IDataintializer
    {
        private readonly StoreDbcontext _dbcontext;

        public Dataintializer(StoreDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Intializeasync()
        {
            
            try
            {
                var HasProsdut= await _dbcontext.Products.AnyAsync();
                var HasBrand= await _dbcontext.ProductBrands.AnyAsync();
                var HasType=await _dbcontext.ProductTypes.AnyAsync();
                if (HasBrand && HasType && HasProsdut) return;
                if(!HasBrand)
                {
                  await  SeedDataFromJson<ProductBrand, int>("brands.json", _dbcontext.ProductBrands);

                }
                if (!HasType)
                {
                 await   SeedDataFromJson<ProductType, int>("types.json", _dbcontext.ProductTypes);
                }
                if (!HasProsdut)
                {
                 await   SeedDataFromJson<Product, int>("products.json", _dbcontext.Products);

                }
               await _dbcontext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
               
            }
        }

        private async Task SeedDataFromJson<T,Tkey>(string filename,DbSet<T> dbset) where T :BaseEntity<Tkey>
        {

            var filePath = @"..\E Commerce.Presistence\Data\DataSeed\JsonFiles\" + filename;
      
            if (!File.Exists(filePath)) return;
            try
            {
                using var datastreem = File.OpenRead(filePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(datastreem, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if(data is not null)
                {
                  await  dbset.AddRangeAsync(data);
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }  
        }
    }
}
