using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private const string RutaImagen = "~/Perfiles";
        private const string RutaI = "/Perfiles/";
        private readonly IAdminService service;
        private readonly IServiceSession sessionService;
        public AdminController(IAdminService service, IServiceSession sessionService)
        {
            this.service = service;
            this.sessionService = sessionService;
        }
        [Authorize]
        public ActionResult Index()
        {

            if (sessionService.BuscarNombreUsuarioSession() == null || sessionService.BuscarNombreUsuarioSession() == "") { return RedirectToAction("Index", "Error"); }
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }

            int idUsuario = sessionService.BuscarIdUsuarioSession();
            var usuario = service.ObtenerListaUsuarios().First(u => u.IdUsuario == idUsuario);
            sessionService.GuardarDatosUsuarioLogueado(usuario);

            var fecha = DateTime.Now.Date;
                var beginDateTime = fecha.Date;
                var endDateTime = fecha.Date.AddDays(1);

                decimal suma = 0;

                var ContGanancias = service.ObtenerListReservas().Count(a => a.Fecha >= beginDateTime && a.Fecha < endDateTime && a.Activo_Inactivo);
                if (ContGanancias > 0)
                {
                    var Ganancias = service.ObtenerListReservas().Where(a => a.Fecha >= beginDateTime && a.Fecha < endDateTime && a.Activo_Inactivo).ToList();
                    for (int i = 0; i < ContGanancias; i++)
                    {
                        suma = suma + Ganancias[i].Total;
                    }
                    ViewBag.GanaciasDelDia = suma;
                }
                else
                {
                    ViewBag.GanaciasDelDia = 0;
                }
                return View();

        }
        [Authorize]
        public decimal CalcularGanancia(DateTime fecha1, DateTime fecha2)
        {
            var fechainicio = fecha1.Date;
            var fechafinal = fecha2.Date.AddDays(1);
            decimal suma = 0;
            var ContGanancias = service.ObtenerListReservas().Count(a => a.Fecha >= fechainicio && a.Fecha < fechafinal);
            if (ContGanancias > 0)
            {
                var Ganancias = service.ObtenerListReservas().Where(a => a.Fecha >= fechainicio && a.Fecha < fechafinal).ToList();
                for (int i = 0; i < ContGanancias; i++)
                {
                    suma = suma + Ganancias[i].Total;
                }
                ViewBag.GanaciasDeFechaAfecha = suma;
            }
            return suma;
        }
        public ActionResult Prueba()
        {
            var nombre = sessionService.BuscarNombreUsuarioSession();
            if ( nombre!= null && nombre!="" )
            {
                int idUsuario = sessionService.BuscarIdUsuarioSession();
                var usuario = service.ObtenerListaUsuarios().First(u => u.IdUsuario == idUsuario);
                sessionService.GuardarDatosUsuarioLogueado(usuario);
                return View(true);
            }
            return View(false);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            ViewBag.TipoUsuarios = service.ObtenerListaTipoUsuarios().ToList();
            return View(new Usuario());
        }
        [HttpGet]

        public ActionResult Servicio()
        {
            ViewBag.Servicios = service.ObtenerListaServicios().Where(a=>a.Activo_Inactivo).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult BuscarServicio(int? id,DateTime fecha)
        {
            if (id == null || id == 0) { return RedirectToAction("Index", "Error"); }
            var datos = service.ObtenerListaTurnos().Where(s => s.IdServicio == id && s.Fecha==fecha && s.Activo_Inactivo).ToList();

            return View(datos);
        }

        public decimal ObtenerPrecioServicio(int? id)
        {
            if (id == null || id == 0) return 0;
            var Dbprecio = service.ObtenerListaServicios().First(p => p.IdServicio == id);
            var precio = Dbprecio.Precio;
            return precio;
        }
        public string ObtenerNombreServicio(int? id)
        {
            if (id == null || id == 0) return "";
            var Dbprecio = service.ObtenerListaServicios().First(p => p.IdServicio == id);
            var nombre = Dbprecio.Nombre;
            return nombre;
        }
        public int ConsultarAforoDeTurno(DateTime Fecha,int? idServicio,TimeSpan horaInicio, TimeSpan horaFin)
        {
            if (idServicio == null || idServicio == 0) return 0;
            var DBServicio = service.ObtenerListaServicios().First(a => a.IdServicio == idServicio);
            var DbTurnosOcupados = service.ObtenerListaTurnos().Count(t => t.Fecha == Fecha && t.IdServicio == idServicio && t.HoraInicio==horaInicio && t.HoraFin == horaFin);
            var calcular = DBServicio.Aforo - DbTurnosOcupados;
            return calcular;
        }

        [HttpPost]
        public ActionResult Crear(Usuario usuario, string RepitaPassword, HttpPostedFileBase file)
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath(RutaImagen), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                usuario.Perfil = RutaI + Path.GetFileName(file.FileName);
            }
            usuario.IdTipoUsuario = 3;
            validar(usuario, RepitaPassword);
            validar1(usuario, RepitaPassword);
            validar2(usuario, RepitaPassword);
            validar3(usuario, RepitaPassword);
            if (ModelState.IsValid)
            {
                service.CrearUsuario(usuario);
                int valor = 1;
                return RedirectToAction("Login","Auth", new { msg = valor });
            }
            ViewBag.TipoUsuarios = service.ObtenerListaTipoUsuarios().ToList();
            return View(usuario);
        }

        private readonly AppConexionDB conexion = new AppConexionDB();
        public void validar(Usuario usuario, string RepitaPassword)
        {
            if (usuario.Nombre == null || usuario.Nombre == "")
                ModelState.AddModelError("Nombre", "El Nombre es obligatorio");
            if (usuario.Nombre != null && Regex.IsMatch(usuario.Nombre, @"^[a-zA-Z ]*$") && usuario.Nombre.Length <= 3)
            {
                ModelState.AddModelError("Nombre", "Debe de tener 3 letras a más");
            }

            if (usuario.Nombre != null && !Regex.IsMatch(usuario.Nombre, @"^[a-zA-Z ]*$"))
            {
                ModelState.AddModelError("Nombre", "El Nombre solo acepta letras");
            }


            if (usuario.Apellido == null || usuario.Apellido == "")
                ModelState.AddModelError("Apellido", "El Apellido es obligatorio");
            if (usuario.Apellido != null && !Regex.IsMatch(usuario.Apellido, @"^[a-zA-Z ]*$"))
            {
                ModelState.AddModelError("Apellido", "El Apellido solo acepta letras");
            }
            if (usuario.Apellido != null && Regex.IsMatch(usuario.Apellido, @"^[a-zA-Z ]*$") && usuario.Apellido.Length <= 3)
            {
                ModelState.AddModelError("Apellido", "Debe de tener 3 letras a más");
            }


        }
        public void validar1(Usuario usuario, string RepitaPassword)
        {


            if (usuario.DNI == null || usuario.DNI == "")
                ModelState.AddModelError("DNI", "El DNI es obligatorio");

            if (usuario.DNI != null && !Regex.IsMatch(usuario.DNI, "^\\d+$"))
            {
                ModelState.AddModelError("DNI", "El Dni solo acepta numeros");
            }

            if (usuario.DNI != null && Regex.IsMatch(usuario.DNI, "^\\d+$") && usuario.DNI.Length != 8)
            {
                ModelState.AddModelError("DNI", "El Dni debe de tener 8 numeros");
            }
            if (usuario.DNI != null && Regex.IsMatch(usuario.DNI, "^\\d+$") && usuario.DNI.Length == 8)
            {
                var usuarioDB = conexion.Usuarios.Any(t => t.DNI == usuario.DNI);
                if (usuarioDB)
                {
                    ModelState.AddModelError("DNI", "Este DNI ya existe");
                }
            }

            if (usuario.Celular == null || usuario.Celular == "")
                ModelState.AddModelError("Celular", "El celular es obligatorio");

            

        }
        public void validar2(Usuario usuario, string RepitaPassword)
        {

            if (usuario.Celular != null && !Regex.IsMatch(usuario.Celular, "^\\d+$"))
            {
                ModelState.AddModelError("Celular", "El campo celular solo acepta numeros");
            }

            if (usuario.Celular != null && Regex.IsMatch(usuario.Celular, "^\\d+$") && usuario.Celular.Length != 9)
            {
                ModelState.AddModelError("Celular", "El campo celular debe de tener 9 numeros");
            }
            if (usuario.Celular != null && Regex.IsMatch(usuario.Celular, "^\\d+$") && usuario.Celular.Length == 9 && usuario.Celular.Substring(0, 1) != "9")
            {
                ModelState.AddModelError("Celular", "El campo celular debe de empezar con 9");
            }


            if (usuario.Correo == null || usuario.Correo == "")
                ModelState.AddModelError("Correo", "El correo es obligatorio");

            if (usuario.Correo != null && !Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {
                ModelState.AddModelError("Correo", "El formato debe ser de correo");
            }

            if (usuario.Correo != null && Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {
                var usuarioCorreoDB = conexion.Usuarios.Any(t => t.Correo == usuario.Correo);
                if (usuarioCorreoDB)
                {
                    ModelState.AddModelError("Correo", "Este correo ya existe");
                }
            }

        }
        public void validar3(Usuario usuario, string RepitaPassword)
        {
           
            if (usuario.Direccion == null || usuario.Direccion == "")
                ModelState.AddModelError("Direccion", "La direccion es obligatorio");
            if (usuario.Password == null || usuario.Password == "")
                ModelState.AddModelError("Password", "El pasword es obligatorio");

            if (RepitaPassword == null || RepitaPassword == "")
                ModelState.AddModelError("RepitaPassword", "Este campo es obligatorio");

            if (usuario.Password != null && RepitaPassword != null && usuario.Password != RepitaPassword)
            {
                ModelState.AddModelError("RepitaPassword", "Los passwords no coinciden");
            }
            if (usuario.Perfil == null)
            {
                ModelState.AddModelError("Perfil", "El campo perfil no pude ser vacio");
            }

        }
    }
}