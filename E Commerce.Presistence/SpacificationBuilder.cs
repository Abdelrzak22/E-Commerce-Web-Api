using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presistence
{
    public static class SpacificationBuilder
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,Tkey>(IQueryable<TEntity> EntryPoint,ISpecifications<TEntity,Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var query = EntryPoint;
            if(specifications is not null)
            {

                if(specifications.Ceriateria is not null)
                {
                    query = query.Where(specifications.Ceriateria);
                }
                if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    foreach(var ex in specifications.IncludeExpressions)
                    {
                        query = query.Include(ex);
                    }
                }
            }

            return query;
        }
    }
}
