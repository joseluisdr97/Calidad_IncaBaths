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
using System.Web;
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
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
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
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
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
        public void ReturnInstance_BuscarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true }});
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.BuscarUsuario(null);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnModelInstance_BuscarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario> {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true }});
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.BuscarUsuario(null) as ViewResult;
            Assert.IsInstanceOf<List<Usuario>>(view.Model);
        }
        [Test]
        public void ContarTiposDeUsuarioCrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaDeTipoUsuarios()).Returns(new List<TipoUsuario> {
                new TipoUsuario{IdTipoUsuario=1,Nombre="Administrador"},
                new TipoUsuario{IdTipoUsuario=3,Nombre="Cliente"}
                });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Crear() as ViewResult;
            Assert.AreEqual(2, (view.ViewBag.TipoUsuarios as List<TipoUsuario>).Count);
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
        public void ReturnInstance_CrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaDeTipoUsuarios()).Returns(new List<TipoUsuario> {
                new TipoUsuario{IdTipoUsuario=3,Nombre="Cliente"}
                });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Crear() as ViewResult;
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearUsuarioDatosNoValidos_CrearPostTest()
        {
            var usuario= new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = null, Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);

            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaDeTipoUsuarios()).Returns(new List<TipoUsuario> {
                new TipoUsuario{IdTipoUsuario=3,Nombre="Cliente"}
                });
            faker.Setup(a => a.Existe(new Usuario { IdUsuario=1, Nombre="Jose", Apellido="Diaz"},1)).Returns(1);
            faker.Setup(a=>a.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerUsuarioPorId(1)).Returns(new Usuario { IdUsuario = 1, Nombre = "Jose", Apellido = "Diaz" });

            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Crear(usuario,"123", file) as ViewResult;
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnModel_CrearUsuarioDatosNoValidos_CrearPostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = null, Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);

            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaDeTipoUsuarios()).Returns(new List<TipoUsuario> {
                new TipoUsuario{IdTipoUsuario=3,Nombre="Cliente"}
                });
            faker.Setup(a => a.Existe(new Usuario { IdUsuario = 1, Nombre = "Jose", Apellido = "Diaz" }, 1)).Returns(1);
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerUsuarioPorId(1)).Returns(new Usuario { IdUsuario = 1, Nombre = "Jose", Apellido = "Diaz" });

            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Crear(usuario, "123", file) as ViewResult;
            Assert.IsInstanceOf<Usuario>(view.Model);
        }
        [Test]
        public void ReturnModel_EditarUsuarioDatosNoValidos_EditarPostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = null, Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = null, Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);

            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaDeTipoUsuarios()).Returns(new List<TipoUsuario> {
                new TipoUsuario{IdTipoUsuario=3,Nombre="Cliente"}
                });
            faker.Setup(a => a.Existe(new Usuario { IdUsuario = 1, Nombre = "Jose", Apellido = "Diaz" }, 1)).Returns(1);
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerUsuarioPorId(1)).Returns(new Usuario { IdUsuario = 1, Nombre = "Jose", Apellido = "Diaz" });

            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(usuario,1, file) as ViewResult;
            Assert.IsInstanceOf<Usuario>(view.Model);
        }
        [Test]
        public void Return_EditarUsuarioDatosNoValidos_EditarPostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = null, Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = null, Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);

            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaDeTipoUsuarios()).Returns(new List<TipoUsuario> {
                new TipoUsuario{IdTipoUsuario=3,Nombre="Cliente"}
                });
            faker.Setup(a => a.Existe(new Usuario { IdUsuario = 1, Nombre = "Jose", Apellido = "Diaz" }, 1)).Returns(1);
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerUsuarioPorId(1)).Returns(new Usuario { IdUsuario = 1, Nombre = "Jose", Apellido = "Diaz" });

            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(usuario, 1, file);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void Return_ComprabarEliminar_EliminarTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = null, Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = null, Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.Eliminar(1);
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void Return_VerMiCuenta_VerMiCuentaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerUsuarioPorId(1)).Returns(new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = null, Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = null, Password = "123", Activo_Inactivo = true });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuenta();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnInstance_VerMiCuenta_VerMiCuentaAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuentaAdmin();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnModelInstance_VerMiCuenta_VerMiCuentaAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuentaAdmin() as ViewResult;
            Assert.IsInstanceOf<Usuario>(view.Model);
        }
        [Test]
        public void ReturnInstance_ActualizarDatosAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUAdmin();
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnModelInstance_ActualizarDatosAdminTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUAdmin() as ViewResult;
            Assert.IsInstanceOf<Usuario>(view.Model);
        }
        [Test]
        public void ReturnModel_VerMiCuenta_VerMiCuentaTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerUsuarioPorId(1)).Returns(new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = null, Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = null, Password = "123", Activo_Inactivo = true });
            faker.Setup(a => a.BuscarIdUsuarioSession()).Returns(1);
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.VerMiCuenta() as ViewResult;
            Assert.IsInstanceOf<Usuario>(view.Model);
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
        public void ReturnInstance_ActualizarDatosUClienteConDatosNoValidos_PostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUCliente(usuario,file);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnInstance_ActualizarDatosUAdminConDatosNoValidos_PostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUAdmin(usuario, file);
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnModelInstance_ActualizarDatosUAdminConDatosNoValidos_PostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUAdmin(usuario, file) as ViewResult;
            Assert.IsInstanceOf<Usuario>(view.Model);
        }
        [Test]
        public void ReturnInstanceDatosNoValidos_CambiarContraUadmin_PostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.CambiarContraUsuarioAdmin(usuario, null,"123");
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnModelInstanceDatosNoValidos_CambiarContraUadmin_PostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.CambiarContraUsuarioAdmin(usuario, null, "123") as ViewResult;
            Assert.IsInstanceOf<Usuario>(view.Model);
        }
        [Test]
        public void ReturnInstanceDatosValidos_CambiarContraUadmin_PostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.Existe(usuario, 1)).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.CambiarContraUsuarioAdmin(usuario, "123", "123");
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnModelInstanceDatosValidos_CambiarContraUadmin_PostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.Existe(usuario, 1)).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true },
                new Usuario { IdUsuario = 3, IdTipoUsuario = 1, Nombre = "Gaby", Apellido = "Aaaa Baaa", DNI = "22332233", Celular = "921472540", Direccion = "Los fresnos", Correo = "gaby@gmail,com", Password = "223", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.CambiarContraUsuarioAdmin(usuario, "123", "123") as RedirectToRouteResult;
            var b = view.RouteValues.Values;
            var convertirALista = b.ToList();

            Assert.AreEqual("Logout", convertirALista[0]);
            Assert.AreEqual("Auth", convertirALista[1]);
        }
        [Test]
        public void ReturnInstanceModel_ActualizarDatosUClienteConDatosNoValidos_PostTest()
        {
            var usuario = new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = null, Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true };
            HttpPostedFileBase file = null;
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoCliente()).Returns(true);
            var faker = new Mock<IUsuarioService>();
            faker.Setup(s => s.BuscarIdUsuarioSession()).Returns(1);
            faker.Setup(a => a.ObtenerListaUsuarios()).Returns(new List<Usuario>
            {
                new Usuario { IdUsuario = 1, IdTipoUsuario = 1, Nombre = "Jose Luis", Apellido = "Diaz Ruiz", DNI = "11111111", Celular = "921472548", Direccion = "Jr Chepen", Correo = "admin1@gmail.com", Password = "123", Activo_Inactivo = true },
                new Usuario { IdUsuario = 2, IdTipoUsuario = 1, Nombre = "Anais", Apellido = "Sanchez Carrera", DNI = "22222222", Celular = "921472549", Direccion = "Chilete", Correo = "anais@gmail.com", Password = "333", Activo_Inactivo = true }
            });
            var controller = new UsuarioController(faker.Object, fakerSession.Object);
            var view = controller.ActualizarDatosUCliente(usuario, file) as ViewResult;
            Assert.IsInstanceOf<Usuario>(view.Model);
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
