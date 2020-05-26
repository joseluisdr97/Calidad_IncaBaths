using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PROYECTO_INCABATHS.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService service;
        private readonly IServiceSession sessionService;
        public AuthController(IAuthService service, IServiceSession sessionService)
        {
            this.service = service;
            this.sessionService = sessionService;
        }
        // GET: Auth
        private AppConexionDB conexion = new AppConexionDB();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuario usuario, string RepitaPassword)//*string Correo, string Password*/)
        {

            var UExiste = service.ObtenerListaUsuarios().Count(u => u.Correo == usuario.Correo && u.Password == usuario.Password && u.Activo_Inactivo==true);

            if (UExiste != 0)
            {
                var UsuarioDB =service.ObtenerListaUsuarios().Where(u => u.Correo == usuario.Correo && u.Password == usuario.Password && u.Activo_Inactivo == true).First();
                FormsAuthentication.SetAuthCookie(UsuarioDB.Correo, false);

                if (UsuarioDB.IdTipoUsuario == 1)
                {
                    sessionService.GuardarDatosUsuarioLogueado(UsuarioDB);
                    return RedirectToAction("Index", "Admin");
                }
                else if (UsuarioDB.IdTipoUsuario == 3)
                {
                    sessionService.GuardarDatosUsuarioLogueado(UsuarioDB);
                    return RedirectToAction("Servicio", "Admin");
                }
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Validation = "Usuario y/o contraseña incorrecta";
            return View();
        }
        public ActionResult Logout()
        {
            //Cuando cerramos sesion lo eliminamos la cookie
            FormsAuthentication.SignOut();

            Session.Clear();
            return RedirectToAction("login");
        }
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        public string ObtenerUsuario()
        {
            var usuario = sessionService.BuscarNombreUsuarioSession();
            string nombre;
            if (usuario == null || usuario=="")
            {
                nombre = "null";
                return nombre;
            }
            else
            {
                nombre = "1";
                return nombre;
            }
        }

    }
}