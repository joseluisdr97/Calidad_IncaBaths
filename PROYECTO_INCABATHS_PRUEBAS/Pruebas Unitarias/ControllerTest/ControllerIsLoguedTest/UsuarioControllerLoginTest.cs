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
    class UsuarioControllerLoginTest
    {
        [Test]
        public void VerificarUsuarioLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
                });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Index();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerificarUsuarioNoLogueado_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker

            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Index();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void VerificarCrearUsuarioLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaDeTipoUsuarios()).Returns(new List<TipoUsuario> {
                new TipoUsuario{IdTipoUsuario=1,Nombre="Administrador"},
                new TipoUsuario{IdTipoUsuario=2,Nombre="Cajero"},
                new TipoUsuario{IdTipoUsuario=3,Nombre="Cliente"}
                });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Crear();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerificarCrearUsuarioNoLogueado_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Crear();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void VerificarEditarUsuarioNoLogueado_EditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(2);
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void VerificarEditarUsuarioLogueado_EditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerUsuarioPorId(2)).Returns(new Usuario {IdUsuario=2, Nombre="Jose", DNI="11111111" });
            faker.Setup(a => a.ObtenerListaDeTipoUsuarios()).Returns(new List<TipoUsuario> {
                new TipoUsuario{IdTipoUsuario=1,Nombre="Administrador"},
                new TipoUsuario{IdTipoUsuario=2,Nombre="Cajero"},
                new TipoUsuario{IdTipoUsuario=3,Nombre="Cliente"}
                });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(2);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerMiCuentaUsuarioLogueado_VerMiCuentaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            fakerSession.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerUsuarioPorId(2)).Returns(new Usuario { IdUsuario = 2, Nombre = "Jose", DNI = "11111111" });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuenta();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerMiCuentaUsuarioNoLogueado_VerMiCuentaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(false);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuenta();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ActualizarDatosClienteLogueado_ActualizarDatosUClienteTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> { new Usuario { IdUsuario = 2, Nombre = "Jose", DNI = "11111111" } });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUCliente();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ActualizarDatosClienteNoLogueado_ActualizarDatosUClienteTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(false);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUCliente();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void CambiarContraUsuarioLogueado_CambiarContraUsuarioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.CambiarContraUsuario();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CambiarContraUsuarioNoLogueado_CambiarContraUsuarioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(false);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.CambiarContraUsuario();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void VerMiCuentaUsuarioLogueadoAdmin_VerMiCuentaAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> { new Usuario { IdUsuario = 2, Nombre = "Jose", DNI = "11111111" } });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuentaAdmin();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void VerMiCuentaUsuarioNoLogueadoAdmin_VerMiCuentaAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuentaAdmin();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ActualizarDatosUAdminLogueadoAdmin_ActualizarDatosUAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> { new Usuario { IdUsuario = 2, Nombre = "Jose", DNI = "11111111" } });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUAdmin();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ActualizarDatosUAdminNoLogueadoAdmin_ActualizarDatosUAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUAdmin();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void CambiarContraUsuarioAdminLogueado_CambiarContraUsuarioAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.CambiarContraUsuarioAdmin();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CambiarContraUsuarioAdminNoLogueado_CambiarContraUsuarioAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(false);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.CambiarContraUsuarioAdmin();
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
    }
}
