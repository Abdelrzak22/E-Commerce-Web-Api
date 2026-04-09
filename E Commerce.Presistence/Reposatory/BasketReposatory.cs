using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModules;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Presistence.Reposatory
{
    public class BasketReposatory : IBasketReposatory
    {
        private readonly IDatabase _database;

        public BasketReposatory(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket basket, TimeSpan time = default)
        {
            var basketjson = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated =await _database.StringSetAsync(basket.Id, basketjson, (time == default) ? TimeSpan.FromDays(7) : time);
            if(IsCreatedOrUpdated)
            {
                var Basket =await _database.StringGetAsync(basket.Id);
                return JsonSerializer.Deserialize<CustomerBasket>(Basket !);  


            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteBasket(string basketId)=>await _database.KeyDeleteAsync(basketId);
        

        public async Task<CustomerBasket?> GetBasket(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            if (basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(basket !);
            
        }
    }
}
