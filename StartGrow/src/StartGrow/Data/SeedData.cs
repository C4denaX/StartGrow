using StartGrow.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StartGrow.Data
{
    public static class SeedData
    {
        //public static void Initialize(UserManager<ApplicationUser> userManager,
        //            RoleManager<IdentityRole> roleManager)
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
 
            
            List<string> rolesNames = new List<string> { "Trabajador", "Inversor" };

            SeedRoles(roleManager, rolesNames);
            SeedUsers(userManager, rolesNames);
            //SeedMovies(dbContext);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager, List<string> roles)
        {    

            foreach (string roleName in roles) { 
                //it checks such role does not exist in the database 
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleName;
                    role.NormalizedName = roleName;
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }

        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, List<string> roles)
        {
            //first, it checks the user does not already exist in the DB
            if (userManager.FindByNameAsync("sergio@startgrow.trabajador.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "sergio@startgrow.trabajador.com";
                user.Email = "sergio@startgrow.trabajador.com";
                user.Nombre = "Sergio";
                user.Apellido1 = "Ruiz";
                user.Apellido2 = "Villafranca";

                IdentityResult result = userManager.CreateAsync(user, "Password1234%").Result;
 
                if (result.Succeeded)
                {
                    //administrator role
                    userManager.AddToRoleAsync(user,roles[0]).Wait();
                }
            }

            if (userManager.FindByNameAsync("David@startgrow.inversor.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "David@startgrow.inversor.com";
                user.Email = "David@startgrow.inversor.com";
                user.Nombre = "David";
                 user.Apellido1= "Giron";
                user.Apellido2 = "Lopez";

                IdentityResult result = userManager.CreateAsync(user, "APassword1234%").Result;

                if (result.Succeeded)
                {
                    //Employee role
                    userManager.AddToRoleAsync(user, roles[1]).Wait();
                }
            }
        }
    }
}


