using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DTOS.IdentityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controller
{
    public class AuthenticationsController:ApiControllerBase
    {
        private readonly IIdentityServic _authentication;

        public AuthenticationsController(IIdentityServic Authentication)
        {
            _authentication = Authentication;
        }

        //Api/Authentication/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDtos>> Login(LoginDtos login)
        {
            var Authenticated = await _authentication.LoginAsync(login);
            return HandleResult(Authenticated);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDtos>> Register(RegisterDtos register)
        {
            var Authenticated = await _authentication.ResgisterAsync(register);
            return HandleResult(Authenticated);
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> checkEmail(string Email)
        {
            var user = await _authentication.CheckEmailExist(Email);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDtos>> GetUserOfEmail()
        {
            var result = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await _authentication.GetUserByEmail(result);
            return HandleResult(user);
        }
    }
}
