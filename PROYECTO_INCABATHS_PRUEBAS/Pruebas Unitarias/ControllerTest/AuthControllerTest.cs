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
    }
}
