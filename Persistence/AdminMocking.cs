using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.AppDbContext;

namespace Persistence
{
    public class AdminMocking
    {
        

        public static void Mock(IApplicationBuilder _applicationBuilder)
        {
            using (var service = _applicationBuilder.ApplicationServices.CreateScope())
            {
                var _context = service.ServiceProvider.GetService<Context>();

                if (!_context.Users.Any())
                {
                    var role = new Role
                    {
                        RoleName = "Admin",
                        RoleDescription = "AppOwner"
                    };
                   
                    var user = new User
                    {
                        FirstName = "Ola",
                        LastName = "Bisi",
                        PhoneNumber = "08132759937",
                        Email ="ola@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("123"),
                        Address="Abk",
                        ProfilePicture="admin.jpg",
                        RoleId = role.Id
                    };
                   
                    // var admin = new Admin
                    // {
                    //     UserId = user.Id,
                    //     CompanyWallet = 0,
                    //     CreatedBy = "System",
                    //     ModifiedBy = "System"
                    // };
                   
                    // _context.Roles.Add(role);
                    // _context.Users.Add(user);
                    // _context.Admin.Add(admin);
                    // _context.SaveChanges();
                }
            }
        }
    }
}
 
    