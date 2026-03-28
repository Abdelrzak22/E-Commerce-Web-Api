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
        public void Intialize()
        {
            
            try
            {
                var HasProsdut=_dbcontext.Products.Any();
                var HasBrand= _dbcontext.ProductBrands.Any();
                var HasType=_dbcontext.ProductTypes.Any();
                if (HasBrand & HasType & HasProsdut) return;
                if(!HasBrand)
                {
                    SeedDataFromJson<ProductBrand, int>("types.json", _dbcontext.ProductBrands);

                }
                if (!HasType)
                {
                    SeedDataFromJson<ProductType, int>("types.json", _dbcontext.ProductTypes);
                }
                if (!HasProsdut)
                {
                    SeedDataFromJson<Product, int>("types.json", _dbcontext.Products);

                }
                _dbcontext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
               
            }
        }

        private void SeedDataFromJson<T,Tkey>(string filename,DbSet<T> dbset) where T :BaseEntity<Tkey>
        {

            var filePath = @"..\E Commerce.Presistence\Data\DataSeed\JsonFiles" + filename;
            if (!File.Exists(filePath)) return;
            try
            {
                using var datastreem = File.OpenRead(filePath);
                var data = JsonSerializer.Deserialize<List<T>>(datastreem, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if(data is not null)
                {
                    dbset.AddRange(data);
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
