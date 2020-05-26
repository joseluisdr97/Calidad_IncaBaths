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
    class TurnoControllerLoginTest
    {
        [Test]
        public void VerificarUsuarioAdminLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<ITurnoService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerTurnos()).Returns(new List<Turno> {
                new Turno { IdTurno = 1, IdServicio = 1, Activo_Inactivo = true },
                new Turno { IdTurno = 2, IdServicio = 2, Activo_Inactivo = true },
                new Turno { IdTurno = 3, IdServicio = 1, Activo_Inactivo = true }
                });
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { IdServicio=2,Nombre="Sauna", Activo_Inactivo=true});
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Index(2);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerificarUsuarioAdminNoLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<ITurnoService>();//ESto reeplaza a la creacion de la clase faker
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Index(2);
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void CrearUsuarioLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<ITurnoService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { IdServicio = 2, Nombre = "Sauna", Activo_Inactivo = true });
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Crear(2);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearUsuarioNoLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<ITurnoService>();//ESto reeplaza a la creacion de la clase faker
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Crear(2);
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void EditarUsuarioLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<ITurnoService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { IdServicio = 2, Nombre = "Sauna", Activo_Inactivo = true });
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Crear(2);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void EditarUsuarioNoLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<ITurnoService>();
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Crear(2);
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
    }
}
