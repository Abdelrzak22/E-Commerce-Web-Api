using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModules;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DTOS.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketReposatory _basketReposatory;
        private readonly IMapper _mapper;

        public BasketService(IBasketReposatory basketReposatory,IMapper mapper)
        {
            _basketReposatory = basketReposatory;
            _mapper = mapper;
        }
        public async Task<BasketDto> CreateOrUpdateAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket >(basket);
            var CreateOrUpdate= await _basketReposatory.CreateOrUpdateBasket(CustomerBasket);
            return _mapper.Map<CustomerBasket,BasketDto>(CreateOrUpdate !);

        }

        public async Task<bool> DeleteAsync(string id) => await _basketReposatory.DeleteBasket(id);
        

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var basket= await _basketReposatory.GetBasket(id);
            return _mapper.Map<CustomerBasket, BasketDto>(basket!);
        }
    }
}
