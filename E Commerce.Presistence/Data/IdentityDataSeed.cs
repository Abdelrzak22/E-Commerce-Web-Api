using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presistence.Data
{
    public class IdentityDataSeed : IDataintializer
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;
        private readonly ILogger<IdentityDataSeed> _logger;

        public IdentityDataSeed(UserManager<ApplicationUser> user,RoleManager<IdentityRole> role,ILogger<IdentityDataSeed>logger)
        {
            _user = user;
            _role = role;
            _logger = logger;
        }

        public async Task Intializeasync()
        {
            try
            {
                if (!_role.Roles.Any())
                {
                    await _role.CreateAsync(new IdentityRole("Admin"));
                    await _role.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_user.Users.Any())
                {
                    var User1 = new ApplicationUser()
                    {
                        DisplayName = "Mohamed",
                        Email = "koko12@gmail.com",
                        UserName = "MohamedTaha",
                        PhoneNumber = "01021777947"
                    };
                    var User2 = new ApplicationUser()
                    {
                        DisplayName = "Salma",
                        Email = "soso12@gmail.com",
                        UserName = "SalmaTaha",
                        PhoneNumber = "01021777948"
                    }
                    ;

                    await _user.CreateAsync(User1, "P@ssw0rd");
                    await _user.CreateAsync(User2, "P@ssw0rd");

                    await _user.AddToRoleAsync(User1, "Admin");
                    await _user.AddToRoleAsync(User2, "SuperAdmin");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while seeding Identitydatabase : messgae= {ex.Message}");
            }

            
        }
    }
}
