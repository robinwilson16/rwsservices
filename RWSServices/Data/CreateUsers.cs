using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RWSServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RWSServices.Data
{
    public class CreateUsers
    {
        public static async Task Initialize(ApplicationDbContext context,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<ApplicationRole> roleManager,
                          IConfiguration configuration)
        {
            context.Database.EnsureCreated();

            String adminId1 = "";
            String adminId2 = "";

            string role1 = "Admin";
            string desc1 = "This is the administrator role";

            string role2 = "Member";
            string desc2 = "This is the members role";

            string password = "P@$$w0rd";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = configuration.GetSection("AppSettings").GetSection("DefaultUsers").GetSection("AdminUser1")["Username"],
                    //UserName = "admin@rwsservices.net",
                    Email = configuration.GetSection("AppSettings").GetSection("DefaultUsers").GetSection("AdminUser1")["Username"],
                    Forename = configuration.GetSection("AppSettings").GetSection("DefaultUsers").GetSection("AdminUser1")["Forename"],
                    Surname = configuration.GetSection("AppSettings").GetSection("DefaultUsers").GetSection("AdminUser1")["Surname"]
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }

            if (await userManager.FindByNameAsync("admin2") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin2@rwsservices.net",
                    Email = "admin2@rwsservices.net",
                    Forename = "Admin2",
                    Surname = "User"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId2 = user.Id;
            }

            if (await userManager.FindByNameAsync("user") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "user@rwsservices.net",
                    Email = "user@rwsservices.net",
                    Forename = "Standard",
                    Surname = "User"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }
        }
    }
}
