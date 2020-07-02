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

namespace PROYECTO_INCABATHS_PRUEBAS.ControllerTest
{
    [TestFixture]
    class ServicioControllerTest
    {
        [Test]
        public void ReturnInstance_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Servicio{IdServicio=3, Nombre="Masajes", Precio=10, Aforo=30},
                new Servicio{IdServicio=4, Nombre="Hidromasajes", Precio=3.5m, Aforo=25}
            });

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Index();

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ReturnInstanceModel_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Servicio{IdServicio=3, Nombre="Masajes", Precio=10, Aforo=30},
                new Servicio{IdServicio=4, Nombre="Hidromasajes", Precio=3.5m, Aforo=25}
            });

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<List<Servicio>>(view.Model);
        }
        [Test]
        public void ReturnModel_IndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Servicio{IdServicio=4, Nombre="Hidromasajes", Precio=3.5m, Aforo=25}
            });

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Index() as ViewResult;

            Assert.AreEqual(new List<Servicio>(), view.Model);
        }
        [Test]
        public void ReturnInstanceModel_BuscarServicioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IServicioService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerListaServicios()).Returns(new List<Servicio>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Servicio{IdServicio=1, Nombre="Sauna", Precio=4, Aforo=20},
                new Servicio{IdServicio=2, Nombre="Piscina", Precio=2, Aforo=100},
                new Servicio{IdServicio=3, Nombre="Masajes", Precio=10, Aforo=30},
                new Servicio{IdServicio=4, Nombre="Hidromasajes", Precio=3.5m, Aforo=25}
            }
            );

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.BuscarServicio("Duchas") as ViewResult;

            Assert.IsInstanceOf<List<Servicio>>(view.Model);
        }

        [Test]
        public void BuscarServicioEnviandoDatos_BuscarServicioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
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
            var model = view.Model as List<Servicio>;

            Assert.AreEqual(2, (view.Model as List<Servicio>).Count);
        }
        [Test]
        public void BuscarServicioEnviandoDatoNull_BuscarServicioTest()
        {
            var fakerSession = new Mock<IServiceSession>();
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
            var view = controller.BuscarServicio(null) as ViewResult;
            var model = view.Model as List<Servicio>;

            Assert.AreEqual(4, model.Count);
        }
        [Test]
        public void ReturnServicioEnvioPorIdNullTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio
            {
                IdServicio = 2,
                Nombre = "Piscina",
                Precio = 2,
                Aforo = 100
            }
            );

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(1) as ViewResult;

            Assert.IsNull(view.Model);
        }
        [Test]
        public void ReturnServicioConsultaDatosTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio
            {
                IdServicio = 2,
                Nombre = "Piscina",
                Precio = 2,
                Aforo = 100
            }
            );

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(2) as ViewResult;
            var model = view.Model as Servicio; //Convertir a lista

            Assert.AreEqual(100, model.Aforo);
        }

        [Test]
        public void ContarCuantosTurnosTieneUnServicio_EliminarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IServicioService>();
            faker.Setup(a => a.ListaDeTurnos()).Returns(new List<Turno>
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=2,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.EliminarServcicioP(2) as ViewResult;
            var model = view.Model as List<Turno>;

            Assert.AreEqual(2, model.Count);
        }
        [Test]
        public void EnviarServicioNullAListaDeTurnos()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IServicioService>();
            faker.Setup(a => a.ListaDeTurnos()).Returns(new List<Turno>
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=2,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.EliminarServcicioP(null) as ViewResult;

            Assert.AreEqual(new List<Turno>(), view.Model);
        }
        [Test]
        public void EnviarIdServicioNullEditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            faker.Setup(a => a.ObtenerServicioPorId(1)).Returns(new Servicio { IdServicio = 1, Nombre = "Duchas", Precio = 2, Aforo = 20 });
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(null);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void EnviarIdServicioNullEliminarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<IServicioService>();
            faker.Setup(a => a.ObtenerServicioPorId(1)).Returns(new Servicio { IdServicio = 1, Nombre = "Duchas", Precio = 2, Aforo = 20 });
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Eliminar(null);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void CrearServicioMethodPostDatosInvalidos_CrearServicioPost()
        {
            var servicio = new Servicio { IdServicio = 1, Nombre = null, Precio = 2, Aforo = 20 };
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Crear(servicio);

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearServicioMethodPostDatosValidos_CrearServicioPost()
        {
            var servicio = new Servicio { IdServicio = 1, Nombre = "Sauna", Precio = 2, Aforo = 20 };
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);

            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Crear(servicio);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void EditarServicioMethodPostDatosInvalidos_EditarServicioPost()
        {
            var servicio = new Servicio { IdServicio = 1, Nombre = null, Precio = 2, Aforo = 20 };
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(servicio, 1) as ViewResult;

            Assert.IsInstanceOf<Servicio>(view.Model);
        }
        [Test]
        public void EditarServicioMethodPostDatosValidos_EditarServicioPost()
        {
            var servicio = new Servicio { IdServicio = 1, Nombre = "Sauna", Precio = 2, Aforo = 20 };
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<IServicioService>();
            var controller = new ServicioController(faker.Object, fakerSession.Object);
            var view = controller.Editar(servicio, 1);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
    }
}
