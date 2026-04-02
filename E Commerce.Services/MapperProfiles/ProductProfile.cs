using AutoMapper;
using E_Commerce.Domain.Entities.ProductModules;
using E_Commerce.Shared.DTOS.Productdtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MapperProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile() {


            CreateMap<ProductBrand, BrandDtos>();

            CreateMap<Product, ProductDtos>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name));

        
        }
    }
}
