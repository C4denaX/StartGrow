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

namespace StartGrow.UT.Controllers.SolicitudesControllerUT
{
   public class SolicitudesController_Create_Test
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
        Microsoft.AspNetCore.Http.DefaultHttpContext purchaseContext;
        private DefaultHttpContext solicitudContext;

        public SolicitudesController_Create_Test()
        {
            _contextOptions = CreateNewContextOptions();
            // Insert seed data into the database using one instance of the
            context = new ApplicationDbContext(_contextOptions);


            context.Users.Add(new Trabajador
            {
                UserName = "sergio@uclm.es",
                Email = "sergio@uclm.es",
                Apellido1 = "Ruiz",
                Apellido2 = "Villafranca",
                Domicilio = "C/Hellin",
                Municipio = "Albacete",
                NIF = "06290424",
                Nacionalidad = "Española",
                PaisDeResidencia = "España",
                Provincia
            = "Albacete"
            });


            Areas area = new Areas { Nombre = "TIC" };
            context.Areas.Add(area);

            TiposInversiones tipo = new TiposInversiones { Nombre = "Crowdfunding" };

            context.TiposInversiones.Add(tipo);
            Rating rating1 = new Rating { Nombre = "A" };
            Rating rating2 = new Rating { Nombre = "F" };

            context.Rating.Add(rating1);
            context.Rating.Add(rating2);

            context.Proyecto.Add(new Proyecto { ProyectoId = 1, FechaExpiracion = new DateTime(2019, 01, 23), Importe = 70000, Interes = null, MinInversion = 50, Nombre = "POCHOLO RULES", NumInversores = 0, Plazo = null, Progreso = 0, RatingId = null });
            context.Proyecto.Add(new Proyecto { ProyectoId = 2, FechaExpiracion = new DateTime(2019, 01, 23), Importe = 30000, Interes = null, MinInversion = 50, Nombre = "GRE-GYM", NumInversores = 0, Plazo = null, Progreso = 0, RatingId = null });
            context.Proyecto.Add(new Proyecto { ProyectoId = 3, FechaExpiracion = new DateTime(2019, 01, 23), Importe = 30000, Interes = null, MinInversion = 50, Nombre = "EINSTEIN-MANIA", NumInversores = 0, Plazo = null, Progreso = 0, RatingId = 1 });


            context.SaveChanges();

            foreach (var proyecto in context.Proyecto.ToList())
            {
                context.ProyectoAreas.Add(new ProyectoAreas { Proyecto = proyecto, Areas = context.Areas.First() });
                context.ProyectoTiposInversiones.Add(new ProyectoTiposInversiones { Proyecto = proyecto, TiposInversiones = context.TiposInversiones.First() });

            }
            context.SaveChanges();

            //how to simulate the connection 
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("sergio@uclm.es");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            solicitudContext = new DefaultHttpContext();
            solicitudContext.User = identity;

        }
        [Fact]
        public async Task Create_SinProyectos()
        {
            using (context)
            {
                // Arrenge
                var controller = new SolicitudesController(context);
                //Simular una conexion de usuario
                controller.ControllerContext.HttpContext = solicitudContext;
                SelectedProyectosForSolicitudViewModel proyectos = new SelectedProyectosForSolicitudViewModel();

                Trabajador trabajadorEsperado = new Trabajador
                {
                    UserName = "sergio@uclm.es",
                    Email = "sergio@uclm.es",
                    Apellido1 = "Ruiz",
                    Apellido2 = "Villafranca",
                    Domicilio = "C/Hellin",
                    Municipio = "Albacete",
                    NIF = "06290424",
                    Nacionalidad = "Española",
                    PaisDeResidencia = "España",
                    Provincia
            = "Albacete"
                };
                SolicitudCreateViewModel solicitudEsperada = new SolicitudCreateViewModel
                {
                    Name = trabajadorEsperado.Nombre,
                    FirstSurname = trabajadorEsperado.Apellido1,
                    SecondSurname = trabajadorEsperado.Apellido2
                };

                //Act
                var result = controller.Create(proyectos);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                SolicitudCreateViewModel currentSolicitud = viewResult.Model as SolicitudCreateViewModel;
                var error = viewResult.ViewData.ModelState["ProyectoNoSeleccionado"].Errors.FirstOrDefault();
                Assert.Equal(currentSolicitud, solicitudEsperada, Comparer.Get<SolicitudCreateViewModel>((p1, p2) =>
                p1.Name == p2.Name && p1.FirstSurname == p2.FirstSurname && p1.SecondSurname == p2.SecondSurname));
                Assert.Equal("Por favor, selecciona un proyecto para poder crear la solicitud", error.ErrorMessage);
            }
        }
        [Fact]
        public async Task Create_ConProyectos()
        {
            using (context)
            {
                // Arrenge
                var controller = new SolicitudesController(context);
                //Simular una conexion de usuario
                controller.ControllerContext.HttpContext = solicitudContext;

                String[] ids = new string[1] { "1" };
                SelectedProyectosForSolicitudViewModel proyectos = new SelectedProyectosForSolicitudViewModel() { IdsToAdd =ids };
                Proyecto proyectoEsperado = new Proyecto { ProyectoId = 1, FechaExpiracion = new DateTime(2019, 01, 23), Importe = 70000, Interes = null, MinInversion = 50, Nombre = "POCHOLO RULES", NumInversores = 0, Plazo = null, Progreso = 0, RatingId = null };
                Trabajador trabajadorEsperado = new Trabajador
                {
                    UserName = "sergio@uclm.es",
                    Email = "sergio@uclm.es",
                    Apellido1 = "Ruiz",
                    Apellido2 = "Villafranca",
                    Domicilio = "C/Hellin",
                    Municipio = "Albacete",
                    NIF = "06290424",
                    Nacionalidad = "Española",
                    PaisDeResidencia = "España",
                    Provincia
            = "Albacete"
                };
                IList<Solicitud> solicitudes = new Solicitud[1] { new Solicitud { SolicitudId = 1, Proyecto = proyectoEsperado, Trabajador = trabajadorEsperado, FechaSolicitud = DateTime.Now } };
                SolicitudCreateViewModel solicitudEsperada = new SolicitudCreateViewModel
                {
                    Name = trabajadorEsperado.Nombre,
                    FirstSurname = trabajadorEsperado.Apellido1,
                    SecondSurname = trabajadorEsperado.Apellido2,
                     Solicitudes = solicitudes

                };
                       
                //Act
                var result = controller.Create(proyectos);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                SolicitudCreateViewModel currentSolicitud = viewResult.Model as SolicitudCreateViewModel;
                Assert.Equal(currentSolicitud, solicitudEsperada, Comparer.Get<SolicitudCreateViewModel>((p1, p2) =>
                p1.Name == p2.Name && p1.FirstSurname == p2.FirstSurname && p1.SecondSurname == p2.SecondSurname));
                Assert.Equal(currentSolicitud.Solicitudes[0].Proyecto, solicitudEsperada.Solicitudes[0].Proyecto, Comparer.Get<Proyecto>((p1,p2) => p1.ProyectoId == p2.ProyectoId
                && p1.FechaExpiracion == p2.FechaExpiracion && p1.Nombre == p2.Nombre));
                
            }
        }
    }
}
