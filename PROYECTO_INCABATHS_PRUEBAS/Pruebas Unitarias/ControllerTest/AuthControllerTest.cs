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
    class AuthControllerTest
    {
        [Test]
        public void ComprobarSiElUsuarioEstaLogueadoPruebaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAuthService>();
            
            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("Jose");

            var controller = new AuthController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerUsuario();

            Assert.AreEqual("1", view);
        }
        [Test]
        public void ComprobarSiElUsuarioNoEstaLogueadoPruebaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAuthService>();

            fakerSession.Setup(a => a.BuscarNombreUsuarioSession()).Returns("");

            var controller = new AuthController(faker.Object, fakerSession.Object);
            var view = controller.ObtenerUsuario();

            Assert.AreEqual("null", view);
        }
        [Test]
        public void ComprobarSiElUsuarioIngresadoExisteAuthTest()
        {
            var usuario = new Usuario { IdUsuario = 1, Nombre = "Jose Luis", Correo = "jose@gmail.com", Password = "1111", IdTipoUsuario = 1, Activo_Inactivo = true };
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAuthService>();
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario{IdUsuario=1, Nombre="Jose Luis", Correo="jose@gmail.com", Password="1111", IdTipoUsuario=1, Activo_Inactivo=true},
                new Usuario{IdUsuario=2, Nombre="Anais", Correo="anais@gmail.com", Password="2222", IdTipoUsuario=3, Activo_Inactivo=true},
                new Usuario{IdUsuario=3, Nombre="Rosa", Correo="rosa@gmail.com", Password="3333", IdTipoUsuario=1, Activo_Inactivo=true},
                new Usuario{IdUsuario=4, Nombre="Gaby", Correo="gaby@gmail.com", Password="4444", IdTipoUsuario=3, Activo_Inactivo=true}
            });

            var controller = new AuthController(faker.Object, fakerSession.Object);
            var view = controller.Login(usuario, "1111");

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ComprobarSiElUsuarioIngresadoNoExisteAuthTest()
        {
            var usuario = new Usuario { IdUsuario = 1, Nombre = "Jose Luis", Correo = "juan@gmail.com", Password = "1111", IdTipoUsuario = 1, Activo_Inactivo = true };
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IAuthService>();
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario{IdUsuario=1, Nombre="Jose Luis", Correo="jose@gmail.com", Password="1111", IdTipoUsuario=1, Activo_Inactivo=true},
                new Usuario{IdUsuario=2, Nombre="Anais", Correo="anais@gmail.com", Password="2222", IdTipoUsuario=3, Activo_Inactivo=true},
                new Usuario{IdUsuario=3, Nombre="Rosa", Correo="rosa@gmail.com", Password="3333", IdTipoUsuario=1, Activo_Inactivo=true},
                new Usuario{IdUsuario=4, Nombre="Gaby", Correo="gaby@gmail.com", Password="4444", IdTipoUsuario=3, Activo_Inactivo=true}
            });

            var controller = new AuthController(faker.Object, fakerSession.Object);
            var view = controller.Login(usuario, null);

            Assert.IsInstanceOf<ViewResult>(view);
        }
    }
}
