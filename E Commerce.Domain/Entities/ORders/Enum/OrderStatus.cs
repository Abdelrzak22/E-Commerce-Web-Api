using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.ORders.Enum
{
    public enum OrderStatus
    {
       Pending=0,
       PaymentRecived,
       PaymentFailed,
    }
}
