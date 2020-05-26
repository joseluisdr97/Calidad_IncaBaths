using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReservaService service;
        private readonly IServiceSession sessionService;
        public ReservaController(IReservaService service, IServiceSession sessionService)
        {
            this.service = service;
            this.sessionService = sessionService;
        }
        // GET: Reserva
        public ActionResult Index()
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            var datos = service.ObtenerListaReservas().Where(a=>a.Activo_Inactivo == true).ToList();
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear()
        {
            ViewBag.Servicios = service.ObtenerListaServicios().ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Reserva reserva)
        {
            int valor = 0;
            if (reserva != null && reserva.DetalleReservas != null && reserva.DetalleReservas.Count > 0)
            {
                int idUsuario = sessionService.BuscarIdUsuarioSession();
                service.CrearReserva(idUsuario, reserva);
                valor = 1;
            }
            return RedirectToAction("MisReservas", "Reserva", new { msg = valor });
        }
        [Authorize]
        public ActionResult MisReservas()
        {
            if (sessionService.EstaLogueadoComoCliente() == false) { return RedirectToAction("Index", "Error"); }
            if (sessionService.BuscarNombreUsuarioSession() == null || sessionService.BuscarNombreUsuarioSession() == "") { return RedirectToAction("Index", "Error"); }
            //var fecha = DateTime.Now.Date;
            var usuarioIdDB = sessionService.BuscarIdUsuarioSession();
            var misReservas = service.ObtenerListaReservas().Where(a => a.IdUsuario == usuarioIdDB && a.Activo_Inactivo==true/*&& a.Fecha==fecha*/).ToList();

            return View(misReservas);
        }
        [Authorize]
        public ActionResult BuscarMisReservas(DateTime fecha)
        {
            if (sessionService.BuscarNombreUsuarioSession() == null || sessionService.BuscarNombreUsuarioSession() == "") { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = sessionService.BuscarIdUsuarioSession();
            var fechas = fecha.Date;
            var misReservas = new List<Reserva>();

            if (fecha != null && fecha.ToString() != "01/01/0001 12:00:00 a.m.")
            {
                misReservas = service.ObtenerListaReservas().Where(a => a.IdUsuario == usuarioIdDB && a.Fecha == fechas).ToList();
            }
            else
            {
                misReservas = service.ObtenerListaReservas().Where(a => a.IdUsuario == usuarioIdDB).ToList();
            }

            return View(misReservas);
        }
        [Authorize]
        public ActionResult BuscarMisReservasAdmin(string dni)
        {
            if (sessionService.BuscarNombreUsuarioSession() == null || sessionService.BuscarNombreUsuarioSession() == "") return RedirectToAction("Index", "Error");
                if (dni!=""&& dni != null)
            {
                var contUDB = service.ObtenerListaUsuarios().Count(u => u.DNI == dni && u.Activo_Inactivo == true);
                if (contUDB > 0)
                {
                    var UsuDB = service.ObtenerListaUsuarios().Where(u => u.DNI == dni).First();
                    var reservas = service.ObtenerListaReservas().Where(r => r.IdUsuario == UsuDB.IdUsuario && r.Activo_Inactivo == true).ToList();
                    return View(reservas);

                }
                else
                {
                    var rese = service.ObtenerListaReservas().Where(r => r.IdUsuario == -1 && r.Activo_Inactivo == true).ToList();
                    return View(rese);
                }
            }else
            {
                return View(service.ObtenerListaReservas().ToList());
            }
           
           
        }
        [Authorize]
        public ActionResult MiDetalleReserva(int id)
        {
            var misReservas = service.ObtenerDetalleReserva().Where(a => a.IdReserva == id && a.Activo_Inactivo == true).ToList();

            return View(misReservas);
        }
        [Authorize]
        public ActionResult Eliminar(int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            var DbReserva = service.ObtenerListaReservas().Where(o => o.IdReserva == id && o.Activo_Inactivo==true).First();
            service.EliminarReserva(DbReserva);

            var CountReservaDb = service.ObtenerDetalleReserva().Count(o => o.IdReserva == id);
            if (CountReservaDb != 0)
            {
                var CountReservaDbLista = service.ObtenerDetalleReserva().Where(o => o.IdReserva == id && o.Activo_Inactivo==true).ToList();
                for (int i = 0; i < CountReservaDb; i++)
                {
                    var idDetalleReserva = CountReservaDbLista[i].IdDetalleReserva;
                    var ReservaDb = service.ObtenerDetalleReserva().Where(o => o.IdDetalleReserva == idDetalleReserva).First();
                    service.EliminarDetalleReserva(ReservaDb);
                }
            }
            return View("Index");
        }

        public ActionResult ObtenerTurnos(int id)
        {
            var fecha = DateTime.Now.Date;
            var fechafinal = DateTime.Now.Date.AddDays(1);
            var turnos = service.ObtenerListaTurnos().Where(a => a.IdServicio == id && a.Fecha>=fecha && a.Fecha<fechafinal).ToList();
            return View(turnos);
        }
        public string ObtenerHoraInicioTurno(int id)
        {
            var TurnoDb = service.ObtenerListaTurnos().Where(a => a.IdTurno == id).First();
            return TurnoDb.HoraInicio.ToString();
        }
        public string ObtenerHoraFinTurno(int id)
        {
            var TurnoDb = service.ObtenerListaTurnos().Where(a => a.IdTurno == id).First();
            return TurnoDb.HoraFin.ToString();
        }
        public string BuscarUsuario(string dni)
        {
            var ExisteU = service.ObtenerListaUsuarios().Count(a => a.DNI == dni);

            if (ExisteU > 0)
            {
                var usuarioDB = service.ObtenerListaUsuarios().Where(u => u.DNI == dni).First();
                
                return usuarioDB.Nombre +" "+ usuarioDB.Apellido;
            }
            else
            {
                return "noexiste";
            }
             
        }
    }
}