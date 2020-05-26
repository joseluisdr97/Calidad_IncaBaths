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

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.AreEqual(4, model.Count);
        }
        [Test]
        public void VerificarViewBagServiciosRetornaUnaListaServicios_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();

            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio> {});

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Crear() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
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
                    new DetalleReserva{IdDetalleReserva=1, IdReserva=2,Fecha=DateTime.Parse("16/05/2020 12:00:00 a.m."),Cantidad=2,Activo_Inactivo=true},
                    new DetalleReserva{IdDetalleReserva=1, IdReserva=2,Fecha=DateTime.Parse("14/05/2020 12:00:00 a.m."),Cantidad=2,Activo_Inactivo=true},
                    new DetalleReserva{IdDetalleReserva=1, IdReserva=3,Fecha=DateTime.Parse("16/05/2020 12:00:00 a.m."),Cantidad=2,Activo_Inactivo=true},
                    new DetalleReserva{IdDetalleReserva=1, IdReserva=4,Fecha=DateTime.Parse("12/05/2020 12:00:00 a.m."),Cantidad=2,Activo_Inactivo=true}

                 });

            faker.Setup(a => a.ObtenerListaReservas()).Returns(
                new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("16/05/2020 12:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Parse("14/05/2020 12:00:00 a.m."),IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Parse("16/05/2020 12:00:00 a.m."),IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Parse("12/05/2020 12:00:00 a.m."),IdUsuario=2,Total=40,Activo_Inactivo=true}
                });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Eliminar(2) as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
        }
    }
}
