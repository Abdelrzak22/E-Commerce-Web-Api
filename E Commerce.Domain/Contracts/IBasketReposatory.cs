using E_Commerce.Domain.Entities.BasketModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketReposatory
    {
        Task<CustomerBasket?> GetBasket(string basketId);
        Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket basket,TimeSpan time=default);
        Task<bool> DeleteBasket(string basketId);
    }
}
