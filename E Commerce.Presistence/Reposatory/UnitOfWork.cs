using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Presistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presistence.Reposatory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbcontext _storeDbcontext;
        private readonly Dictionary<Type, object> _Reposatory = [];

        public UnitOfWork(StoreDbcontext storeDbcontext)
        {
            _storeDbcontext = storeDbcontext;
        }

        public IGenaricReposatory<IEntity, Tkey> GenericRepo<IEntity, Tkey>() where IEntity : BaseEntity<Tkey>
        {
            var EntityType= typeof(IEntity);
            if (_Reposatory.TryGetValue(EntityType, out object? repo)) 
                return (IGenaricReposatory<IEntity,Tkey >) repo;

            var newRepo = new GenaricReposatory<IEntity, Tkey>(_storeDbcontext);
            _Reposatory[EntityType] = newRepo;
            return newRepo;
        }

        public async Task<int> SaveChangeAsync()=>   await  _storeDbcontext.SaveChangesAsync();
        
    }
}
