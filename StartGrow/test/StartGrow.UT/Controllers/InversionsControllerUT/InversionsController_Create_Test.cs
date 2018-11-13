using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartGrow.Controllers;
using StartGrow.Data;
using StartGrow.Models;
using StartGrow.Models.SolicitudViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StartGrow.UT.Controllers.InversionsControllerUT
{
    class InversionsController_Create_Test
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("StartGrow")
                    .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext inversionContext;
        
        public InversionsController_Create_Test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context

            //Areas Temáticas
            context.Areas.Add(new Areas { Nombre = "Sanidad" });

            //Rating
            context.Rating.Add(new Rating { Nombre = "A" });

            //Tipos de Inversiones
            context.TiposInversiones.Add(new TiposInversiones { Nombre = "Crowdfunding" });

            //Proyecto
            context.Proyecto.Add(new Proyecto { ProyectoId = 1, FechaExpiracion = new DateTime(2019, 01, 23), Importe = 30000, Interes = (float)5.90, MinInversion = 50, Nombre = "E-MEDICA", NumInversores = 0, Plazo = 12, Progreso = 0, RatingId = 3 });
            context.Proyecto.Add(new Proyecto { ProyectoId = 2, FechaExpiracion = new DateTime(2019, 01, 14), Importe = 70000, Interes = (float)7.25, MinInversion = 0, Nombre = "PROTOS", NumInversores = 0, Plazo = 48, Progreso = 0, RatingId = 2 });
            //context.Proyecto.Add (new Proyecto { ProyectoId = 3, FechaExpiracion = new DateTime (2019, 01, 14), Importe = 93000, Interes = (float) 4.50, MinInversion = 100, Nombre = "SUBSOLE", NumInversores = 0, Plazo = 6, Progreso = 0, RatingId = 1 });

            //Inversor
            context.Users.Add(new Inversor
            {
                UserName = "yasin@uclm.com",
                NIF = "47446245M",
                PhoneNumber = "684010548",
                Email = "yasin@uclm.com",
                Nombre = "Yasin",
                Apellido1 = "Muñoz",
                Apellido2 = "El Merabety",
                Domicilio = "Gabriel Ciscar, 26",
                Nacionalidad = "Española",
                PaisDeResidencia = "España",
                Provincia = "Albacete"
            });

            context.SaveChanges();

            foreach (var proyecto in context.Proyecto.ToList())
            {
                context.ProyectoAreas.Add(new ProyectoAreas { Proyecto = proyecto, Areas = context.Areas.First() });
                context.ProyectoTiposInversiones.Add(new ProyectoTiposInversiones { Proyecto = proyecto, TiposInversiones = context.TiposInversiones.First() });
            }
            context.SaveChanges();

            //Simulación conexión de un usuario
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("yasin@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            inversionContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            inversionContext.User = identity;
        }


    }
}
