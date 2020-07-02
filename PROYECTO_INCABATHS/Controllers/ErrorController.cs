using PROYECTO_INCABATHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS.Controllers
{
    public class ErrorController : Controller
    {
        private readonly AppConexionDB conexion = new AppConexionDB();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VolerInicio()
        {
            var EstaLogueado = Session["UsuarioNombre"];
            if(EstaLogueado!=null && EstaLogueado.ToString() != "")
            {
                var IdUsuario = Convert.ToInt32(Session["UsuarioId"]);
                var usuario = conexion.Usuarios/*.Where()*/.First(a => a.IdUsuario == IdUsuario);
                if (usuario.IdTipoUsuario == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }else if (usuario.IdTipoUsuario == 3)
                {
                    return RedirectToAction("Prueba", "Admin");
                }
            }
            return RedirectToAction("Prueba", "Admin");
        }
    }
}