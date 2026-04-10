using E_Commerce.Domain.Contracts;
using E_Commerce.ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CacheService:ICacheService
    {
        private readonly ICacheRepository _cache;

        public CacheService(ICacheRepository cache)
        {
            _cache = cache;
        }
        public async Task<string?> GetAsync(string key)
        {
            return await _cache.GetAsync(key);
        }

        public async Task SetAsync(string key, object value, TimeSpan timetolive)
        {

            var values = JsonSerializer.Serialize(value, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _cache.SetAsync(key, values, timetolive);
        }

        
    }
}
