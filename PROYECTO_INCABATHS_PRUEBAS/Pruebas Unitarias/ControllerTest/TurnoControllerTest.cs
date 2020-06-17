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
    class TurnoControllerTest
    {
        [Test]
        public void ContarTurnosDeUnServicioEnviandoFechaIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<ITurnoService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { IdServicio = 2, Nombre = "Piscina", Aforo = 20 });

            faker.Setup(a => a.ObtenerTurnos()).Returns(new List<Turno>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=3,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.BuscarTurno(DateTime.Parse("18/05/2020 12:00:00 a.m."), 2) as ViewResult;
            var model = view.Model as List<Turno>;

            Assert.AreEqual(1,model.Count);
        }

        [Test]
        public void ContarTurnosDeUnServicioEnviandoFechaNullIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<ITurnoService>();//ESto reeplaza a la creacion de la clase faker
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { IdServicio = 2, Nombre = "Piscina", Aforo = 20 });

            faker.Setup(a => a.ObtenerTurnos()).Returns(new List<Turno>//Se pone el metodo al que quiero llamar y se pone lo que nosotros queremos retornar
            {
                new Turno{IdTurno=1, IdServicio=1,Fecha=DateTime.Now.Date,HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=2, IdServicio=2,Fecha=DateTime.Parse("20/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true},
                new Turno{IdTurno=3, IdServicio=2,Fecha=DateTime.Parse("18/05/2020 12:00:00 a.m."),HoraInicio=new TimeSpan(0, 10, 30,0),HoraFin=new TimeSpan(0, 10, 30,0),Activo_Inactivo=true}
            }
            );

            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.BuscarTurno(null, 2) as ViewResult;
            var model = view.Model as List<Turno>;

            Assert.AreEqual(2, model.Count);
        }
        [Test]
        public void ReturnEnviandoIdServicioNullIndexTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<ITurnoService>();
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio {});
            faker.Setup(a => a.ObtenerTurnos()).Returns(new List<Turno>{}
            );
            var controller = new TurnoController(faker.Object,fakerSession.Object);
            var view = controller.Index(null);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnEnviandoIdServicioNullBuscarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<ITurnoService>();
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { });
            faker.Setup(a => a.ObtenerTurnos()).Returns(new List<Turno> { }
            );
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.BuscarTurno(null,null);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnEnviandoIdServicioNullEditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<ITurnoService>();
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { });
            faker.Setup(a => a.ObtenerTurnos()).Returns(new List<Turno> { }
            );
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Editar(null);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void ReturnEnviandoIdServicioNullEliminarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            var faker = new Mock<ITurnoService>();
            faker.Setup(a => a.ObtenerServicioPorId(2)).Returns(new Servicio { });
            faker.Setup(a => a.ObtenerTurnos()).Returns(new List<Turno> { }
            );
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.BuscarTurno(null, null);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void BuscarDatoServicioByIdCrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<ITurnoService>();
            faker.Setup(a => a.ObtenerServicioPorId(1)).Returns(new Servicio { IdServicio = 1, Nombre = "Duchas", Precio = 2, Aforo = 20 });
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Crear(1) as ViewResult;

            Assert.AreEqual("Duchas", view.ViewBag.Servicio.Nombre);
        }
        [Test]
        public void EnviarIdServicioNullCrearTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<ITurnoService>();
            faker.Setup(a => a.ObtenerServicioPorId(1)).Returns(new Servicio { IdServicio = 1, Nombre = "Duchas", Precio = 2, Aforo = 20 });
            var controller = new TurnoController(faker.Object, fakerSession.Object);
            var view = controller.Crear(null);

            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }

        [Test]
        public void BuscarDatoTurnoByIdEditarTest()
        {
            var fakerSession = new Mock<IServiceSession>();
            fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
            var faker = new Mock<ITurnoService>();
            faker.Setup(a => a.ObtenerTurnoPorId(1)).Returns(new Turno { IdTurno = 1, IdServicio = 1, Fecha = DateTime.Now.Date, HoraInicio = new TimeSpan(0, 10, 30, 0), HoraFin = new TimeSpan(0, 10, 30, 0), Activo_Inactivo = true });
            var controller = new TurnoController(faker.Object,fakerSession.Object);
            var view = controller.Editar(1) as ViewResult;
            var model = view.Model as Turno;

            Assert.AreEqual(1, model.IdServicio);
        }
        //[Test]
        //public void CrearTurnoDatosNoValidos_CrearPostTest()
        //{
        //    var Turno = new Turno  { IdTurno = 1,IdServicio=1, HoraInicio=new TimeSpan(0,10,30,0),HoraFin=new TimeSpan(0,11,0,0),Fecha=DateTime.Now.Date, Activo_Inactivo=true };
        //    var fakerSession = new Mock<IServiceSession>();
        //    fakerSession.Setup(a => a.EstaLogueadoComoAdministrador()).Returns(true);
        //    var faker = new Mock<ITurnoService>();
        //    var controller = new TurnoController(faker.Object, fakerSession.Object);
        //    var view = controller.Crear(Turno, 1) as ViewResult;

        //    Assert.IsInstanceOf<Turno>(view.Model);
        //}


    }
}
