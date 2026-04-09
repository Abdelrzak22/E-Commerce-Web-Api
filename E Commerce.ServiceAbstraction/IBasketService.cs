using E_Commerce.Shared.DTOS.Basket;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string id);
        Task<BasketDto> CreateOrUpdateAsync(BasketDto basket);
        Task<bool> DeleteAsync(string id);
    }
}
