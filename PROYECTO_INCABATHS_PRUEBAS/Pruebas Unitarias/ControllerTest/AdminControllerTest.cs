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
    class AdminControllerTest
    {
        [Test]
        public void ContarGananciasDelDiaIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);

            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> { new Usuario { IdUsuario = 2, Nombre = "Jose", Perfil="hola",DNI="11111111",IdTipoUsuario=1 } });
            faker.Setup(a => a.ObtenerListReservas()).Returns(
                new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Now,IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Now,IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Now,IdUsuario=2,Total=40,Activo_Inactivo=true}
                });

            var controller = new AdminController(faker.Object,fakerSession.Object);
            var view = controller.Index() as ViewResult;

            Assert.AreEqual(95, view.ViewBag.GanaciasDelDia);
        }
        [Test]
        public void ContarGananciasDeDiaSinTenerDatosIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);

            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> { new Usuario { IdUsuario = 2, Nombre = "Jose", Perfil = "hola", DNI = "11111111", IdTipoUsuario = 1 } });
            faker.Setup(a => a.ObtenerListReservas()).Returns(new List<Reserva> {});

            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Index() as ViewResult;

            Assert.AreEqual(0, view.ViewBag.GanaciasDelDia);
        }
        [Test]
        public void ContarGananciasUsuarioSinLoguearIndexTest()
        {

            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("");

            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Index();

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ContarGananciasPorRangoFechaIndexTest()
        {

            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            faker.Setup(a => a.ObtenerListReservas()).Returns(
                new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("13/05/2020 09:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Parse("14/05/2020 08:00:00 a.m."),IdUsuario=3,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Parse("15/05/2020 11:00:00 a.m."),IdUsuario=4,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Now,IdUsuario=2,Total=40,Activo_Inactivo=true}
                });

            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.CalcularGanancia(DateTime.Parse("11/05/2020 12:00:00 a.m."), DateTime.Parse("16/05/2020 12:00:00 a.m."));

            Assert.AreEqual(75,view);
        }
        [Test]
        public void ComprobarSiElUsuarioEstaLogueadoPruebaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> { new Usuario { IdUsuario = 2, Nombre = "Jose", Perfil = "hola", DNI = "11111111", IdTipoUsuario = 1 } });
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListReservas()).Returns(
                new List<Reserva> {});
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Prueba() as ViewResult;

            Assert.AreEqual(true, view.Model);
        }
        [Test]
        public void ComprobarSiElUsuarioNoEstáLogueadoPruebaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> { new Usuario { IdUsuario = 2, Nombre = "Jose", Perfil = "hola", DNI = "11111111", IdTipoUsuario = 1 } });
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListReservas()).Returns(
                new List<Reserva> { });
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Prueba() as ViewResult;

            Assert.AreEqual(false, view.Model);
        }

        [Test]
        public void ConsultarAforoDeTurnoEnviandoServicioNull()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.ConsultarAforoDeTurno(DateTime.Parse("18/05/2020 12:00:00 a.m."), null,new TimeSpan(0, 10, 30, 0),new TimeSpan(0, 10, 30, 0));

            Assert.AreEqual(0, view);
        }

        //[Test]
        //public void ConsultarAforoDeTurnoEnviandoDatosValidos()
        //{
        //    var fakerSession = new Mock<IServiceSession>();
        //    var faker = new Mock<IAdminService>();
        //    faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio>
        //    {
        //        new Servicio{IdServicio=1, Nombre="Sauna", Precio=4, Aforo=10, Activo_Inactivo=true}
        //    });
        //    faker.Setup(a => a.ObtenerListaTurnos()).Returns(new List<Turno>
        //    {
        //        new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
        //        new Turno{IdTurno=2, IdServicio=3,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
        //        new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
        //    });
        //    var controller = new AdminController(faker.Object, fakerSession.Object);
        //    var view = controller.ConsultarAforoDeTurno(DateTime.Parse("18/05/2020 12:00:00 a.m."), null, new TimeSpan(0, 10, 30, 0), new TimeSpan(0, 10, 30, 0));

        //    Assert.AreEqual(0, view);
        //}
        [Test]
        public void ReturnListaTipoUsuariosCrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            faker.Setup(a => a.ObtenerListaTipoUsuarios()).Returns(
                new List<TipoUsuario> {});
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Crear() as ViewResult;

            Assert.IsInstanceOf<List<TipoUsuario>>(view.ViewBag.TipoUsuarios);
        }
        [Test]
        public void ReturnNuevoUsuarioCrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            faker.Setup(a => a.ObtenerListaTipoUsuarios()).Returns(new List<TipoUsuario> { });
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Crear() as ViewResult;

            Assert.IsInstanceOf<Usuario>(view.Model);
        }
        [Test]
        public void ReturnViewBagListaDeServiciosServicioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio> { });
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Servicio() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsInstanceOf<List<Servicio>>(view.ViewBag.Servicios);
        }
        [Test]
        public void BuscarServicioConUsuarioNull_BuscarServicioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.BuscarServicio(null, DateTime.Parse("15/05/2020 11:00:00 a.m."));

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void RetornarTurnoConElIdUsuarioIndicado_BuscarServicioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            faker.Setup(a => a.ObtenerListaTurnos()).Returns(new List<Turno> {
             new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m.").Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m.").Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            });
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.BuscarServicio(2, DateTime.Parse("18/05/2020 12:00:00 a.m.").Date) as ViewResult;
            var model = view.Model as List<Turno>;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.AreEqual(3, model[1].IdTurno);
        }
        [Test]
        public void ReturnEnviandoIdServicioNull_ObtenerServicioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerPrecioServicio(null);

            Assert.AreEqual(0,view);
        }
        [Test]
        public void ReturnEnviandoIdServicio_ObtenerNombreServicioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerNombreServicio(null);
            Assert.AreEqual("", view);
        }
    }
}
