﻿using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
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
        private AppConexionDB conexion = new AppConexionDB();
        // GET: Turno
        [Authorize]
        [HttpGet]
        public ActionResult Index(string fechaP, int id)
        {
            var datos = new List<Turno>();
            DateTime fecha = Convert.ToDateTime(fechaP);
            if (fechaP == null || fechaP== "01/01/0001 12:00:00 a.m.")
            {
                datos = conexion.Turnos.Include(t => t.Servicio).Where(t => t.Servicio.IdServicio == id).ToList();
            }
            else
            {
                
                datos = conexion.Turnos.Include(t => t.Servicio).Where(t => t.Servicio.IdServicio == id && t.Fecha==fecha).ToList();
            }
            ViewBag.datos = fecha;
            ViewBag.NombreServicio = conexion.Servicios.Where(a => a.IdServicio == id).First();
            ViewBag.IdServicio = id;
            return View(datos);
        }
        public ActionResult BuscarTurno(DateTime fechaP, int id)
        {
            var datos = new List<Turno>();
            if (fechaP == null || Convert.ToString(fechaP) == "01/01/0001 12:00:00 a.m.")
            {
                datos = conexion.Turnos.Include(t => t.Servicio).Where(t => t.Servicio.IdServicio == id).ToList();
            }
            else
            {
                datos = conexion.Turnos.Include(t => t.Servicio).Where(t => t.Servicio.IdServicio == id && t.Fecha == fechaP).ToList();
            }
            ViewBag.datos = fechaP;
            ViewBag.IdServicio = id;
            return View(datos);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Crear(int id)
        {
            ViewBag.IdServicio = id;
            ViewBag.NombreServicio = conexion.Servicios.Where(a => a.IdServicio == id).First();
            return View(new Turno());
        }
        [HttpPost]
        [Authorize]
        public ActionResult Crear(Turno turno, int id)
        {
            validar(turno, id);
            if (ModelState.IsValid == true)
            {
                turno.IdServicio = id;
                
                conexion.Turnos.Add(turno);
                conexion.SaveChanges();

                return RedirectToAction("Index", new { id = turno.IdServicio });
            }
            ViewBag.IdServicio = id;
            ViewBag.NombreServicio = conexion.Servicios.Where(a => a.IdServicio == id).First();
            return View(turno);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var DbTurno = conexion.Turnos.Find(id);
            ViewBag.IdServicio = conexion.Servicios.Find(DbTurno.IdServicio);
            ViewBag.NombreServicio = conexion.Servicios.Where(a => a.IdServicio == id).First();
            return View(DbTurno);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Turno turno, int id)
        {
            var DbTurno = conexion.Turnos.Find(id);
            validarEditar(turno, id);
            if (ModelState.IsValid == true)
            {
                DbTurno.HoraInicio = turno.HoraInicio;
                DbTurno.HoraFin = turno.HoraFin;
                DbTurno.Fecha = turno.Fecha;
                conexion.SaveChanges();
                return RedirectToAction("Index", new { id = DbTurno.IdServicio });
            }
            ViewBag.IdServicio = conexion.Servicios.Find(DbTurno.IdServicio);
            ViewBag.NombreServicio = conexion.Servicios.Where(a => a.IdServicio == id).First();
            return View(turno);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var DbTurno = conexion.Turnos.Find(id);
            conexion.Turnos.Remove(DbTurno);
            conexion.SaveChanges();
            return RedirectToAction("Index", new { id = DbTurno.IdServicio });
        }



        public void validar(Turno turno, int id)
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
        public void validarEditar(Turno turno, int id)
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
            //if (Convert.ToString(turno.HoraInicio) != "00:00:00" && Convert.ToString(turno.HoraInicio) != "00:00:00")
            //{
            //    if (turno.HoraInicio != turno.HoraFin)
            //    {
            //        if (turno.HoraInicio < turno.HoraFin)
            //        {
            //            var Existe = conexion.Turnos.Any(a => a.IdServicio == id && a.HoraInicio == turno.HoraInicio && a.HoraFin == turno.HoraFin && a.Dia == turno.Dia);
            //            var UsuarioDBC = conexion.Usuarios.Where(a => a.IdUsuario == id).First();
            //            var UsuCorreo = conexion.Usuarios.Where(a => a.Correo == UsuarioDBC.Correo).First();
            //            if (UsuCorreo.Correo != usuario.Correo)
            //            {
            //                var usuarioDB = conexion.Usuarios.Any(t => t.Correo == usuario.Correo);
            //                if (usuarioDB)
            //                {
            //                    ModelState.AddModelError("Correo", "Este Correo ya existe");
            //                }
            //            }
            //        }
            //    }
            //}

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