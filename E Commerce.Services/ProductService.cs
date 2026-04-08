using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductModules;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Services.Spacifications;
using E_Commerce.Shared.DTOS.Productdtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService( IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDtos>> GetAllBrandAsync()
        {
            var Brands = await _unitOfWork.GenericRepo<ProductBrand, int>().GetAllAsync();

            return  _mapper.Map<IEnumerable<BrandDtos>>(Brands);    
        }

        public async Task<IEnumerable<ProductDtos>> GetAllProductAsync()
        {

            var specifi = new ProductWithTypeAndBrandSpecifications();
            var products = await _unitOfWork.GenericRepo<Product, int>().GetAllAsync(specifi);
            return _mapper.Map<IEnumerable<ProductDtos>>(products);
        }

        public async Task<IEnumerable<TypeDtos>> GetAllTypeAsync()
        {
            var types=await _unitOfWork.GenericRepo<ProductType,int>().GetAllAsync();   
            return _mapper.Map<IEnumerable<TypeDtos>>(types);
        }

        public async Task<ProductDtos> GetProductById(int id)
        {
            var product = await _unitOfWork.GenericRepo<Product, int>().GetByIdAsync(id);
            return _mapper.Map<ProductDtos>(product);
        }
    }
}
