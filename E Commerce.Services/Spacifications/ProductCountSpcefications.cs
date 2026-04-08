using E_Commerce.Domain.Entities.ProductModules;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Spacifications
{
    internal class ProductCountSpcefications : BaseSpacifications<Product, int>
    {
        public ProductCountSpcefications(ProductQueryParams queryParams) : base(p => (queryParams.BrandId == null || p.BrandId == queryParams.BrandId) && (queryParams.TypeId == null || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
        }
    }
}
