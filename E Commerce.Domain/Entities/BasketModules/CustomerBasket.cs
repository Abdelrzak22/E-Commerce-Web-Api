using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.BasketModules
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItems> Items { get; set; } = [];
    }
}
