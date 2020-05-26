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
    public class TurnoController : Controller
    {
        private readonly IServiceSession sessionService;
        private readonly ITurnoService service;
        public TurnoController(ITurnoService service, IServiceSession sessionService)
        {
            this.sessionService = sessionService;
            this.service = service;
        }
        // GET: Turno
        [Authorize]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };

                var datos = service.ObtenerTurnos().Where(t => t.IdServicio == id && t.Activo_Inactivo==true).ToList();
                ViewBag.Servicio = service.ObtenerServicioPorId(id);
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult BuscarTurno(DateTime? fecha, int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            var datos = new List<Turno>();
            if (fecha == null || fecha.ToString() == "01/01/0001 12:00:00 a.m.")
            {
                datos = service.ObtenerTurnos().Where(t => t.IdServicio == id && t.Activo_Inactivo == true).ToList();
            }
            else
            {
                datos = service.ObtenerTurnos().Where(t => t.IdServicio == id && t.Fecha == fecha && t.Activo_Inactivo == true).ToList();
            }
            ViewBag.Servicio = service.ObtenerServicioPorId(id);
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear(int? id)
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            ViewBag.Servicio = service.ObtenerServicioPorId(id);
            return View(new Turno());
        }
        [HttpPost]
        [Authorize]
        public ActionResult Crear(Turno turno, int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            validar(turno, id);
            if (ModelState.IsValid == true)
            {
                service.GuardarTurno(id,turno);
                return RedirectToAction("Index", new { id = turno.IdServicio });
            }
            ViewBag.Servicio = service.ObtenerServicioPorId(id);
            return View(turno);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            var DbTurno = service.ObtenerTurnoPorId(id);
            ViewBag.Servicio = service.ObtenerServicioPorId(DbTurno.IdServicio);
            return View(DbTurno);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Turno turno, int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            var DbTurno = service.ObtenerTurnoPorId(id);
            validarEditar(turno, id);
            if (ModelState.IsValid == true)
            {
                service.EditarTurno(turno, DbTurno);
                return RedirectToAction("Index", new { id = DbTurno.IdServicio });
            }
            ViewBag.Servicio = service.ObtenerServicioPorId(DbTurno.IdServicio);
            return View(DbTurno);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); };
            var DbTurno = service.ObtenerTurnoPorId(id);
            if (DbTurno != null)
            {
                service.EliminarTurno(DbTurno);
                ViewBag.Turno = DbTurno;
                return RedirectToAction("Index", new { id = service.ObtenerTurnoPorId(id).IdServicio });
                
            }
            return View();
        }




        private AppConexionDB conexion = new AppConexionDB();
        public void validar(Turno turno, int? id)
        {
            string fechaP = Convert.ToString(turno.Fecha);
            if(turno.Fecha==null || fechaP=="01/01/0001 12:00:00 a.m.")
                ModelState.AddModelError("Fecha", "El campo fecha no debe de ser nulo");
            if (turno.Fecha != null && fechaP != "01/01/0001 12:00:00 a.m.")
            {
                if (turno.Fecha < DateTime.Now.Date)
                    ModelState.AddModelError("Fecha", "La fecha debe ser mayor a la actual");
            }

            if (Convert.ToString(turno.HoraInicio) == "00:00:00")
                ModelState.AddModelError("HoraInicio", "El campo hora de ingreso no debe de ser nulo");
            if (Convert.ToString(turno.HoraFin) == "00:00:00")
                ModelState.AddModelError("HoraFin", "El campo hora de salida no debe de ser nulo");



            if (Convert.ToString(turno.HoraInicio) != "00:00:00" && Convert.ToString(turno.HoraInicio) != "00:00:00")
            {
                if (turno.HoraInicio == turno.HoraFin)
                {
                    ModelState.AddModelError("HoraFin", "La hora de salida no debe de ser igual a la hora de inicio");
                }
            }

            if (Convert.ToString(turno.HoraInicio) != "00:00:00" && Convert.ToString(turno.HoraInicio) != "00:00:00")
            {
                if (turno.HoraInicio != turno.HoraFin)
                {
                    if (turno.HoraInicio > turno.HoraFin)
                    {
                        ModelState.AddModelError("HoraInicio", "La hora de ingreso no debe de ser mayor a la hora de salida");
                    }
                }
            }
            if (Convert.ToString(turno.HoraInicio) != "00:00:00" && Convert.ToString(turno.HoraInicio) != "00:00:00")
            {
                if (turno.HoraInicio != turno.HoraFin)
                {
                    if (turno.HoraInicio < turno.HoraFin)
                    {
                        var Existe = conexion.Turnos.Any(a => a.IdServicio == id && a.HoraInicio == turno.HoraInicio && a.HoraFin == turno.HoraFin && a.Fecha == turno.Fecha);
                        if (Existe)
                            ModelState.AddModelError("HoraFin", "Este turno ya existe");
                    }
                }
            }
            //VALIDACION DE CRUCE DE HORARIOS

            if (Convert.ToString(turno.HoraInicio) != "00:00:00" && Convert.ToString(turno.HoraInicio) != "00:00:00")
            {
                if (turno.HoraInicio != turno.HoraFin)
                {
                    if (turno.HoraInicio < turno.HoraFin)
                    {
                        var Existe = conexion.Turnos.Any(a => a.IdServicio == id && a.HoraInicio == turno.HoraInicio && a.HoraFin == turno.HoraFin && a.Fecha == turno.Fecha);
                        if (!Existe)
                        {
                            var ListaTurnos = conexion.Turnos.Where(a => a.IdServicio == id).ToList();
                            for (int i = 0; i < ListaTurnos.Count; i++)
                            {

                                if (turno.HoraInicio >= ListaTurnos[i].HoraInicio && turno.HoraInicio <= ListaTurnos[i].HoraFin && ListaTurnos[i].Fecha == turno.Fecha)
                                {
                                    ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                                }
                                if (turno.HoraFin <= ListaTurnos[i].HoraFin && turno.HoraFin >= ListaTurnos[i].HoraInicio && ListaTurnos[i].Fecha == turno.Fecha)
                                {
                                    ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                                }
                                if (turno.HoraFin >= ListaTurnos[i].HoraFin && turno.HoraInicio <= ListaTurnos[i].HoraFin && ListaTurnos[i].Fecha == turno.Fecha)
                                {
                                    ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                                }
                                if (turno.HoraInicio <= ListaTurnos[i].HoraInicio && turno.HoraFin >= ListaTurnos[i].HoraInicio && ListaTurnos[i].Fecha == turno.Fecha)
                                {
                                    ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                                }
                            }
                        }

                    }
                }
            }
        }
        public void validarEditar(Turno turno, int? id)
        {
            if (Convert.ToString(turno.HoraInicio) == "00:00:00")
                ModelState.AddModelError("HoraInicio", "El campo hora de ingreso no debe de ser nulo");
            if (Convert.ToString(turno.HoraFin) == "00:00:00")
                ModelState.AddModelError("HoraFin", "El campo hora de salida no debe de ser nulo");



            if (Convert.ToString(turno.HoraInicio) != "00:00:00" && Convert.ToString(turno.HoraInicio) != "00:00:00")
            {
                if (turno.HoraInicio == turno.HoraFin)
                {
                    ModelState.AddModelError("HoraFin", "La hora de salida no debe de ser igual a la hora de inicio");
                }
            }

            if (Convert.ToString(turno.HoraInicio) != "00:00:00" && Convert.ToString(turno.HoraInicio) != "00:00:00")
            {
                if (turno.HoraInicio != turno.HoraFin)
                {
                    if (turno.HoraInicio > turno.HoraFin)
                    {
                        ModelState.AddModelError("HoraInicio", "La hora de ingreso no debe de ser mayor a la hora de salida");
                    }
                }
            }

            //VALIDACION DE CRUCE DE HORARIOS

            if (Convert.ToString(turno.HoraInicio) != "00:00:00" && Convert.ToString(turno.HoraInicio) != "00:00:00")
            {
                if (turno.HoraInicio != turno.HoraFin)
                {
                    if (turno.HoraInicio < turno.HoraFin)
                    {
                        var Existe = conexion.Turnos.Any(a => a.IdServicio == id && a.HoraInicio == turno.HoraInicio && a.HoraFin == turno.HoraFin && a.Fecha == turno.Fecha);
                        if (!Existe)
                        {
                            var ListaTurnos = conexion.Turnos.Where(a => a.IdServicio == id).ToList();
                            for (int i = 0; i < ListaTurnos.Count; i++)
                            {

                                if (turno.HoraInicio >= ListaTurnos[i].HoraInicio && turno.HoraInicio <= ListaTurnos[i].HoraFin && ListaTurnos[i].Fecha == turno.Fecha)
                                {
                                    ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                                }
                                if (turno.HoraFin <= ListaTurnos[i].HoraFin && turno.HoraFin >= ListaTurnos[i].HoraInicio && ListaTurnos[i].Fecha == turno.Fecha)
                                {
                                    ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                                }
                                if (turno.HoraFin >= ListaTurnos[i].HoraFin && turno.HoraInicio <= ListaTurnos[i].HoraFin && ListaTurnos[i].Fecha == turno.Fecha)
                                {
                                    ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                                }
                                if (turno.HoraInicio <= ListaTurnos[i].HoraInicio && turno.HoraFin >= ListaTurnos[i].HoraInicio && ListaTurnos[i].Fecha == turno.Fecha)
                                {
                                    ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}

