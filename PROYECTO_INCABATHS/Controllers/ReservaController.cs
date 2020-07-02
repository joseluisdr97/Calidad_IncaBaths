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
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            var datos = service.ObtenerListaReservas().Where(a=>a.Activo_Inactivo).ToList();
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
            if (!sessionService.EstaLogueadoComoCliente()) { return RedirectToAction("Index", "Error"); }
            int valor = 0;
            if (reserva != null && reserva.DetalleReservas != null && reserva.DetalleReservas.Count > 0)
            {
                int idUsuario = sessionService.BuscarIdUsuarioSession();
                reserva.Activo_Inactivo = true;
                service.CrearReserva(idUsuario, reserva);
                valor = 1;
                return RedirectToAction("MisReservas", "Reserva", new { msg = valor });
            }
            return View();
        }
        [Authorize]
        public ActionResult MisReservas()
        {
            if (!sessionService.EstaLogueadoComoCliente()) { return RedirectToAction("Index", "Error"); }
            if (sessionService.BuscarNombreUsuarioSession() == null || sessionService.BuscarNombreUsuarioSession() == "") { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = sessionService.BuscarIdUsuarioSession();
            var misReservas = service.ObtenerListaReservas().Where(a => a.IdUsuario == usuarioIdDB && a.Activo_Inactivo).ToList();

            return View(misReservas);
        }
        [Authorize]
        public ActionResult BuscarMisReservas(DateTime fecha)
        {
            if (sessionService.BuscarNombreUsuarioSession() == null || sessionService.BuscarNombreUsuarioSession() == "") { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = sessionService.BuscarIdUsuarioSession();
            var fechas = fecha.Date;
            var endDateTime = fechas.Date.AddDays(1);
            List<Reserva> misReservas;

            if (fecha.ToString() == "01/01/0001 12:00:00 a.m.")
            {
                misReservas = service.ObtenerListaReservas().Where(a => a.IdUsuario == usuarioIdDB).ToList();
            }
            else
            {
                misReservas = service.ObtenerListaReservas().Where(a => a.IdUsuario == usuarioIdDB && a.Fecha >= fechas && a.Fecha < endDateTime).ToList();
            }

            return View(misReservas);
        }
        [Authorize]
        public ActionResult BuscarMisReservasAdmin(string dni)
        {
            if (sessionService.BuscarNombreUsuarioSession() == null || sessionService.BuscarNombreUsuarioSession() == "") return RedirectToAction("Index", "Error");
                if (dni!=""&& dni != null)
            {
                var contUDB = service.ObtenerListaUsuarios().Count(u => u.DNI == dni && u.Activo_Inactivo);
                if (contUDB > 0)
                {
                    var UsuDB = service.ObtenerListaUsuarios().First(u => u.DNI == dni);
                    var reservas = service.ObtenerListaReservas().Where(r => r.IdUsuario == UsuDB.IdUsuario && r.Activo_Inactivo).ToList();
                    return View(reservas);

                }
                else
                {
                    var rese = service.ObtenerListaReservas().Where(r => r.IdUsuario == -1 && r.Activo_Inactivo).ToList();
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
            var misReservas = service.ObtenerDetalleReserva().Where(a => a.IdReserva == id && a.Activo_Inactivo).ToList();
            return View(misReservas);
        }
        [Authorize]
        public ActionResult Eliminar(int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }
            var DbReserva = service.ObtenerListaReservas().First(o => o.IdReserva == id && o.Activo_Inactivo);
            service.EliminarReserva(DbReserva);

            var CountReservaDb = service.ObtenerDetalleReserva().Count(o => o.IdReserva == id);
            if (CountReservaDb != 0)
            {
                var CountReservaDbLista = service.ObtenerDetalleReserva().Where(o => o.IdReserva == id && o.Activo_Inactivo).ToList();
                for (int i = 0; i < CountReservaDb; i++)
                {
                    var idDetalleReserva = CountReservaDbLista[i].IdDetalleReserva;
                    var ReservaDb = service.ObtenerDetalleReserva().First(o => o.IdDetalleReserva == idDetalleReserva);
                    service.EliminarDetalleReserva(ReservaDb);
                }
            }
            return RedirectToAction("Index");
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
            var TurnoDb = service.ObtenerListaTurnos().First(a => a.IdTurno == id);
            return TurnoDb.HoraInicio.ToString();
        }
        public string ObtenerHoraFinTurno(int id)
        {
            var TurnoDb = service.ObtenerListaTurnos().First(a => a.IdTurno == id);
            return TurnoDb.HoraFin.ToString();
        }
        public string BuscarUsuario(string dni)
        {
            var ExisteU = service.ObtenerListaUsuarios().Count(a => a.DNI == dni);

            if (ExisteU > 0)
            {
                var usuarioDB = service.ObtenerListaUsuarios().First(u => u.DNI == dni);
                
                return usuarioDB.Nombre +" "+ usuarioDB.Apellido;
            }
            else
            {
                return "noexiste";
            }
        }
    }
}