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
        [Authorize]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }

                var datos = service.ObtenerTurnos().Where(t => t.IdServicio == id && t.Activo_Inactivo).ToList();
                ViewBag.Servicio = service.ObtenerServicioPorId(id);
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult BuscarTurno(DateTime? fecha, int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }
            List<Turno> datos;
            if (fecha == null || fecha.ToString() == "01/01/0001 12:00:00 a.m.")
            {
                datos = service.ObtenerTurnos().Where(t => t.IdServicio == id && t.Activo_Inactivo).ToList();
            }
            else
            {
                datos = service.ObtenerTurnos().Where(t => t.IdServicio == id && t.Fecha == fecha && t.Activo_Inactivo).ToList();
            }
            ViewBag.Servicio = service.ObtenerServicioPorId(id);
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear(int? id)
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }
            ViewBag.Servicio = service.ObtenerServicioPorId(id);
            return View(new Turno());
        }
      
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Turno turno, int? id)
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }
            validar(turno, id);
            validar1(turno, id);
            validar2(turno, id);
            validar2_copia(turno, id);
            validar3(turno, id);
            validar3_copia(turno, id);
            if (ModelState.IsValid)
            {
                service.GuardarTurno(id, turno);
                return RedirectToAction("Index", new { id = turno.IdServicio });
            }
            ViewBag.Servicio = service.ObtenerServicioPorId(id);
            return View(turno);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }
            var DbTurno = service.ObtenerTurnoPorId(id);
            ViewBag.Servicio = service.ObtenerServicioPorId(DbTurno.IdServicio);
            return View(DbTurno);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Turno turno, int? id)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }
            var DbTurno = service.ObtenerTurnoPorId(id);
            validarEditar(turno, id);
            validarEditar1(turno, id);
            validarEditar2(turno, id);
            validarEditar1_copia(turno, id);
            validarEditar2_copia(turno, id);
            if (ModelState.IsValid)
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
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }
            var DbTurno = service.ObtenerTurnoPorId(id);
            if (DbTurno != null)
            {
                service.EliminarTurno(DbTurno);
                ViewBag.Turno = DbTurno;
                return RedirectToAction("Index", new { id = service.ObtenerTurnoPorId(id).IdServicio });
                
            }
            return View();
        }

        public void validar(Turno turno, int? id)
        {
            string fechaP = Convert.ToString(turno.Fecha);
            if(fechaP=="01/01/0001 12:00:00 a.m.")
                ModelState.AddModelError("Fecha", "El campo fecha no debe de ser nulo");
            if (turno.Fecha.ToString() != "01/01/0001 12:00:00 a.m." && turno.Fecha < DateTime.Now.Date)
            {
                    ModelState.AddModelError("Fecha", "La fecha debe ser mayor a la actual");
            }
            Turno t1 = turno,t2=turno,t3=turno,t4=turno;
            if (Convert.ToString(t1.HoraInicio) == "00:00:00")
                ModelState.AddModelError("HoraInicio", "El campo hora de ingreso no debe de ser nulo");
            if (Convert.ToString(t2.HoraFin) == "00:00:00")
                ModelState.AddModelError("HoraFin", "El campo hora de salida no debe de ser nulo");



            if (Convert.ToString(t3.HoraInicio) != "00:00:00" && turno.HoraInicio == turno.HoraFin)
            {
                    ModelState.AddModelError("HoraFin", "La hora de salida no debe de ser igual a la hora de inicio");
            }

            if (Convert.ToString(t4.HoraInicio) != "00:00:00" && turno.HoraInicio != turno.HoraFin && turno.HoraInicio > turno.HoraFin)
            {
                        ModelState.AddModelError("HoraInicio", "La hora de ingreso no debe de ser mayor a la hora de salida");
            }
            
        }
        public void validar1(Turno turno1, int? id)
        {

            if (Convert.ToString(turno1.HoraInicio) != "00:00:00" && turno1.HoraInicio != turno1.HoraFin && turno1.HoraInicio < turno1.HoraFin && service.Existe(id, turno1))
            {
                    ModelState.AddModelError("HoraFin", "Este turno ya existe");
            }

        }
        public void validar2(Turno turno2, int? id)
        {
            if (Convert.ToString(turno2.HoraInicio) != "00:00:00" && turno2.HoraInicio != turno2.HoraFin && turno2.HoraInicio < turno2.HoraFin && !service.Existe(id, turno2))
            {
                    var ListaTurnos = service.ObtenerTurnos().Where(a => a.IdServicio == id).ToList();
                    for (int i = 0; i < ListaTurnos.Count; i++)
                    {
                        if (turno2.HoraInicio >= ListaTurnos[i].HoraInicio && turno2.HoraInicio <= ListaTurnos[i].HoraFin && ListaTurnos[i].Fecha == turno2.Fecha)
                        {
                            ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                        }
                    }

            }
        }
        public void validar2_copia(Turno turno2C, int? id)
        {
            if (Convert.ToString(turno2C.HoraInicio) != "00:00:00" && turno2C.HoraInicio != turno2C.HoraFin && turno2C.HoraInicio < turno2C.HoraFin && !service.Existe(id, turno2C))
            {
                    var ListaTurnos = service.ObtenerTurnos().Where(a => a.IdServicio == id).ToList();
                    for (int i = 0; i < ListaTurnos.Count; i++)
                    {
                        if (turno2C.HoraFin <= ListaTurnos[i].HoraFin && turno2C.HoraFin >= ListaTurnos[i].HoraInicio && ListaTurnos[i].Fecha == turno2C.Fecha)
                        {
                            ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                        }
                    }
            }
        }
        public void validar3(Turno turno3, int? id)
        {
            if (Convert.ToString(turno3.HoraInicio) != "00:00:00" && turno3.HoraInicio != turno3.HoraFin && turno3.HoraInicio < turno3.HoraFin && !service.Existe(id, turno3))
            {
                    var ListaTurnos = service.ObtenerTurnos().Where(a => a.IdServicio == id).ToList();
                    for (int i = 0; i < ListaTurnos.Count; i++)
                    {
                        if (turno3.HoraFin >= ListaTurnos[i].HoraFin && turno3.HoraInicio <= ListaTurnos[i].HoraFin && ListaTurnos[i].Fecha == turno3.Fecha)
                        {
                            ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                        }
                    }
            }
        }
        public void validar3_copia(Turno turno3C, int? id)
        {
            if (Convert.ToString(turno3C.HoraInicio) != "00:00:00" && turno3C.HoraInicio != turno3C.HoraFin && turno3C.HoraInicio < turno3C.HoraFin && !service.Existe(id, turno3C))
            {
                    var ListaTurnos = service.ObtenerTurnos().Where(a => a.IdServicio == id).ToList();
                    for (int i = 0; i < ListaTurnos.Count; i++)
                    {
                        if (turno3C.HoraInicio <= ListaTurnos[i].HoraInicio && turno3C.HoraFin >= ListaTurnos[i].HoraInicio && ListaTurnos[i].Fecha == turno3C.Fecha)
                        {
                            ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                        }
                    }
            }
        }
        public void validarEditar(Turno turno, int? id)
        {
            Turno tu1 = turno, tu2 = turno, tu3 = turno, tu4 = turno;
            if (Convert.ToString(tu1.HoraInicio) == "00:00:00")
                ModelState.AddModelError("HoraInicio", "El campo hora de ingreso no debe de ser nulo");
            if (Convert.ToString(tu2.HoraFin) == "00:00:00")
                ModelState.AddModelError("HoraFin", "El campo hora de salida no debe de ser nulo");



            if (Convert.ToString(tu3.HoraInicio) != "00:00:00" && turno.HoraInicio == turno.HoraFin)
            {
                    ModelState.AddModelError("HoraFin", "La hora de salida no debe de ser igual a la hora de inicio");
            }

            if (Convert.ToString(tu4.HoraInicio) != "00:00:00" && turno.HoraInicio != turno.HoraFin && turno.HoraInicio > turno.HoraFin)
            {
                        ModelState.AddModelError("HoraInicio", "La hora de ingreso no debe de ser mayor a la hora de salida");
            }
        }
        public void validarEditar1(Turno turno1, int? id)
        {
            if (Convert.ToString(turno1.HoraInicio) != "00:00:00" && turno1.HoraInicio != turno1.HoraFin && turno1.HoraInicio < turno1.HoraFin && !service.Existe(id, turno1))
            {
                    var ListaTurnos = service.ObtenerTurnos().Where(a => a.IdServicio == id).ToList();
                    for (int i = 0; i < ListaTurnos.Count; i++)
                    {

                        if (turno1.HoraInicio >= ListaTurnos[i].HoraInicio && turno1.HoraInicio <= ListaTurnos[i].HoraFin && ListaTurnos[i].Fecha == turno1.Fecha)
                        {
                            ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                        }
                    }
            }
        }
        public void validarEditar1_copia(Turno turno1EC, int? id)
        {
            if (Convert.ToString(turno1EC.HoraInicio) != "00:00:00" && turno1EC.HoraInicio != turno1EC.HoraFin && turno1EC.HoraInicio < turno1EC.HoraFin && !service.Existe(id, turno1EC))
            {
                    var ListaTurnos = service.ObtenerTurnos().Where(a => a.IdServicio == id).ToList();
                    for (int i = 0; i < ListaTurnos.Count; i++)
                    {

                        if (turno1EC.HoraFin <= ListaTurnos[i].HoraFin && turno1EC.HoraFin >= ListaTurnos[i].HoraInicio && ListaTurnos[i].Fecha == turno1EC.Fecha)
                        {
                            ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                        }
                    }
            }
        }
        public void validarEditar2(Turno turnoE2, int? id)
        {
            if (Convert.ToString(turnoE2.HoraInicio) != "00:00:00" && turnoE2.HoraInicio != turnoE2.HoraFin && turnoE2.HoraInicio < turnoE2.HoraFin && !service.Existe(id, turnoE2))
            {
                    var ListaTurnos = service.ObtenerTurnos().Where(a => a.IdServicio == id).ToList();
                    for (int i = 0; i < ListaTurnos.Count; i++)
                    {
                        if (turnoE2.HoraFin >= ListaTurnos[i].HoraFin && turnoE2.HoraInicio <= ListaTurnos[i].HoraFin && ListaTurnos[i].Fecha == turnoE2.Fecha)
                        {
                            ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                        }
                    }
            }
        }
        public void validarEditar2_copia(Turno turnoE2C, int? id)
        {
            if (Convert.ToString(turnoE2C.HoraInicio) != "00:00:00" && turnoE2C.HoraInicio != turnoE2C.HoraFin && turnoE2C.HoraInicio < turnoE2C.HoraFin && !service.Existe(id, turnoE2C))
            {
                    var ListaTurnos = service.ObtenerTurnos().Where(a => a.IdServicio == id).ToList();
                    for (int i = 0; i < ListaTurnos.Count; i++)
                    {
                        if (turnoE2C.HoraInicio <= ListaTurnos[i].HoraInicio && turnoE2C.HoraFin >= ListaTurnos[i].HoraInicio && ListaTurnos[i].Fecha == turnoE2C.Fecha)
                        {
                            ModelState.AddModelError("HoraFin", "Este horario se esta cruzando con otro");
                        }
                    }
            }
        }
    }
}

