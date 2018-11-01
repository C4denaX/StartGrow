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

            if (userManager.FindByNameAsync("gregorio@uclm.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "gregorio@uclm.com";
                user.Email = "gregorio@uclm.com";
                user.Nombre = "Gregorio";
                 user.Apellido1= "Diaz";
                user.Apellido2 = "Descalzo";

                IdentityResult result = userManager.CreateAsync(user, "APassword1234%").Result;

                if (result.Succeeded)
                {
                    //Employee role
                    userManager.AddToRoleAsync(user, roles[1]).Wait();
                }
            }
            /*
            if (userManager.FindByNameAsync("peter@uclm.com").Result == null)
            {
                //A customer class has been defined because it has different attributes (purchase, rental, etc.)
                Customer user = new Customer();
                user.UserName = "peter@uclm.com";
                user.Email = "peter@uclm.com";
                user.Name = "Peter";
                user.FirstSurname = "Jackson";
                user.SecondSurname = "Jackson";

                IdentityResult result = userManager.CreateAsync(user, "OtherPass12$").Result;

                if (result.Succeeded)
                {
                    //customer role
                    userManager.AddToRoleAsync(user, roles[2]).Wait();
                }
            }
            */
        }
/*
        public static void SeedMovies(ApplicationDbContext dbContext)
        {
            //Genres and movies are created so that they are available whenever the system is run
            Movie movie;
            Genre genre = dbContext.Genre.FirstOrDefault(m => m.Name.Contains("The Lord of the Rings"));
            if (genre == null) { 
            genre = new Genre()
            {
                Name = "Drama"
            };
            dbContext.Genre.Add(genre);
        }
          
            if (!dbContext.Movie.Any(m => m.Title.Contains("The Lord of the Rings"))) {
                movie = new Movie()
                {
                    Title = "The Lord of the Rings",
                    QuantityForRenting = 10,
                    PriceForRenting = 1,
                    QuantityForPurchase = 12,
                    PriceForPurchase = 15,
                    Genre = genre
                };
                dbContext.Movie.Add(movie);
            }
            
            genre = dbContext.Genre.FirstOrDefault(m => m.Name.Contains("The Lord of the Rings"));
            if (genre == null) { 
                genre = new Genre()
                {
                    Name = "Action"
                };
                dbContext.Genre.Add(genre);
            }
            if (!dbContext.Movie.Any(m => m.Title.Contains("Star Wars"))) {
                movie = new Movie()
                {
                    Title = "Star Wars",
                    QuantityForRenting = 10,
                    PriceForRenting = 1,
                    QuantityForPurchase = 12,
                    PriceForPurchase = 10,
                    Genre = genre
                };
                dbContext.Movie.Add(movie);
            }
            genre = dbContext.Genre.FirstOrDefault(m => m.Name.Contains("The Lord of the Rings"));
            if (genre == null) { 
                genre = new Genre()
                {
                    Name = "Commedy"
                };
                dbContext.Genre.Add(genre);
            }
            if (!dbContext.Movie.Any(m => m.Title.Contains("Campeones"))) {
                movie = new Movie()
                {
                    Title = "Campeones",
                    QuantityForRenting = 10,
                    PriceForRenting = 2,
                    QuantityForPurchase = 12,
                    PriceForPurchase = 20,
                    Genre = genre
                };
                dbContext.Movie.Add(movie);
            }
            dbContext.SaveChanges();
        }
        */
    }
   


}


