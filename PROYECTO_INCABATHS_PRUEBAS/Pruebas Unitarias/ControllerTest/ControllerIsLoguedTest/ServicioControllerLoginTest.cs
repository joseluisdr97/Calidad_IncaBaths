using Moq;
using NUnit.Framework;
using PROYECTO_INCABATHS;
using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.Controllers;
using PROYECTO_INCABATHS.Interfaces;
using PROYECTO_INCABATHS.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS_PRUEBAS.ControllerTest.ControllerIsLoguedTest
{
    [TestFixture]
    class ServicioControllerLoginTest
    {
        [Test]
        public void VerificarUsuarioLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio> {
                new Servicio { IdServicio = 1, Nombre = "Sauna", Activo_Inactivo = true },
                new Servicio { IdServicio = 2, Nombre = "Piscina",  Activo_Inactivo = true },
                new Servicio { IdServicio = 3, Nombre = "Masajes", Activo_Inactivo = true }
                });
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Index();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerificarUsuarioNoLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Index();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void CrearUsuarioLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Crear();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearUsuarioNoLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Crear();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void EditarUsuarioLogueado_EditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { IdServicio = 2, Nombre = "Piscina", Activo_Inactivo = true });
    
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(2);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void EditarUsuarioNoLogueado_EditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IServicioService>();

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(2);
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void Return_BuscarServicioLogueadoTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio>
            {
                new Servicio{IdServicio=1, Nombre="Sauna", Precio=4, Aforo=20,Activo_Inactivo=true},
                new Servicio{IdServicio=2, Nombre="Piscina niños", Precio=2, Aforo=100,Activo_Inactivo=true},
                new Servicio{IdServicio=3, Nombre="Piscina Adultos", Precio=2, Aforo=100,Activo_Inactivo=true},
                new Servicio{IdServicio=4, Nombre="Masajes", Precio=10, Aforo=30,Activo_Inactivo=true},
            }
            );
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.BuscarServicio("Piscina") as ViewResult;
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void Return_BuscarServicioNoLogueadoTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.BuscarServicio("Piscina");
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnInstanceUsuarioLogueado_EditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio{IdServicio = 2,Nombre = "Piscina",Precio = 2,Aforo = 100});

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(1);

            Assert.IsInstanceOf< ViewResult>(view);
        }
        [Test]
        public void ReturnInstanceUsuarioNoLogueado_EditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(1);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
    }
}
