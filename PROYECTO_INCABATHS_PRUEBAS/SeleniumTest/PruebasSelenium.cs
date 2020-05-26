using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PROYECTO_INCABATHS_PRUEBAS.SeleniumTest
{
    [TestFixture]
    class PruebasSelenium
    {
        //PRUEBAS LOGIN
        [Test]
        public void VerificarQueNoIngresoLogin()
        {
            IWebDriver navegador = new ChromeDriver();

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
            IWebDriver chrome = new ChromeDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";
            Thread.Sleep(2000);

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            var boton = chrome.FindElement(By.CssSelector("#ingresar"));
            boton.Click();
            Thread.Sleep(2000);
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnIdexAdmin"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void VerificarQueIngresoLoginUsuarioCliente()
        {
            IWebDriver chrome = new ChromeDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";
                        Thread.Sleep(2000);

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("jose@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("1234");

            var boton = chrome.FindElement(By.CssSelector("#ingresar"));
            boton.Click();
            Thread.Sleep(2000);
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnServicioCliente"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }
        [Test]
        public void IngresoAPaginaLogin()
        {
            IWebDriver chrome = new ChromeDriver();
            chrome.Url = "http://localhost:56854/Auth/Login";
            Assert.AreEqual("http://localhost:56854/Auth/Login", chrome.Url);
            chrome.Close();
        }
        [Test]
        public void RedireccionarACrearCuentaUsuario()
        {
            IWebDriver chrome = new ChromeDriver();

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
            IWebDriver chrome = new ChromeDriver();

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
            IWebDriver chrome = new ChromeDriver();

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
            IWebDriver chrome = new ChromeDriver();

            chrome.Url = "http://localhost:56854/Auth/Login";

            var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
            input1Busqueda.SendKeys("admin@gmail.com");
            var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
            input2Busqueda.SendKeys("admin");

            chrome.FindElement(By.CssSelector("#ingresar")).Click();
            chrome.FindElement(By.CssSelector("#Res")).Click();
            var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnReservaIndex"));

            Assert.IsNotNull(buscarId);
            chrome.Close();
        }

        [Test]
        public void CrearServicioConDatosNullServicioCrear()
        {
            IWebDriver chrome = new ChromeDriver();

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
            IWebDriver chrome = new ChromeDriver();

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
            IWebDriver chrome = new ChromeDriver();

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
            IWebDriver chrome = new ChromeDriver();

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
        public void CrearTurnoConDatosNull_TurnoCrear()
        {
            IWebDriver chrome = new ChromeDriver();

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
        //[Test]
        //public void CrearTurno_TurnoCrear()
        //{
        //    IWebDriver chrome = new ChromeDriver();

        //    chrome.Url = "http://localhost:56854/Auth/Login";

        //    var input1Busqueda = chrome.FindElement(By.CssSelector("#correo"));
        //    input1Busqueda.SendKeys("admin@gmail.com");
        //    var input2Busqueda = chrome.FindElement(By.CssSelector("#contraseña"));
        //    input2Busqueda.SendKeys("admin");

        //    chrome.FindElement(By.CssSelector("#ingresar")).Click();
        //    chrome.FindElement(By.CssSelector("#Ser")).Click();
        //    chrome.Url = "http://localhost:56854/Turno/Index?id=7";
        //    chrome.Url = "http://localhost:56854/Turno/Crear?id=7";
        //    var inputs = chrome.FindElement(By.CssSelector("#inputs"));
        //    inputs.FindElements(By.CssSelector("input"))[0].SendKeys("11/02/2005");
        //    inputs.FindElements(By.CssSelector("input"))[1].SendKeys("09:00:00");
        //    Thread.Sleep(5000);
        //    inputs.FindElements(By.CssSelector("input"))[2].SendKeys("11:00:00");

        //    //chrome.FindElement(By.CssSelector("#btnGuardar")).Click();

        //    //var buscarId = chrome.FindElement(By.CssSelector("#EstoyEnTurnoIndex"));

        //    //Assert.IsNotNull(buscarId);
        //    chrome.Close();
        //}
        //[Test]
        //public void CrearCuentaUsuarioCliente()
        //{
        //    IWebDriver chrome = new ChromeDriver();

        //    chrome.Url = "http://localhost:56854/Admin/Crear";
        //    var inputs = chrome.FindElement(By.CssSelector("#inputs"));
        //    var input1=chrome.FindElements(By.CssSelector("input"))[1]; input1.SendKeys("Jose");

        //    Thread.Sleep(2000);

        //    //Assert.IsNotNull(buscarId);
        //    chrome.Close();
        //}
    }
}
