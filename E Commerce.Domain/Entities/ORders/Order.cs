using E_Commerce.Domain.Entities.ORders.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.ORders
{
    public class Order<Tkey>:BaseEntity<Tkey>
    {
        public string Email { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;
        public OrderStatus Status { get; set; }
        public OrderAdress ShoppingAdress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId {  get; set; } //fk
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; } 
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
