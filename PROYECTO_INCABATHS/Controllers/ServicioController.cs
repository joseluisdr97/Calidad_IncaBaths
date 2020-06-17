using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using PROYECTO_INCABATHS.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS.Controllers
{
    public class ServicioController : Controller
    {
        private readonly IServiceSession sessionService;
        private readonly IServicioService service;
        public ServicioController(IServicioService service, IServiceSession sessionService)
        {
            this.sessionService = sessionService;
            this.service = service;
        }

        //private AppConexionDB conexion = new AppConexionDB();

        // GET: Servicio
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            var model = service.ObtenerListaServicios().Where(a => a.Activo_Inactivo == true).ToList();

            return View(model);
        }
        [Authorize]
        public ActionResult BuscarServicio(string query)//PETICION AJAX
        {
            var model = new List<Servicio>();
            if (query == null || query == "")
            {
                model = service.ObtenerListaServicios().Where(a => a.Activo_Inactivo == true).ToList();
            }
            else
            {
                model = service.ObtenerListaServicios().Where(o => o.Nombre.Contains(query) && o.Activo_Inactivo == true).ToList();
            }
            ViewBag.datos = query;
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear()
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            return View(new Servicio());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Servicio servicio)
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            int id = 0;
            validar(servicio, id);
            if (ModelState.IsValid == true)
            {
                service.GuardarServicio(servicio);
                return RedirectToAction("Index");
            }
            return View(servicio);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            var model = service.ObtenerServicioPorId(id);//PRUEBA
            ViewBag.IdServicio = id;
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Servicio servicio, int id)
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            validar(servicio, id);
            if (ModelState.IsValid == true)
            {
                service.EditarServicio(id,servicio);
                return RedirectToAction("Index");
            }
            ViewBag.IdServicio = id;
            return View(servicio);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            var CountTurnoDb = service.ListaDeTurnos().Where(o => o.IdServicio == id && o.Activo_Inactivo == true).ToList();
            ViewBag.MisTurnos = CountTurnoDb;
            if (CountTurnoDb.Count != 0)
            {
                service.EliminarTurnosDelServicio(id);
            }
            service.EliminarServicio(id);
            return RedirectToAction("Index");
        }
        public void validar(Servicio servicio, int id)
        {

            //var conexion = new AppConexionDB();
            //var existe = conexion.Servicios.Any(a => a.Nombre == servicio.Nombre);
            //if (existe)
            //{
            //    ModelState.AddModelError("Nombre", "Ya tiene un servicio con el mismo nombre");
            //}

            if (servicio.Nombre == null || servicio.Nombre == "")
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");

            if (servicio.Nombre != null)
            {
                if (!Regex.IsMatch(servicio.Nombre, @"^[a-zA-Z ]*$"))
                    ModelState.AddModelError("Nombre", "El campo nombre solo acepta letras");
            }

            if (servicio.Precio == 0 || Convert.ToString(servicio.Precio) == "")
                ModelState.AddModelError("Precio", "El campo precio es obligatorio");

            if (servicio.Precio != 0 && Convert.ToString(servicio.Precio) == null)
            {
                if (!Regex.IsMatch(Convert.ToString(servicio.Precio), @"^\d{1,2}([.][0-9]{1,2})?$"))
                    ModelState.AddModelError("Precio", "El campo precio solo acepta decimales [.]");
            }
            if (Regex.IsMatch(Convert.ToString(servicio.Precio), @"^[a-zA-Z ]*$"))
                ModelState.AddModelError("Precio", "El campo precio no acepta letras");


            if (servicio.Aforo == 0 || Convert.ToString(servicio.Aforo) == "")
                ModelState.AddModelError("Aforo", "El campo aforo es obligatorio");

        }
        public ViewResult EliminarServcicioP(int? id)
        {
            var model = service.ListaDeTurnos().Where(o => o.IdServicio == id && o.Activo_Inactivo == true).ToList();
            return View(model);
        }
    }
}