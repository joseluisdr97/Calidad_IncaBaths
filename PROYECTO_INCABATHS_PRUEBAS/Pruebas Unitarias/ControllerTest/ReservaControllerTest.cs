using Moq;
using NUnit.Framework;
using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.Controllers;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS_PRUEBAS.ControllerTest
{
    [TestFixture]
    class ReservaControllerTest
    {
        [Test]
        public void ContarReservas_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IReservaService>();

            faker.Setup(a => a.ObtenerListaReservas()).Returns(
                new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Now,IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Now,IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Now,IdUsuario=2,Total=40,Activo_Inactivo=true}
                });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Index() as ViewResult;
            var model = view.Model as List<Reserva>;

            Assert.AreEqual(4, model.Count);
        }
        [Test]
        public void ReturnInstance_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IReservaService>();

            faker.Setup(a => a.ObtenerListaReservas()).Returns(
                new List<Reserva> {});
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnModel_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IReservaService>();

            faker.Setup(a => a.ObtenerListaReservas()).Returns(
                new List<Reserva> { });
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<List<Reserva>>(view.Model);
        }
        [Test]
        public void Return_UnaListaServicios_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();

            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio> {});

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Crear() as ViewResult;

            Assert.IsInstanceOf<List<Servicio>>(view.ViewBag.Servicios);
        }
        [Test]
        public void ReturnViewBag_ListaServicios_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();

            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio> { });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Crear() as ViewResult;
            Assert.IsInstanceOf<List<Servicio>>(view.ViewBag.Servicios);
        }
        [Test]
        public void ReturnMisReservasConUsuarioNoLogueado_MisReservasTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IReservaService>();
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("");

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MisReservas();

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnMisReservasConUsuarioLogueado_MisReservasTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IReservaService>();

            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListaReservas()).Returns(new List<Reserva> { });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MisReservas();

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerificarCuantasReservasTieneUnUsuarioEnviandoId_MisReservasTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IReservaService>();
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);

            faker.Setup(a => a.ObtenerListaReservas()).Returns(new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Now,IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Now,IdUsuario=4,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Now,IdUsuario=3,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Now,IdUsuario=2,Total=40,Activo_Inactivo=true}
            });
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MisReservas() as ViewResult;
            var model = view.Model as List<Reserva>;

            Assert.AreEqual(2, model.Count);
        }
        [Test]
        public void ReturnModel_MisReservasTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IReservaService>();
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);

            faker.Setup(a => a.ObtenerListaReservas()).Returns(new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Now,IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Now,IdUsuario=4,Total=25,Activo_Inactivo=true}
            });
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MisReservas() as ViewResult;

            Assert.IsInstanceOf<List<Reserva>>(view.Model);
        }
        [Test]
        public void ReturnBuscarMisReservasConUsuarioNoLogueado_MisReservasTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IReservaService>();
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("");
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MisReservas();

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnBuscarMisReservasDeUnaFechaEspecificada_BuscarMisReservasTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();

            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListaReservas()).Returns(
                new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("16/05/2020 12:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Parse("14/05/2020 12:00:00 a.m."),IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Parse("16/05/2020 12:00:00 a.m."),IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Parse("12/05/2020 12:00:00 a.m."),IdUsuario=2,Total=40,Activo_Inactivo=true}
                });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.BuscarMisReservas(DateTime.Parse("16/05/2020 12:00:00 a.m.").Date) as ViewResult;
            var model = view.Model as List<Reserva>;
            Assert.AreEqual(2, model.Count);
        }
        [Test]
        public void ReturnBuscarMisReservasDeUnaFechaNull_BuscarMisReservasTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();

            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListaReservas()).Returns(
                new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("16/05/2020 12:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Parse("14/05/2020 12:00:00 a.m."),IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Parse("16/05/2020 12:00:00 a.m."),IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Parse("12/05/2020 12:00:00 a.m."),IdUsuario=2,Total=40,Activo_Inactivo=true}
                });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.BuscarMisReservas(DateTime.Parse("01/01/0001 12:00:00 a.m.")) as ViewResult;
            var model = view.Model as List<Reserva>;
            Assert.AreEqual(4, model.Count);
        }
        [Test]
        public void ReturnIdNullEliminar_EliminarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Eliminar(null);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void VerificarSiEliminoCorrectamente_EliminarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();

            faker.Setup(a => a.ObtenerDetalleReserva()).Returns(
                 new List<DetalleReserva> {
                    new DetalleReserva{IdDetalleReserva=1, IdReserva=2,Fecha=DateTime.Parse("16/05/2020 12:00:00"),Cantidad=2,Activo_Inactivo=true},
                    new DetalleReserva{IdDetalleReserva=1, IdReserva=2,Fecha=DateTime.Parse("14/05/2020 12:00:00"),Cantidad=2,Activo_Inactivo=true},
                    new DetalleReserva{IdDetalleReserva=1, IdReserva=3,Fecha=DateTime.Parse("16/05/2020 12:00:00"),Cantidad=2,Activo_Inactivo=true},
                    new DetalleReserva{IdDetalleReserva=1, IdReserva=4,Fecha=DateTime.Parse("12/05/2020 12:00:00"),Cantidad=2,Activo_Inactivo=true}

                 });

            faker.Setup(a => a.ObtenerListaReservas()).Returns(
                new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("16/05/2020 12:00:00"),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Parse("14/05/2020 12:00:00"),IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Parse("16/05/2020 12:00:00"),IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Parse("12/05/2020 12:00:00"),IdUsuario=2,Total=40,Activo_Inactivo=true}
                });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Eliminar(2);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }

        [Test]
        public void VerificarQueNoSecreoLaReserva_CrearReservaPost()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IReservaService>();

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Crear(null);

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearReservaCorrectamente_CrearReservaPost()
        {
            var reserva = new Reserva { IdUsuario = 1, IdModoPago = 1, Fecha = DateTime.Now, Total = 100, Activo_Inactivo=true };
            var detalleReservas=new List<DetalleReserva> { 
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 2, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 2, Fecha = DateTime.Parse("14/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 3, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 4, Fecha = DateTime.Parse("12/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true }
            };
            reserva.DetalleReservas = detalleReservas;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            var faker = new Mock<IReservaService>();

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Crear(reserva);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void Return_Verificarsisecreo_CrearReservaPost()
        {
            var reserva = new Reserva { IdUsuario = 1, IdModoPago = 1, Fecha = DateTime.Now, Total = 100, Activo_Inactivo = true };
            var detalleReservas = new List<DetalleReserva> {
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 2, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 2, Fecha = DateTime.Parse("14/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 3, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 4, Fecha = DateTime.Parse("12/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true }
            };
            reserva.DetalleReservas = detalleReservas;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            var faker = new Mock<IReservaService>();

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Crear(reserva) as RedirectToRouteResult;
            var b = view.RouteValues.Values;
            var convertirALista = b.ToList();

            Assert.AreEqual("MisReservas", convertirALista[1]);
            Assert.AreEqual("Reserva", convertirALista[2]);
        }
        [Test]
        public void ReturnInstance_MiDetalleReservaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerDetalleReserva()).Returns(
                new List<DetalleReserva> {
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 2, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 2, IdReserva = 2, Fecha = DateTime.Parse("14/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 3, IdReserva = 3, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 4, IdReserva = 4, Fecha = DateTime.Parse("12/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true }
            });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MiDetalleReserva(2);

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ContarReservasDeEsteUsuario_MiDetalleReservaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerDetalleReserva()).Returns(
                new List<DetalleReserva> {
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 2, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 2, IdReserva = 2, Fecha = DateTime.Parse("14/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 3, IdReserva = 3, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 4, IdReserva = 4, Fecha = DateTime.Parse("12/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true }
            });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MiDetalleReserva(2) as ViewResult;
            var model=view.Model as List<DetalleReserva>;

            Assert.AreEqual(2, model.Count);
        }
        [Test]
        public void ReturnModel_MiDetalleReservaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerDetalleReserva()).Returns(
                new List<DetalleReserva> {
                    new DetalleReserva { IdDetalleReserva = 1, IdReserva = 2, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 2, IdReserva = 2, Fecha = DateTime.Parse("14/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 3, IdReserva = 3, Fecha = DateTime.Parse("16/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true },
                    new DetalleReserva { IdDetalleReserva = 4, IdReserva = 4, Fecha = DateTime.Parse("12/05/2020 12:00:00"), Cantidad = 2, Activo_Inactivo = true }
            });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MiDetalleReserva(2) as ViewResult;

            Assert.IsInstanceOf<List<DetalleReserva>>(view.Model);
        }
        [Test]
        public void ReturnInstance_ObtenerTunosTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaTurnos()).Returns(new List<Turno>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=3,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerTurnos(2) as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnInstanceModel_ObtenerTunosTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaTurnos()).Returns(new List<Turno>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=3,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerTurnos(2) as ViewResult;

            Assert.IsInstanceOf<List<Turno>>(view.Model);
        }
        [Test]
        public void BuscarSiUsuarioSiExiste_BuscarUsuarioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
             });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.BuscarUsuario("11111111");

            Assert.AreEqual("Jose Luis Diaz Ruiz", view);
        }
        [Test]
        public void BuscarSiUsuarioNoExiste_BuscarUsuarioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "66556655", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
             });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.BuscarUsuario("11111111");

            Assert.AreEqual("noexiste", view);
        }
    }
}
