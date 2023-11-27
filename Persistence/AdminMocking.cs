using System;
using System.Linq;
using Domain.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.AppDbContext;

namespace Persistence
{
    public class AdminMocking
    {
        public static async void Mock(IApplicationBuilder _applicationBuilder)
        {
            using (var service = _applicationBuilder.ApplicationServices.CreateScope())
            {
                var _context = service.ServiceProvider.GetService<Context>();
                // _context.Database.EnsureCreated();
                // await _context.Database.MigrateAsync();
                if (!_context.Users.Any())
                {
                    var role = new Role
                    {
                        Id = Guid.NewGuid(),
                        RoleName = "admin",
                        RoleDescription = "AppOwner",
                        CreatedBy = "System",
                        ModifiedBy = "System",
                        DateCreated = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    };

                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Ola",
                        LastName = "Bisi",
                        PhoneNumber = "08132759937",
                        Email = "mybluvedcreator@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("123"),
                        Address = "Abk",
                        ProfilePicture = "admin.jpg",
                        RoleId = role.Id,
                        Gender = Domain.Enum.Gender.Male,
                        Role = role,
                        CreatedBy = "System",
                        ModifiedBy = "System",
                        DateCreated = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    };
                    role.Users.Add(user);
                    var admin = new Admin
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        CreatedBy = "System",
                        ModifiedBy = "System",
                        User = user,
                        DateCreated = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    };

                    // _context.Roles.Add(role);
                    // _context.Users.Add(user);
                    // _context.SaveChanges();
                    await _context.Admin.AddAsync(admin);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}

