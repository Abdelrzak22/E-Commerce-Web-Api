using E_Commerce.Shared;
using E_Commerce.Shared.DTOS.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ServiceAbstraction
{
    public interface IIdentityServic
    {
        Task<Result<UserDtos>> LoginAsync(LoginDtos loginDtos);
        Task<Result<UserDtos>> ResgisterAsync(RegisterDtos registerDtos);
    }
}
