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
    class UsuarioControllerTest
    {
        [Test]
        public void ContarUsuariosEnMiListaIndexTest()
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
            var view = controller.Index() as ViewResult;
            var model = view.Model as List<Usuario>;
            Assert.AreEqual(3, model.Count);
        }
        [Test]
        public void BuscarUsuarioPorDniIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
                });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.BuscarUsuario("22222222") as ViewResult;
            var model = view.Model as List<Usuario>;
            Assert.AreEqual("22222222", model[0].DNI);
        }
        [Test]
        public void BuscarUsuarioEnviandoNullIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
                });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.BuscarUsuario(null) as ViewResult;
            var model = view.Model as List<Usuario>;
            Assert.AreEqual(3, model.Count);
        }
        [Test]
        public void ContarTiposDeUsuarioCrearTest()
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
            var view = controller.Crear() as ViewResult;
            Assert.AreEqual(3, (view.ViewBag.TipoUsuarios as List<TipoUsuario>).Count);
        }
        [Test]
        public void BuscarUnTiposDeUsuarioCrearTest()
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
            var view = controller.Crear() as ViewResult;
            Assert.AreEqual("Cajero", (view.ViewBag.TipoUsuarios as List<TipoUsuario>)[1].Nombre);
        }
        [Test]
        public void VerificarSiElUsuarioEstaActivoTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerUsuarioPorId(2)).Returns(new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuenta() as ViewResult;
            var model = view.Model as Usuario;
            Assert.AreEqual(true, model.Activo_Inactivo);
        }
        [Test]
        public void VerificarSiElUsuarioEstaInActivoTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerUsuarioPorId(2)).Returns(new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = false });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuenta() as ViewResult;
            var model = view.Model as Usuario;
            Assert.AreEqual(false, model.Activo_Inactivo);
        }
        [Test]
        public void VerificarSiElUsuarioEsTipoCliente()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerUsuarioPorId(2)).Returns(new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = false });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuenta() as ViewResult;
            var model = view.Model as Usuario;
            Assert.AreEqual(1, model.IdTipoUsuario);
        }

        [Test]
        public void BuscarSiLosDatosCoincidenDelUsuEnviado()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(2);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUCliente() as ViewResult;
            var model = view.Model as Usuario;
            Assert.AreEqual(2, model.IdUsuario);
            Assert.AreEqual(1, model.IdTipoUsuario);
            Assert.AreEqual("Anais", model.Nombre);
            Assert.AreEqual("22222222", model.DNI);
        }
        [Test]
        public void ActualizarDatosConUsuarioNoLogueado()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(0);
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUCliente() as ViewResult;
            var model = view.Model as Usuario;
            Assert.AreEqual(null, model);
        }
        [Test]
        public void ReturnCambiarContraUsuarioUsuarioNoLogueado()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var controller = new UsuarioController(null, fakerSession.Object);
            var view = controller.CambiarContraUsuarioAdmin() as ViewResult;
            Assert.AreEqual(null, (view.Model as Usuario).DNI);
        }
    }
}
