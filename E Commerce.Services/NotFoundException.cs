using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public abstract class NotFoundException(string Message):Exception(Message)
    {
    }

    public sealed class ProductNotFound(int id):NotFoundException($"the product with id={id} is not found")
    {

    }

    public sealed class BasketNotFound(string id):NotFoundException($"the basket with id={id} is not found")
    {

    }
}
