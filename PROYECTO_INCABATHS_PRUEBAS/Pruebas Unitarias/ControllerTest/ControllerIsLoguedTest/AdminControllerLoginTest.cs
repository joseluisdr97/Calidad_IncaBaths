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

namespace PROYECTO_INCABATHS_PRUEBAS.ControllerTest.ControllerIsLoguedTest
{
    [TestFixture]
    class AdminControllerLoginTest
    {
        [Test]
        public void VerificarSiNoEstaLogueadoIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);

            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Index();

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void VerificarSiEstaLogueadoIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAdminService>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);

            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> { new Usuario { IdUsuario = 2, Nombre = "Jose", Perfil = "hola", DNI = "11111111", IdTipoUsuario = 1 } });
            faker.Setup(a => a.ObtenerListReservas()).Returns(
                new List<Reserva> {
                    new Reserva{IdReserva=1,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),IdUsuario=2,Total=20,Activo_Inactivo=true},
                    new Reserva{IdReserva=2,Fecha=DateTime.Now,IdUsuario=2,Total=25,Activo_Inactivo=true},
                    new Reserva{IdReserva=3,Fecha=DateTime.Now,IdUsuario=2,Total=30,Activo_Inactivo=true},
                    new Reserva{IdReserva=4,Fecha=DateTime.Now,IdUsuario=2,Total=40,Activo_Inactivo=true}
                });

            var controller = new AdminController(faker.Object, fakerSession.Object);
            var view = controller.Index();

            Assert.IsInstanceOf<ViewResult>(view);

        }
        //[Test]
        //public void CrearUsuarioLogueado_CrearTest()
        //{
        //    var fakerSession = new Mock<IServiceSession>();
        //    var faker = new Mock<IAdminService>();
        //    fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
        //    faker.Setup(a => a.ObtenerListaTipoUsuarios()).Returns(
        //        new List<TipoUsuario> { });
        //    var controller = new AdminController(faker.Object, fakerSession.Object);
        //    var view = controller.Crear() as ViewResult;

        //    Assert.IsInstanceOf<ViewResult>(view);
        //}
        //[Test]
        //public void CrearUsuarioNoLogueado_CrearTest()
        //{
        //    var fakerSession = new Mock<IServiceSession>();
        //    var faker = new Mock<IAdminService>();
        //    fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
        //    var controller = new AdminController(faker.Object, fakerSession.Object);
        //    var view = controller.Crear();

        //    Assert.IsInstanceOf<RedirectToRouteResult>(view);
        //}
    }
}
