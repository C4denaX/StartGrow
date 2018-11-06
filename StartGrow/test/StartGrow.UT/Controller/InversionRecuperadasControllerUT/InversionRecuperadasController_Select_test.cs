using System;
using System.Collections.Generic;
using System.Text;
using StartGrow.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartGrow.Models;
using StartGrow.Data;
using StartGrow.Models.InversionRecuperadaViewModels;

namespace StartGrow.UT.Controller.InversionRecuperadasControllerUT
{

    public class InversionRecuperadasController_Select_test
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            //Crear un nuevo proveedor de servicios, y por tanto, una nueva instancia
            //de base de datos InMemory
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
            //Crear una nueva instancia de opciones indicando el contexto que use
            //una base de datos InMemory y el nuevo proovedor de servicios.

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("StartGrow")
                    .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext inversionRecuperadaContext;
        public InversionRecuperadasController_Select_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            //Insertar datos semilla en la base de datos usando una instancia de contexto   


            var rating = new Rating { Nombre = "A" };
            context.Rating.Add(rating);
            var area = new Areas { Nombre = "Sanidad" };
            context.Areas.Add(area);
            var tipo = new TiposInversiones { Nombre = "Crownfunding" };
            context.TiposInversiones.Add(tipo);


            
            Proyecto proyecto1 = new Proyecto
            {
                ProyectoId = 1,
                FechaExpiracion = new DateTime(2020, 1, 1),
                Importe = 12,
                Interes = 2,
                MinInversion = 5,
                Nombre = "Pruebas en sanidad",
                NumInversores = 0,
                Plazo = 12,
                Progreso = 34,
                Rating = rating
            };
            context.Proyecto.Add(proyecto1);

            context.ProyectoAreas.Add(new ProyectoAreas { Proyecto = proyecto1, Areas = area });


            Inversor inversor1 = new Inversor
            {
                Id = "1",
                Nombre = "david@uclm.es",
                Email = "david@uclm.es",
                Apellido1 = "Girón",
                Apellido2 = "López",
                Domicilio = "C/Cuenca",
                Municipio = "Albacete",
                NIF = "48259596",
                Nacionalidad = "Española",
                PaisDeResidencia = "España",
                Provincia = "Albacete",
                PasswordHash = "hola"
            };
            context.Users.Add(inversor1);


            context.Inversion.Add(new Inversion
            {
                InversionId = 1,
                Cuota = 6,
                EstadosInversiones = "En_Curso",
                Intereses = 12,
                Inversor = inversor1,
                Proyecto = proyecto1,
                TipoInversionesId = 1,
                Total = 50
            });

            context.Inversion.Add(new Inversion
            {
                InversionId = 2,
                Cuota = 15,
                EstadosInversiones = "Finalizado",
                Intereses = 23,
                Inversor = inversor1,
                Proyecto = proyecto1,
                TipoInversionesId = 1,
                Total = 100
            });

            context.SaveChanges();

            //Para simular la conexión:
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("david@uclm.es");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            inversionRecuperadaContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            inversionRecuperadaContext.User = identity;

        }











        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //PRUEBAS DEL MÉTODO GET -----> UNO POR CADA CAMINO INDEPENDIENTE
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Fact]
        public async Task Select_SinFiltros()
        {

            using (context) //Base SQL ya generada con datos incluidos
            {
                //ARRANGE (Organizar) --> Creación de condiciones para la prueba.
                var controller = new InversionRecuperadasController(context);
                controller.ControllerContext.HttpContext = inversionRecuperadaContext;


                var rating = new Rating { Nombre = "A" };
                var area = new Areas { Nombre = "Sanidad" };
                var tipo = new TiposInversiones { Nombre = "Crownfunding" };

                Proyecto proyecto1 = new Proyecto
                {
                    ProyectoId = 1,
                    FechaExpiracion = new DateTime(2020, 1, 1),
                    Importe = 12,
                    Interes = 2,
                    MinInversion = 5,
                    Nombre = "Pruebas en sanidad",
                    NumInversores = 0,
                    Plazo = 12,
                    Progreso = 34,
                    Rating = rating
                };
  
                Inversor inversor1 = new Inversor
                {
                    Id = "1",
                    Nombre = "david@uclm.es",
                    Email = "david@uclm.es",
                    Apellido1 = "Girón",
                    Apellido2 = "López",
                    Domicilio = "C/Cuenca",
                    Municipio = "Albacete",
                    NIF = "48259596",
                    Nacionalidad = "Española",
                    PaisDeResidencia = "España",
                    Provincia = "Albacete",
                    PasswordHash = "hola"
                };

                var inversionesEsperadas = new Inversion[2]
                {
                    new Inversion {
                        InversionId = 1,
                        Cuota = 6,
                        EstadosInversiones = "En_Curso",
                        Intereses = 12,
                        Inversor = inversor1,
                        Proyecto = proyecto1,
                        TipoInversionesId = 1,
                        Total = 50
                    },

                     new Inversion {
                        InversionId = 2,
                        Cuota = 15,
                        EstadosInversiones = "Finalizado",
                        Intereses = 23,
                        Inversor = inversor1,
                        Proyecto = proyecto1,
                        TipoInversionesId = 1,
                        Total = 100
                    }
                };

                //ACT (Actuar) --> Realización de la prueba
                var result = controller.SelectInversionForRecuperarInversion(0, null, null, null, null);

                //ASSERT --> Verificación de que el resultado fue el que se esperaba
                var viewResult = Assert.IsType<ViewResult>(result); //Comprueba si el controlador devuelve una vista
                SelectInversionForRecuperarInversionViewModel model = viewResult.Model as SelectInversionForRecuperarInversionViewModel;

                Assert.Equal(inversionesEsperadas, model.Inversiones, Comparer.Get<Inversion>((i1, i2) =>  i1.Cuota == i2.Cuota && i1.EstadosInversiones == i2.EstadosInversiones
                 && i1.TipoInversionesId == i1.TipoInversionesId && i1.Total == i2.Total));
            }
        }





        [Fact]
        public async Task Select_FiltroId()
        {

            using (context) //Base SQL ya generada con datos incluidos
            {
                //ARRANGE (Organizar) --> Creación de condiciones para la prueba.
                var controller = new InversionRecuperadasController(context);
                controller.ControllerContext.HttpContext = inversionRecuperadaContext;


                var inversionesEsperadas = new Inversion[1]
                {
                    new Inversion {
                        InversionId = 1,
                        Cuota = 6,
                        EstadosInversiones = "En_Curso",
                        Intereses = 12,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 50
                    }
                    /*
                     new Inversion {
                        InversionId = 2,
                        Cuota = 15,
                        EstadosInversiones = "Recaudación",
                        Intereses = 23,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 100
                    }
                    */
                };

                //ACT (Actuar) --> Realización de la prueba
                var result = controller.SelectInversionForRecuperarInversion(1, null, null, null, null);

                //ASSERT --> Verificación de que el resultado fue el que se esperaba
                var viewResult = Assert.IsType<ViewResult>(result); //Comprueba si el controlador devuelve una vista
                SelectInversionForRecuperarInversionViewModel model = viewResult.Model as SelectInversionForRecuperarInversionViewModel;

                Assert.Equal(inversionesEsperadas, model.Inversiones, Comparer.Get<Inversion>((i1, i2) => i1.InversionId == i2.InversionId
                && i1.Cuota == i2.Cuota && i1.EstadosInversiones == i2.EstadosInversiones && i1.Intereses == i2.Intereses && i1.InversorId == i2.InversorId
                && i1.ProyectoId == i2.ProyectoId && i1.TipoInversionesId == i1.TipoInversionesId && i1.Total == i2.Total));
            }
        }



        [Fact]
        public async Task Select_FiltroArea()
        {

            using (context) //Base SQL ya generada con datos incluidos
            {
                //ARRANGE (Organizar) --> Creación de condiciones para la prueba.
                var controller = new InversionRecuperadasController(context);
                controller.ControllerContext.HttpContext = inversionRecuperadaContext;
                var inversionesEsperadas = new Inversion[2]
                {
                    new Inversion {
                        InversionId = 1,
                        Cuota = 6,
                        EstadosInversiones = "En_Curso",
                        Intereses = 12,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 50
                    },

                     new Inversion {
                        InversionId = 1,
                        Cuota = 15,
                        EstadosInversiones = "Recaudación",
                        Intereses = 23,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 100
                    }
                };

                //ACT (Actuar) --> Realización de la prueba
                var result = controller.SelectInversionForRecuperarInversion(0, "Sanidad", null, null, null);

                //ASSERT --> Verificación de que el resultado fue el que se esperaba
                var viewResult = Assert.IsType<ViewResult>(result); //Comprueba si el controlador devuelve una vista
                SelectInversionForRecuperarInversionViewModel model = viewResult.Model as SelectInversionForRecuperarInversionViewModel;

                Assert.Equal(inversionesEsperadas, model.Inversiones, Comparer.Get<Inversion>((i1, i2) => i1.InversionId == i2.InversionId
                && i1.Cuota == i2.Cuota && i1.EstadosInversiones == i2.EstadosInversiones && i1.Intereses == i2.Intereses && i1.InversorId == i2.InversorId
                && i1.ProyectoId == i2.ProyectoId && i1.TipoInversionesId == i1.TipoInversionesId && i1.Total == i2.Total));
            }
        }



        [Fact]
        public async Task Select_FiltroEstado()
        {

            using (context) //Base SQL ya generada con datos incluidos
            {
                //ARRANGE (Organizar) --> Creación de condiciones para la prueba.
                var controller = new InversionRecuperadasController(context);
                controller.ControllerContext.HttpContext = inversionRecuperadaContext;
                var inversionesEsperadas = new Inversion[2]
                {
                    new Inversion {
                        InversionId = 1,
                        Cuota = 6,
                        EstadosInversiones = "En_Curso",
                        Intereses = 12,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 50
                    },

                     new Inversion {
                        InversionId = 1,
                        Cuota = 15,
                        EstadosInversiones = "Recaudación",
                        Intereses = 23,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 100
                    }
                };

                //ACT (Actuar) --> Realización de la prueba
                var result = controller.SelectInversionForRecuperarInversion(0, null, "En_Curso", null, null);

                //ASSERT --> Verificación de que el resultado fue el que se esperaba
                var viewResult = Assert.IsType<ViewResult>(result); //Comprueba si el controlador devuelve una vista
                SelectInversionForRecuperarInversionViewModel model = viewResult.Model as SelectInversionForRecuperarInversionViewModel;

                Assert.Equal(inversionesEsperadas, model.Inversiones, Comparer.Get<Inversion>((i1, i2) => i1.InversionId == i2.InversionId
                && i1.Cuota == i2.Cuota && i1.EstadosInversiones == i2.EstadosInversiones && i1.Intereses == i2.Intereses && i1.InversorId == i2.InversorId
                && i1.ProyectoId == i2.ProyectoId && i1.TipoInversionesId == i1.TipoInversionesId && i1.Total == i2.Total));
            }
        }




        [Fact]
        public async Task Select_FiltroTipo()
        {

            using (context) //Base SQL ya generada con datos incluidos
            {
                //ARRANGE (Organizar) --> Creación de condiciones para la prueba.
                var controller = new InversionRecuperadasController(context);
                controller.ControllerContext.HttpContext = inversionRecuperadaContext;
                var inversionesEsperadas = new Inversion[2]
                {
                    new Inversion {
                        InversionId = 1,
                        Cuota = 6,
                        EstadosInversiones = "En_Curso",
                        Intereses = 12,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 50
                    },

                     new Inversion {
                        InversionId = 1,
                        Cuota = 15,
                        EstadosInversiones = "Recaudación",
                        Intereses = 23,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 100
                    }
                };

                //ACT (Actuar) --> Realización de la prueba
                var result = controller.SelectInversionForRecuperarInversion(0, null, null, "Crownfunding", null);

                //ASSERT --> Verificación de que el resultado fue el que se esperaba
                var viewResult = Assert.IsType<ViewResult>(result); //Comprueba si el controlador devuelve una vista
                SelectInversionForRecuperarInversionViewModel model = viewResult.Model as SelectInversionForRecuperarInversionViewModel;

                Assert.Equal(inversionesEsperadas, model.Inversiones, Comparer.Get<Inversion>((i1, i2) => i1.InversionId == i2.InversionId
                && i1.Cuota == i2.Cuota && i1.EstadosInversiones == i2.EstadosInversiones && i1.Intereses == i2.Intereses && i1.InversorId == i2.InversorId
                && i1.ProyectoId == i2.ProyectoId && i1.TipoInversionesId == i1.TipoInversionesId && i1.Total == i2.Total));
            }
        }




        [Fact]
        public async Task Select_FiltroRating()
        {

            using (context) //Base SQL ya generada con datos incluidos
            {
                //ARRANGE (Organizar) --> Creación de condiciones para la prueba.
                var controller = new InversionRecuperadasController(context);
                controller.ControllerContext.HttpContext = inversionRecuperadaContext;
                var inversionesEsperadas = new Inversion[2]
                {
                    new Inversion {
                        InversionId = 1,
                        Cuota = 6,
                        EstadosInversiones = "En_Curso",
                        Intereses = 12,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 50
                    },

                     new Inversion {
                        InversionId = 1,
                        Cuota = 15,
                        EstadosInversiones = "Recaudación",
                        Intereses = 23,
                        InversorId = "1",
                        ProyectoId = 1,
                        TipoInversionesId = 1,
                        Total = 100
                    }
                };

                //ACT (Actuar) --> Realización de la prueba
                var result = controller.SelectInversionForRecuperarInversion(0, null, null, null, "A");

                //ASSERT --> Verificación de que el resultado fue el que se esperaba
                var viewResult = Assert.IsType<ViewResult>(result); //Comprueba si el controlador devuelve una vista
                SelectInversionForRecuperarInversionViewModel model = viewResult.Model as SelectInversionForRecuperarInversionViewModel;

                Assert.Equal(inversionesEsperadas, model.Inversiones, Comparer.Get<Inversion>((i1, i2) => i1.InversionId == i2.InversionId
                && i1.Cuota == i2.Cuota && i1.EstadosInversiones == i2.EstadosInversiones && i1.Intereses == i2.Intereses && i1.InversorId == i2.InversorId
                && i1.ProyectoId == i2.ProyectoId && i1.TipoInversionesId == i1.TipoInversionesId && i1.Total == i2.Total));
            }
        }

        









        //----------------------------------------------------------------------------------------------------
        //PRUEBAS DEL MÉTODO POST -----> UNO POR CADA CAMINO INDEPENDIENTE
        //----------------------------------------------------------------------------------------------------

    }
}
