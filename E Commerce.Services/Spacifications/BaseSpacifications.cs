using AutoMapper.Configuration.Conventions;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Spacifications
{
    internal abstract class BaseSpacifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {


        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Ceriateria { get; }

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity,object>> expression)
        {
            OrderBy = expression;
        }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }









        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void ApplyPagination (int PageSize,int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }













        protected void AddOrderByDescending(Expression<Func<TEntity,object>> expression)
        {
            OrderByDescending = expression;
        }


        public BaseSpacifications(Expression<Func<TEntity,bool>> value)
        {
            Ceriateria = value;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> Includeexp)
        {
            IncludeExpressions.Add(Includeexp);
        }
    }


}
