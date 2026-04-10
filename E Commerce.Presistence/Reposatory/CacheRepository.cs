using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presistence.Reposatory
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _connection;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _connection = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string key)
        {
            var value = await _connection.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string key, string value, TimeSpan timetolive)
        {
            await _connection.StringSetAsync(key, value, timetolive);
        }
    }
}
