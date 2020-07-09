using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PROYECTO_INCABATHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace PROYECTO_INCABATHS_PRUEBAS.SeleniumTest
{
    [TestFixture]
    class PruebasSelenium
    {
        //PRUEBAS LOGIN
        [Test]
        public void VerificarQueNoIngresoLogin()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            Thread.Sleep(2000);

            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("administrador");

            var boton = navegador.FindElement(By.CssSelector("#ingresar"));
            boton.Click();
            var buscarId = navegador.FindElement(By.CssSelector("#estoyenlogin"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void VerificarQueIngresoLoginUsuarioAdmin()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            Thread.Sleep(2000);

            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            var boton = navegador.FindElement(By.CssSelector("#ingresar"));
            boton.Click();
            Thread.Sleep(2000);
            var buscarId = navegador.FindElement(By.CssSelector("#EstoyEnIdexAdmin"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void VerificarQueIngresoLoginUsuarioCliente()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
                        Thread.Sleep(2000);

            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");

            var boton = navegador.FindElement(By.CssSelector("#ingresar"));
            boton.Click();
            Thread.Sleep(2000);
            var buscarId = navegador.FindElement(By.CssSelector("#EstoyEnServicioCliente"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void IngresoAPaginaLogin()
        {
            IWebDriver navegador = new FirefoxDriver();
            navegador.Url = "http://localhost:56854/Auth/Login";
            Assert.AreEqual("http://localhost:56854/Auth/Login", navegador.Url);
            navegador.Close();
        }
        [Test]
        public void RedireccionarACrearCuentaUsuario()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";
            var boton = chrome.FindElement(By.CssSelector("#NoTegoCuenta"));
            boton.Click();
            Thread.Sleep(2000);
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnCrearCliente"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAdminServicioIndex()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.FindElement(By.CssSelector("#Ser")).Click();
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnServicioIndex"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAdminUsuarioIndex()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.FindElement(By.CssSelector("#Usu")).Click();
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnUsuarioIndex"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAdminReservaIndex()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Reserva/Index";
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnReservaIndex"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }

        [Test]
        public void CrearServicioConDatosNullServicioCrear()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.FindElement(By.CssSelector("#Ser")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Crear";
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("10");
            inputs.FindElements(By.CssSelector("input"))[2].SendKeys("50");
            chrome.FindElement(By.CssSelector("#btnGuardar")).Click();

            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnCrearServicio"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void CrearServicio_ServicioCrear()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.FindElement(By.CssSelector("#Ser")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Crear";
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("PruebaCrear");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("10");
            inputs.FindElements(By.CssSelector("input"))[2].SendKeys("50");
            chrome.FindElement(By.CssSelector("#btnGuardar")).Click();

            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnServicioIndex"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void EditarServicioValoresNull_ServicioEditar()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.FindElement(By.CssSelector("#Ser")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Editar?id=7";
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].Clear();
            inputs.FindElements(By.CssSelector("input"))[1].Clear();
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("10");
            inputs.FindElements(By.CssSelector("input"))[2].Clear();
            inputs.FindElements(By.CssSelector("input"))[2].SendKeys("100");
            chrome.FindElement(By.CssSelector("#btnGuardar")).Click();

            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnServicioEditar"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void EditarServicio_ServicioEditar()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.FindElement(By.CssSelector("#Ser")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Editar?id=7";
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].Clear();
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("PruebaEditada");
            chrome.FindElement(By.CssSelector("#btnGuardar")).Click();
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnServicioIndex"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void IngresoCorrecto_TurnoCrear()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.FindElement(By.CssSelector("#Ser")).Click();
            chrome.Url = "http://localhost:56854/Turno/Index?id=7";
            chrome.Url = "http://localhost:56854/Turno/Crear?id=7";
            chrome.FindElement(By.CssSelector("#btnGuardar")).Click();

            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnTurnoCrear"));
            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        //POR ARREGLAR - ERROR FECHA
        [Test]
        public void CrearTurnoConDatosInvalidos_TurnoCrear()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Index";
            chrome.Url = "http://localhost:56854/Turno/Index?id=7";
            chrome.Url = "http://localhost:56854/Turno/Crear?id=7";
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("09:00:00");
            inputs.FindElements(By.CssSelector("input"))[2].SendKeys("09:00:00");
           chrome.FindElement(By.CssSelector("#btnGuardar")).Click();
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnTurnoCrear"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void CrearTurnoConDatosValidos_TurnoCrear()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Index";
            chrome.Url = "http://localhost:56854/Turno/Index?id=7";
            chrome.Url = "http://localhost:56854/Turno/Crear?id=7";
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("2021-05-05");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("01:00:00");
            inputs.FindElements(By.CssSelector("input"))[2].SendKeys("02:00:00");
            chrome.FindElement(By.CssSelector("#btnGuardar")).Click();

            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnTurnoIndex"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAEditarTurno_EditarTurno()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Index";
            chrome.Url = "http://localhost:56854/Turno/Index?id=7";
            chrome.Url = "http://localhost:56854/Turno/Editar?id=7";
  
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnEditarTurno"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void EditarTurnoConDatosInvalidos_EditarTurno()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Index";
            chrome.Url = "http://localhost:56854/Turno/Index?id=7";
            chrome.Url = "http://localhost:56854/Turno/Editar?id=1016";
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("");
            chrome.FindElement(By.CssSelector("#ConfirmarCambios")).Click();
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnEditarTurno"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void EditarTurnoConDatosValidos_EditarTurno()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Servicio/Index";
            chrome.Url = "http://localhost:56854/Turno/Index?id=7";
            chrome.Url = "http://localhost:56854/Turno/Editar?id=1016";
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("2021-01-02");
            chrome.FindElement(By.CssSelector("#ConfirmarCambios")).Click();
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnTurnoIndex"));

            Assert.IsNotNull(buscarId);
            //VOLVER A ESTADO NORMAL
            chrome.Url = "http://localhost:56854/Turno/Editar?id=1016";
            var inputss = chrome.FindElement(By.CssSelector("#inputs"));
            inputss.FindElements(By.CssSelector("input"))[0].SendKeys("2021-01-01");
            chrome.FindElement(By.CssSelector("#ConfirmarCambios")).Click();
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAVerMiCuentaUsuario()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuenta";
        
            var BuscarId=chrome.FindElement(By.CssSelector("#EstoyEnVerMiCuentaUsuarioCliente"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAActualizarContraseñaUsuarioCliente()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuenta";
            chrome.FindElement(By.CssSelector("#LinkCambiarContraseñaUsuarioCliente")).Click();
            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnCambiarContraUsuarioCliente"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void CambiarContraseñaUsuarioClienteConDatosInvalidos()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuenta";

            chrome.FindElement(By.CssSelector("#LinkCambiarContraseñaUsuarioCliente")).Click();
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("1234");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("12345");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("123456");

            chrome.FindElement(By.CssSelector("#ConfirmarCambioContraUsuarioCliente")).Click();
            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnCambiarContraUsuarioCliente"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void CambiarContraseñaUsuarioClienteConDatosValidos()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuenta";

            chrome.FindElement(By.CssSelector("#LinkCambiarContraseñaUsuarioCliente")).Click();
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("1234");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("12345");
            inputs.FindElements(By.CssSelector("input"))[2].SendKeys("12345");

            chrome.FindElement(By.CssSelector("#ConfirmarCambioContraUsuarioCliente")).Click();
            var BuscarId = chrome.FindElement(By.CssSelector("#estoyenlogin"));

            Assert.IsNotNull(BuscarId);

            //REESTABLECER DATOS
            chrome.FindElement(By.CssSelector("#correo")).Clear(); chrome.FindElement(By.CssSelector("#correo")).SendKeys("jose@gmail.com");
            chrome.FindElement(By.CssSelector("#contraseña")).Clear(); chrome.FindElement(By.CssSelector("#contraseña")).SendKeys("12345");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/CambiarContraUsuario";
            var inputs1 = chrome.FindElement(By.CssSelector("#inputs"));
            inputs1.FindElements(By.CssSelector("input"))[0].SendKeys("12345");
            inputs1.FindElements(By.CssSelector("input"))[1].SendKeys("1234");
            inputs1.FindElements(By.CssSelector("input"))[2].SendKeys("1234");
            chrome.FindElement(By.CssSelector("#ConfirmarCambioContraUsuarioCliente")).Click();
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAVerMiCuentaUsuarioAdmin()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuentaAdmin";

            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnVerMiCuentaUsuarioAdmin"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAActualizarContraseñaUsuarioAdmin()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuentaAdmin";
            chrome.FindElement(By.CssSelector("#LinkCambiarContraseñaUsuarioAdmin")).Click();
            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnCambiarContraUsuarioAdmin"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void CambiarContraseñaUsuarioAdminConDatosInvalidosAdmin()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuentaAdmin";

            chrome.FindElement(By.CssSelector("#LinkCambiarContraseñaUsuarioAdmin")).Click();
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("admin");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("administrador");
            inputs.FindElements(By.CssSelector("input"))[2].SendKeys("123456");

            chrome.FindElement(By.CssSelector("#ConfirmarCambioContraUsuarioAdmin")).Click();
            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnCambiarContraUsuarioAdmin"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void CambiarContraseñaUsuarioAdminConDatosValidos()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuentaAdmin";

            chrome.FindElement(By.CssSelector("#LinkCambiarContraseñaUsuarioAdmin")).Click();
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[0].SendKeys("admin");
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("administrador");
            inputs.FindElements(By.CssSelector("input"))[2].SendKeys("administrador");

            chrome.FindElement(By.CssSelector("#ConfirmarCambioContraUsuarioAdmin")).Click();
            var BuscarId = chrome.FindElement(By.CssSelector("#estoyenlogin"));

            Assert.IsNotNull(BuscarId);

            //REESTABLECER DATOS
            chrome.FindElement(By.CssSelector("#correo")).Clear(); chrome.FindElement(By.CssSelector("#correo")).SendKeys("admin@gmail.com");
            chrome.FindElement(By.CssSelector("#contraseña")).Clear(); chrome.FindElement(By.CssSelector("#contraseña")).SendKeys("administrador");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/CambiarContraUsuarioAdmin";
            var inputs1 = chrome.FindElement(By.CssSelector("#inputs"));
            inputs1.FindElements(By.CssSelector("input"))[0].SendKeys("administrador");
            inputs1.FindElements(By.CssSelector("input"))[1].SendKeys("admin");
            inputs1.FindElements(By.CssSelector("input"))[2].SendKeys("admin");
            chrome.FindElement(By.CssSelector("#ConfirmarCambioContraUsuarioAdmin")).Click();
            chrome.Close();
        }
        [Test]
        public void IngresoCorrectoAActualizarDatosUsuario()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuenta";
            chrome.FindElement(By.CssSelector("#LinkActualizarDatosUsuarioCliente")).Click();
            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnActualizarDatosUsuarioCliente"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void ActualizarDatosUsuarioClienteConDatosInvalidos()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuenta";

            chrome.FindElement(By.CssSelector("#LinkActualizarDatosUsuarioCliente")).Click();
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("1111");
            chrome.FindElement(By.CssSelector("#ConfirmarCambios")).Click();

            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnActualizarDatosUsuarioCliente"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void ActualizarDatosUsuarioClienteConDatosValidos()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuenta";

            chrome.FindElement(By.CssSelector("#LinkActualizarDatosUsuarioCliente")).Click();
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("Jose Luis");
            chrome.FindElement(By.CssSelector("#ConfirmarCambios")).Click();

            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnVerMiCuentaUsuarioCliente"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void ActualizarDatosUsuarioAdminConDatosInvalidos()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuentaAdmin";

            chrome.FindElement(By.CssSelector("#LinkActualizarDatosUsuarioAdmin")).Click();
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("1111");
            chrome.FindElement(By.CssSelector("#ConfirmarCambios")).Click();

            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnActualizarDatosUsuarioAdmin"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void ActualizarDatosUsuarioAdminConDatosValidos()
        {
            IWebDriver chrome = new FirefoxDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.Url = "http://localhost:56854/Usuario/VerMiCuentaAdmin";

            chrome.FindElement(By.CssSelector("#LinkActualizarDatosUsuarioAdmin")).Click();
            var inputs = chrome.FindElement(By.CssSelector("#inputs"));
            inputs.FindElements(By.CssSelector("input"))[1].SendKeys("Jose Luis");
            chrome.FindElement(By.CssSelector("#ConfirmarCambios")).Click();

            var BuscarId = chrome.FindElement(By.CssSelector("#EstoyEnVerMiCuentaUsuarioAdmin"));

            Assert.IsNotNull(BuscarId);
            chrome.Close();
        }
        [Test]
        public void BuscarTurnosEnReservaSinSeleccionarFecha_o_Servicio_Test()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Admin/Servicio";
            navegador.FindElement(By.CssSelector("#BuscarTurnos")).Click();

            var buscarClase=navegador.FindElements(By.CssSelector("div .swal2-container div .swal2-header div .swall2-error"));

            Assert.IsNotNull(buscarClase);
            navegador.Close();
        }
        [Test]
        public void BuscarTurnosEnReservaIngresandoDatosCorrectos_Test()
        {
            IWebDriver navegador = new FirefoxDriver();
           
            navegador.Url = "http://localhost:56854/Admin/Servicio";
            navegador.FindElement(By.CssSelector("#BuscarFecha")).SendKeys("2021-01-01");

            IWebElement element = navegador.FindElement(By.Id("BuscarServicio")); 
             SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByValue("7");

            navegador.FindElement(By.CssSelector("#BuscarServicio")).Click();
            navegador.FindElement(By.CssSelector("#BuscarTurnos")).Click();

            var buscarId = navegador.FindElement(By.CssSelector("#NombreServ"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        //POR HACER
        [Test]
        public void RealizarUnaReserva_SinHaberseLogueado_Test()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Admin/Servicio";
            navegador.FindElement(By.CssSelector("#BuscarFecha")).SendKeys("2021-01-01");

            IWebElement element = navegador.FindElement(By.Id("BuscarServicio"));
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByValue("7");

            navegador.FindElement(By.CssSelector("#BuscarTurnos")).Click();
            var Etiqueta = navegador.FindElements(By.CssSelector("#turnos Table tbody tr"))[0];
            Etiqueta.FindElement(By.CssSelector("a")).Click();

            
            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();

            var buscarId = navegador.FindElement(By.CssSelector("#estoyenlogin"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void RealizarUnaReserva_UsuarioLogueado_Test()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Admin/Servicio";
            navegador.FindElement(By.CssSelector("#BuscarFecha")).SendKeys("2021-01-01");

            IWebElement element = navegador.FindElement(By.Id("BuscarServicio"));
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByValue("7");

            navegador.FindElement(By.CssSelector("#BuscarTurnos")).Click();
            var Etiqueta = navegador.FindElements(By.CssSelector("#turnos Table tbody tr"))[0];
            Etiqueta.FindElement(By.CssSelector("a")).Click();

            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();

            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();

            navegador.FindElement(By.CssSelector("#BuscarFecha")).SendKeys("2021-01-01");

            IWebElement element1 = navegador.FindElement(By.Id("BuscarServicio"));
            SelectElement selectElement1 = new SelectElement(element1);
            selectElement1.SelectByValue("7");

            navegador.FindElement(By.CssSelector("#BuscarTurnos")).Click();
            var Etiqueta1 = navegador.FindElements(By.CssSelector("#turnos Table tbody tr"))[0];
            Etiqueta1.FindElement(By.CssSelector("a")).Click();

            navegador.FindElement(By.CssSelector(".swal2-input")).SendKeys("1");
            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();
            Thread.Sleep(2000);
            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();
            Thread.Sleep(2000);
            navegador.FindElement(By.CssSelector("#btnReservar")).Click();

            var buscarId = navegador.FindElement(By.CssSelector("#EstoyEnMisReservas"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void AñadirTurnosAMiReserva_Test()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();

            navegador.FindElement(By.CssSelector("#BuscarFecha")).SendKeys("2021-01-01");

            IWebElement element1 = navegador.FindElement(By.Id("BuscarServicio"));
            SelectElement selectElement1 = new SelectElement(element1);
            selectElement1.SelectByValue("7");

            navegador.FindElement(By.CssSelector("#BuscarTurnos")).Click();
            var Etiqueta1 = navegador.FindElements(By.CssSelector("#turnos Table tbody tr"))[0];
            Etiqueta1.FindElement(By.CssSelector("a")).Click();

            navegador.FindElement(By.CssSelector(".swal2-input")).SendKeys("1");
            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();
            Thread.Sleep(2000);
            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();
            Thread.Sleep(1000);

            var buscarId = navegador.FindElement(By.CssSelector("#btnReservar"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void AñadirElMismoTurno2Veces_Test()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();

            navegador.FindElement(By.CssSelector("#BuscarFecha")).SendKeys("2021-01-01");

            IWebElement element1 = navegador.FindElement(By.Id("BuscarServicio"));
            SelectElement selectElement1 = new SelectElement(element1);
            selectElement1.SelectByValue("7");

            navegador.FindElement(By.CssSelector("#BuscarTurnos")).Click();
            var Etiqueta1 = navegador.FindElements(By.CssSelector("#turnos Table tbody tr"))[0];
            Etiqueta1.FindElement(By.CssSelector("a")).Click();

            navegador.FindElement(By.CssSelector(".swal2-input")).SendKeys("1");
            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();
            Thread.Sleep(2000);
            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();
            Thread.Sleep(1000);

            Etiqueta1.FindElement(By.CssSelector("a")).Click();
            navegador.FindElement(By.CssSelector(".swal2-input")).SendKeys("1");
            navegador.FindElement(By.CssSelector(".swal2-confirm")).Click();
            Thread.Sleep(1000);
            var buscarClase=navegador.FindElements(By.CssSelector(".swall2-error"));

            Assert.IsNotNull(buscarClase);
            navegador.Close();
        }
        [Test]
        public void BuscarGananciasIngresandoFechasVacias_AdminIndexTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();

            navegador.FindElement(By.CssSelector("#btnBuscar")).Click();

            var buscarId = navegador.FindElement(By.CssSelector("#swal2-content"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void BuscarGananciasIngresandoFechaInicioMayorALaFinal_AdminIndexTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();
            navegador.FindElement(By.CssSelector("#desde")).SendKeys("2020-03-01");
            navegador.FindElement(By.CssSelector("#hasta")).SendKeys("2020-01-01");

            navegador.FindElement(By.CssSelector("#btnBuscar")).Click();

            var buscarId = navegador.FindElement(By.CssSelector("#swal2-content"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void BuscarGananciasIngresandoFechasCorrectas_AdminIndexTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();
            navegador.FindElement(By.CssSelector("#desde")).SendKeys("2020-01-01");
            navegador.FindElement(By.CssSelector("#hasta")).SendKeys("2020-03-01");

            navegador.FindElement(By.CssSelector("#btnBuscar")).Click();

            var buscarId = navegador.FindElement(By.CssSelector("#GananciaCalculada"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void BuscarServicioPorNombre_AdminServicioIndexTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();
            navegador.Url = "http://localhost:56854/Servicio/Index";
            navegador.FindElement(By.CssSelector("#query")).SendKeys("Piscina");
            navegador.FindElement(By.CssSelector("#btnBuscar")).Click();
            Thread.Sleep(1000);

            var buscarId = navegador.FindElement(By.CssSelector("#ServicioBuscado"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void BuscarTurnoPorFecha_AdminTurnoIndexTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();
            navegador.Url = "http://localhost:56854/Turno/Index?id=3";
            navegador.FindElement(By.CssSelector("#query")).SendKeys("2020-07-07");
            navegador.FindElement(By.CssSelector("#btnBuscar")).Click();
            Thread.Sleep(1000);

            var buscarId = navegador.FindElement(By.CssSelector("#TurnoBuscado"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void BuscarUsuarioPorDNI_AdminUsuarioIndexTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();
            navegador.Url = "http://localhost:56854/Usuario/Index";
            navegador.FindElement(By.CssSelector("#query")).SendKeys("11111112");
            navegador.FindElement(By.CssSelector("#btnBuscar")).Click();
            Thread.Sleep(1000);

            var buscarId = navegador.FindElement(By.CssSelector("#UsuarioBuscado"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void BuscarReservaPorDNIUsuario_AdminReservaIndexTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();
            navegador.Url = "http://localhost:56854/Reserva/Index";
            navegador.FindElement(By.CssSelector("#query")).SendKeys("11111112");
            navegador.FindElement(By.CssSelector("#btnBuscar")).Click();
            Thread.Sleep(1000);

            var buscarId = navegador.FindElement(By.CssSelector("#ReservaBuscada"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void BuscarMisReservasUsuario_UsuarioMisReservasTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();
            navegador.Url = "http://localhost:56854/Reserva/MisReservas";
            navegador.FindElement(By.CssSelector("#query")).SendKeys("2020-05-03");
            navegador.FindElement(By.CssSelector("#btnBuscar")).Click();
            Thread.Sleep(1000);

            var buscarId = navegador.FindElement(By.CssSelector("#MisReservasBuscadas"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }
        [Test]
        public void VerMiDetalleReserva_UsuarioMisReservasTest()
        {
            IWebDriver navegador = new FirefoxDriver();

            navegador.Url = "http://localhost:56854/Auth/Login";
            var input1Busqueda = navegador.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = navegador.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");
            navegador.FindElement(By.CssSelector("#ingresar")).Click();
            navegador.Url = "http://localhost:56854/Reserva/MisReservas";

            var buscartr=navegador.FindElements(By.CssSelector("#tbody tr"))[0];
            buscartr.FindElement(By.CssSelector("a")).Click();
            Thread.Sleep(1000);

            var buscarId = navegador.FindElement(By.CssSelector("#MiDetalleReservaBuscada"));

            Assert.IsNotNull(buscarId);
            navegador.Close();
        }

    }
}
