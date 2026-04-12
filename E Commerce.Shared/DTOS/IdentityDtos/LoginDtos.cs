using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOS.IdentityDtos
{
    public record LoginDtos([EmailAddress]string Email,string Password);
   
}
