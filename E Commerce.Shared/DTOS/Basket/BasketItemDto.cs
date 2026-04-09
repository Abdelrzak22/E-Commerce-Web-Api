using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOS.Basket
{
    public record BasketItemDto(int Id ,string ProductName,string PictureUrl, decimal Price,int Quantity);
    
}
