using E_Commerce.Presentation.Attribute;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOS.Productdtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }


        [HttpGet]

        //get:baseurl/api/Products
        [RedisCache]
        public async Task<ActionResult<PaginatedResult<ProductDtos>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products=await _productServices.GetAllProductAsync(queryParams);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDtos>> GetProductById(int id)
        {
            var product=await _productServices.GetProductById(id);
            return Ok(product);
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IEnumerable<BrandDtos>>> GetAllBrands()
        {

            var brands=await _productServices.GetAllBrandAsync();
            return Ok(brands);
        }


        [HttpGet("types")]

        public async Task<ActionResult<IEnumerable<TypeDtos>>> GetAllTypes()
        {

            var types = await _productServices.GetAllTypeAsync();
            return Ok(types);
        }
    }
}
