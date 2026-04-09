using AutoMapper;
using E_Commerce.Domain.Entities.BasketModules;
using E_Commerce.Shared.DTOS.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MapperProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile() {
        
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItems, BasketItemDto>().ReverseMap();
        
        
        }
    }

}
