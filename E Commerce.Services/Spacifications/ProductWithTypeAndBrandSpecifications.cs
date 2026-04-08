using E_Commerce.Domain.Entities.ProductModules;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Spacifications
{
    internal class ProductWithTypeAndBrandSpecifications:BaseSpacifications<Product,int>
    {
        public ProductWithTypeAndBrandSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }
        public ProductWithTypeAndBrandSpecifications(ProductQueryParams queryParams) :
            base(p => (queryParams.BrandId == null || p.BrandId == queryParams.BrandId) && (queryParams.TypeId == null || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))

        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch(queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
                    
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
    }
}
