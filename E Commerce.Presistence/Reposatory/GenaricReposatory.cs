using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presistence.Reposatory
{
    public class GenaricReposatory<IEntity, Tkey> : IGenaricReposatory<IEntity, Tkey> where IEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbcontext _storeDbcontext;
        

        public GenaricReposatory(StoreDbcontext storeDbcontext)
        {
            _storeDbcontext = storeDbcontext;
        }

        public async Task AddAsync(IEntity entity)=>        await _storeDbcontext.Set<IEntity>().AddAsync(entity);
      

        public void Delete(IEntity entity)=>_storeDbcontext.Set<IEntity>().Remove(entity);
        

        public async Task<IEnumerable<IEntity>> GetAllAsync()=> await _storeDbcontext.Set<IEntity>().ToListAsync();

        public async Task<IEnumerable<IEntity>> GetAllAsync(ISpecifications<IEntity, Tkey> specifications)
        {
            return await SpacificationBuilder.CreateQuery(_storeDbcontext.Set<IEntity>(), specifications).ToListAsync();
        }

        public async Task<IEntity?> GetByIdAsync(int id)=>await _storeDbcontext.Set<IEntity>().FindAsync(id);
        

        public void Update(IEntity entity) =>  _storeDbcontext.Set<IEntity>().Update(entity);
        
    }
}
