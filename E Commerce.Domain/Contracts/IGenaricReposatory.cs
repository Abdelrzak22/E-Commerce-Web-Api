using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenaricReposatory<IEntity,Tkey> where IEntity:BaseEntity<Tkey>
    {
        Task<IEnumerable<IEntity>> GetAllAsync();
        Task<IEntity?> GetByIdAsync(int id);
        Task AddAsync(IEntity entity);
        void Update (IEntity entity);
        void Delete (IEntity entity);
    }
}
