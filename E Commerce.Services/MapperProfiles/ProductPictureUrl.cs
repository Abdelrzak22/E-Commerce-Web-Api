
using AutoMapper;
using E_Commerce.Domain.Entities.ProductModules;
using E_Commerce.Shared.DTOS.Productdtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MapperProfiles
{
    public class ProductPictureUrl : IValueResolver<Product, ProductDtos, string>

    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDtos destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            if (source.PictureUrl.StartsWith("http"))
                return source.PictureUrl;
            var baseurl = _configuration.GetSection("URLS")["BaseUrl"];
            if(string.IsNullOrEmpty(baseurl))
                return string.Empty;

            var picturl=$"{baseurl}/{source.PictureUrl}";
            return picturl;
        }
    }
}
