using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOS.Basket
{
    public record BasketDto(string Id , ICollection<BasketItemDto> Items);
    
}
