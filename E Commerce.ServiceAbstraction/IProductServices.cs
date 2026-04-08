using E_Commerce.Shared.DTOS.Productdtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ServiceAbstraction
{
    public interface IProductServices
    {

        Task< IEnumerable<ProductDtos>> GetAllProductAsync(int? brandid, int? typeid);

        Task<ProductDtos> GetProductById(int id);

        Task<IEnumerable<BrandDtos>> GetAllBrandAsync();
        Task<IEnumerable<TypeDtos>> GetAllTypeAsync();
    }
}
