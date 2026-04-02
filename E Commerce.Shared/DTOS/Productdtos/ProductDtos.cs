using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOS.Productdtos
{
    public class ProductDtos
    {
        public string Name { get; set; } = default!;
        public int Id { get;set; }  
        public string Description { get; set; }=default!;
        public string PictureUrl { get; set; } =default!;
        public decimal Price { get; set; }
        public string ProductType { get; set; } = default!;
        public string ProductBrand { get; set; } =default!; 
    }
}
