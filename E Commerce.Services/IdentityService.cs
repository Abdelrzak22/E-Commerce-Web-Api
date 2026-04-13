using E_Commerce.Domain.Entities.IdentityModules;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOS.IdentityDtos;
using E_Commerce.Shared.DTOS.Productdtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class IdentityService : IIdentityServic
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuraion;

        public IdentityService(UserManager<ApplicationUser> userManager,IConfiguration configuraion)
        {
            _userManager = userManager;
            _configuraion = configuraion;
        }

        public async Task<Result<UserDtos>> LoginAsync(LoginDtos loginDtos)
        {
            var User = await _userManager.FindByEmailAsync(loginDtos.Email);
            if (User is null)
                return Result<UserDtos>.Fail(Error.InvalidCredentials("User.invalidcredentilas"));
            var password = await _userManager.CheckPasswordAsync(User, loginDtos.Password);
            if(!password)
                return Result<UserDtos>.Fail(Error.InvalidCredentials("User.invalidcredentilas"));
            var token =await CreateTokenAsync(User);
            return Result<UserDtos>.Ok(new UserDtos(User.Email!, User.DisplayName, token));


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
            if (iscreated.Succeeded)
            {
                var token = await CreateTokenAsync(user);
                return Result<UserDtos>.Ok(new UserDtos(user.Email!, user.DisplayName, token));
            }
               

            var errors = iscreated.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
            return Result<UserDtos>.Fail(errors);
           



        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {

            var claim = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email !),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName !),


            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretkey = _configuraion["jwtOptions:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var Credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuraion["jwtOptions:Issure"],
                audience: _configuraion["jwtOptions:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims:claim,
                signingCredentials: Credintials);

            return new JwtSecurityTokenHandler().WriteToken(Token);


        }
    }
}
