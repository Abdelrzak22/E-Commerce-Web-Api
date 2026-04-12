using E_Commerce.Domain.Entities.IdentityModules;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOS.IdentityDtos;
using E_Commerce.Shared.DTOS.Productdtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class IdentityService : IIdentityServic
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<UserDtos>> LoginAsync(LoginDtos loginDtos)
        {
            var User = await _userManager.FindByEmailAsync(loginDtos.Email);
            if (User is null)
                return Result<UserDtos>.Fail(Error.InvalidCredentials("User.invalidcredentilas"));
            var password = await _userManager.CheckPasswordAsync(User, loginDtos.Password);
            if(!password)
                return Result<UserDtos>.Fail(Error.InvalidCredentials("User.invalidcredentilas"));
            return Result<UserDtos>.Ok(new UserDtos(User.Email!, User.DisplayName, "token"));


        }

        public async Task<Result<UserDtos>> ResgisterAsync(RegisterDtos registerDtos)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDtos.DisplayName,
                Email = registerDtos.Email,
                PhoneNumber = registerDtos.PhoneNumber,
                UserName = registerDtos.UserName
            };
            var iscreated = await _userManager.CreateAsync(user, registerDtos.Password);
            if(iscreated.Succeeded)
                return Result<UserDtos>.Ok(new UserDtos(user.Email!, user.DisplayName, "token"));

            var errors = iscreated.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
            return Result<UserDtos>.Fail(errors);
           



        }
    }
}
