using E_Commerce.Domain.Entities.ProductModules;
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
        public ProductWithTypeAndBrandSpecifications() : base(null)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }
    }
}
