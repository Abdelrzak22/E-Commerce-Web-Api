using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOS.IdentityDtos
{
    public record UserDtos(string Email,string DisplayName,string Token);
    
}
