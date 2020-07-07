using Moq;
using NUnit.Framework;
using PROYECTO_INCABATHS;
using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.Controllers;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS_PRUEBAS.ControllerTest.ControllerIsLoguedTest
{
    [TestFixture]
    class ReservaControllerLoginTest
    {
        [Test]
        public void VerificarUsuarioLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IReservaService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaReservas()).Returns(new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Now,IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Now,IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Now,IdUsuario=2,Total=40,Activo_Inactivo=true}
                });
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Index();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerificarUsuarioNoLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IReservaService>();//ESto reeplaza a la creacion de la clase faker
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Index();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void VerificarMisReservasLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            var faker = new Mock<IReservaService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaReservas()).Returns(new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Now,IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Now,IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Now,IdUsuario=2,Total=40,Activo_Inactivo=true}
                });
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MisReservas();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerificarMisReservasNoLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(false);
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            var faker = new Mock<IReservaService>();//ESto reeplaza a la creacion de la clase faker
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.MisReservas();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnInstance_ObtenerTunosLogueadoTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaTurnos()).Returns(new List<Turno>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=3,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerTurnos(2);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnInstance_ObtenerTunosNoLogueadoTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaTurnos()).Returns(new List<Turno>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=3,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerTurnos(2);

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnInstanceUsuarioLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio> { });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Crear();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnInstanceUsuarioNoLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(false);
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio> { });

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.Crear();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnInstanceUsuarioLogueado_ObtenerTunosTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IReservaService>();
            faker.Setup(a => a.ObtenerListaTurnos()).Returns(new List<Turno>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=3,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerTurnos(2);

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnInstanceUsuarioNoLogueado_ObtenerTunosTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IReservaService>();
            var controller = new ReservaController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerTurnos(2);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
    }
}
