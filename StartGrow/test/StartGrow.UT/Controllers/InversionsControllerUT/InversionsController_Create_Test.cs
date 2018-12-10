using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartGrow.Controllers;
using StartGrow.Data;
using StartGrow.Models;
using StartGrow.Models.InversionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StartGrow.UT.Controllers.InversionsControllerUT
{
    public class InversionsController_Create_Test
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
        private DefaultHttpContext inversionContext;

        public InversionsController_Create_Test()
        {
            _contextOptions = CreateNewContextOptions();
            // Insert seed data into the database using one instance of the context
            context = new ApplicationDbContext(_contextOptions);

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

            //Areas Temáticas
            context.Areas.Add(new Areas { Nombre = "Sanidad" });

            //Rating
            context.Rating.Add(new Rating { Nombre = "A" });

            //Tipos de Inversiones
            context.TiposInversiones.Add(new TiposInversiones { Nombre = "Crowdfunding" });

            //Proyecto
            context.Proyecto.Add(new Proyecto { ProyectoId = 1, FechaExpiracion = new DateTime(2019, 01, 23), Importe = 30000, Interes = (float)5.90, MinInversion = 50, Nombre = "E-MEDICA", NumInversores = 0, Plazo = 12, Progreso = 0, RatingId = 3 });
            context.Proyecto.Add(new Proyecto { ProyectoId = 2, FechaExpiracion = new DateTime(2019, 01, 14), Importe = 70000, Interes = (float)7.25, MinInversion = 0, Nombre = "PROTOS", NumInversores = 0, Plazo = 48, Progreso = 0, RatingId = 2 });
            context.Proyecto.Add (new Proyecto { ProyectoId = 3, FechaExpiracion = new DateTime (2019, 01, 14), Importe = 93000, Interes = (float) 4.50, MinInversion = 100, Nombre = "SUBSOLE", NumInversores = 0, Plazo = 6, Progreso = 0, RatingId = 1 });            

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

        //METODO GET        

        [Fact]
        public async Task CreateGet_ConProyectosSeleccionados()
        {
            using (context)
            {
                // Arrenge
                var controller = new InversionsController(context);
                controller.ControllerContext.HttpContext = inversionContext;

                TiposInversiones tipoInversion1 = new TiposInversiones { Nombre = "Crowdfunding" };
                TiposInversiones tipoInversion2 = new TiposInversiones { Nombre = "Business Angels" };

                var tiposInversiones = new List <TiposInversiones> { tipoInversion1, tipoInversion2};

                var expectedTiposInversiones = new SelectList(tiposInversiones.Select(p => p.Nombre.ToList()));

                String[] ids = new string[1] { "1" };

                SelectedProyectosForInversionViewModel proyectos = new SelectedProyectosForInversionViewModel() { IdsToAdd = ids };
                Proyecto expectedProyecto = new Proyecto { ProyectoId = 1, FechaExpiracion = new DateTime(2019, 01, 23), Importe = 30000, Interes = (float)5.90, MinInversion = 50, Nombre = "E-MEDICA", NumInversores = 0, Plazo = 12, Progreso = 0, RatingId = 3 };
                Inversor expectedInversor = new Inversor
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
                };

                Inversion expectedInversion = new Inversion { Proyecto = expectedProyecto, Inversor = expectedInversor };
                IList<InversionCreateViewModel> inversiones = new InversionCreateViewModel[1] { new InversionCreateViewModel { inversion = expectedInversion } };
                InversionesCreateViewModel expectedInversionCV = new InversionesCreateViewModel
                {
                    Name = expectedInversor.Nombre,
                    FirstSurname = expectedInversor.Apellido1,
                    SecondSurname = expectedInversor.Apellido2,
                    inversiones = inversiones

                };

                //Act
                var result = controller.Create(proyectos);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                InversionesCreateViewModel model = viewResult.Model as InversionesCreateViewModel;

                Assert.Equal(model, expectedInversionCV, Comparer.Get<InversionesCreateViewModel>((p1, p2) =>
                p1.Name == p2.Name && p1.FirstSurname == p2.FirstSurname && p1.SecondSurname == p2.SecondSurname));

                Assert.Equal(model.inversiones[0].inversion, expectedInversionCV.inversiones[0].inversion, Comparer.Get<Inversion>((p1, p2) => p1.TipoInversiones.Nombre == p2.TipoInversiones.Nombre
                 && p1.InversionId == p2.InversionId));

                Assert.Equal(model.inversiones[0].inversion.Proyecto, expectedInversionCV.inversiones[0].inversion.Proyecto, Comparer.Get<Proyecto>((p1, p2) => p1.Nombre == p2.Nombre
                && p1.FechaExpiracion == p2.FechaExpiracion && p1.ProyectoId == p2.ProyectoId));

                Assert.Equal(expectedTiposInversiones, (SelectList)viewResult.ViewData["TiposInversiones"], Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
                

            }
        }

        [Fact]
        public async Task CreateGet_SinProyectosSeleccionados()
        {
            using (context)
            {
                // Arrenge
                var controller = new InversionsController(context);
                controller.ControllerContext.HttpContext = inversionContext;

                SelectedProyectosForInversionViewModel proyectos = new SelectedProyectosForInversionViewModel() { IdsToAdd = new string[0] };


                //Act


                //Assert


            }
        }

        //METODO POST

    }
}

