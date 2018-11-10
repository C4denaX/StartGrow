using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartGrow.Models;
using StartGrow.Data;
using StartGrow.Models.PreferenciasViewModel;
using StartGrow.Controllers;
using Microsoft.AspNetCore.Http;

namespace StartGrow.UT.Controllers.AccountController_test
{
    class Account_SelectPreferenciasForInversor_test
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("StartGrow").UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext accountContext;

        public Account_SelectPreferenciasForInversor_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            
            // Insert seed data into the database using one instance of the context

            context.Areas.Add(new Areas { Nombre = "Sanidad" });
            context.Areas.Add(new Areas { Nombre = "Consultoria" });
            context.Areas.Add(new Areas { Nombre = "Educación" });
            context.Areas.Add(new Areas { Nombre = "Seguridad" });
            context.Areas.Add(new Areas { Nombre = "Construcción" });
            context.Areas.Add(new Areas { Nombre = "Transporte" });
            context.Areas.Add(new Areas { Nombre = "TIC" });
            context.Areas.Add(new Areas { Nombre = "Ingeniería" });
            context.Areas.Add(new Areas { Nombre = "Hogar" });
            context.Areas.Add(new Areas { Nombre = "Alimentación" });
            context.Areas.Add(new Areas { Nombre = "Textil" });
            context.Areas.Add(new Areas { Nombre = "Comercio" });
            context.Areas.Add(new Areas { Nombre = "Hosteleria" });
            context.Areas.Add(new Areas { Nombre = "Administración" });
            context.Areas.Add(new Areas { Nombre = "Automóviles" });
            context.Areas.Add(new Areas { Nombre = "Reparaciones" });
            context.Areas.Add(new Areas { Nombre = "Banca" });
            context.Areas.Add(new Areas { Nombre = "Maquinaría" });

            context.TiposInversiones.Add(new TiposInversiones { Nombre = "Business Angels" });
            context.TiposInversiones.Add(new TiposInversiones { Nombre = "Crownfunding" });
            context.TiposInversiones.Add(new TiposInversiones { Nombre = "Venture Capital" });

            context.Rating.Add(new Rating { Nombre = "A" });
            context.Rating.Add(new Rating { Nombre = "B" });
            context.Rating.Add(new Rating { Nombre = "C" });
            context.Rating.Add(new Rating { Nombre = "D" });

            context.SaveChanges();
        }


        [Fact]
        public async Task Select_SinParametro()
        {
            // Arrange
            using (context) //Set the test case will use the inMemory database created in the constructor
            {
         //       var controller = HttpContext.RequestServices.GetService(Type.GetType("StartGrow.Data.ApplicationDbContext"));

                //var controller = new AccountController(context);
          //      controller.ControllerContext.HttpContext = accountContext;

                var preferenciaEsperada = new Preferencias[2]
                {
                    new Preferencias{AreasId = 1, RatingId = 1, TiposInversionesId = 1},
                    new Preferencias{AreasId = 2, RatingId = 2, TiposInversionesId = 2}
                };

                //Areas
                var areaEsperada = new Areas[1] { new Areas { Nombre = "Sanidad" } };

                //TiposInversiones
                var tipoEsperado = new TiposInversiones[1] { new TiposInversiones { Nombre = "Crownfunding" } };

                //Rating
                var ratingEsperado = new Rating[1] { new Rating { Nombre = "A" } };

                //Act
      //          var result = await controller.SelectPreferenciasForInversor();

                //Assert
          //      var viewResult = Assert.IsType<ViewResult>(result);
            /*    SelectPreferenciasForInversorViewModel model = viewResult.Model as SelectPreferenciasForInversorViewModel;

                Assert.Equal(areaEsperada, model.Areas, Comparer.Get<Areas>((a1, a2) => a1.Nombre == a2.Nombre));
                Assert.Equal(tipoEsperado, model.TiposInversiones, Comparer.Get<TiposInversiones>((t1, t2) => t1.Nombre == t2.Nombre));
                Assert.Equal(ratingEsperado, model.Rating, Comparer.Get<Rating>((r1, r2) => r1.Nombre == r2.Nombre));
                */
            }
        }
    }
}
