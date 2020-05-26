using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS.Controllers
{
    
    public class UsuarioController : Controller
    {
        private readonly IServiceSession sessionService;
        private readonly IUsuarioService service;
        public UsuarioController(IUsuarioService service, IServiceSession sessionService)
        {
            this.service = service;
            this.sessionService = sessionService;
        }
           
        // GET: Usuario
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            var datos = service.ObtenerListaUsuarios().Where(o => o.Activo_Inactivo == true).ToList();
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult BuscarUsuario(string query)
        {
            var datos = new List<Usuario>();
            if (query == null || query == "")
            {
                datos = service.ObtenerListaUsuarios().Where(o => o.Activo_Inactivo == true).ToList();
            }
            else
            {
                datos = service.ObtenerListaUsuarios().Where(o => o.DNI.Contains(query) && o.Activo_Inactivo==true).ToList();
            }
            ViewBag.datos = query;
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear()
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            ViewBag.TipoUsuarios = service.ObtenerListaDeTipoUsuarios().ToList();
            return View(new Usuario());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Usuario usuario, string RepitaPassword, HttpPostedFileBase file)
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            //validar(usuario, RepitaPassword);
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/Perfiles"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                usuario.Perfil = "/Perfiles/" + Path.GetFileName(file.FileName);
            }
            validar(usuario,RepitaPassword);
            if (ModelState.IsValid == true)
            {
                service.CrearUsuario(usuario);
                return RedirectToAction("Index");
            }
            ViewBag.TipoUsuarios = service.ObtenerListaDeTipoUsuarios().ToList();
            return View(usuario);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            if (id == 0 || id == null) { return RedirectToAction("Index", "Error"); }

            ViewBag.TipoUsuarios = service.ObtenerListaDeTipoUsuarios().ToList();
            var UsuarioDb = service.ObtenerUsuarioPorId(id);
            ViewBag.IdUsuario = id;
            return View(UsuarioDb);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Usuario usuario, int? id, HttpPostedFileBase file)
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            if (id == 0 || id == null) { return RedirectToAction("Index", "Error"); }

            var UsuarioDb = service.ObtenerUsuarioPorId(id);
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/Perfiles"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                usuario.Perfil = "/Perfiles/" + Path.GetFileName(file.FileName);
                UsuarioDb.Perfil = usuario.Perfil;
            }
            validarEditarUsuario(usuario, id);
            if (ModelState.IsValid == true)
            {
                service.EditarUsuario(usuario, UsuarioDb);
                return RedirectToAction("Index");
            }
            ViewBag.TipoUsuarios = service.ObtenerListaDeTipoUsuarios().ToList();
            ViewBag.IdUsuario = id;
            return View(UsuarioDb);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            service.EliminarUsuario(id);
            return RedirectToAction("Index");

        }
        [Authorize]
        [HttpGet]
        public ActionResult VerMiCuenta()//REFACTORIZAR METODO ES IGUAL CON EL SIGUIENTE
        {
            if (sessionService.EstaLogueadoComoCliente() == false) { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = service.BuscarIdUsuarioSession();

                var UsuarioDb = service.ObtenerUsuarioPorId(usuarioIdDB);
                return View(UsuarioDb);
        }
        [Authorize]
        [HttpGet]
        public ActionResult ActualizarDatosUCliente()
        {
            if (sessionService.EstaLogueadoComoCliente() == false) { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            if (usuarioIdDB != 0)
            {
                var UsuarioDb = service.ObtenerListaUsuarios().Where(o => o.IdUsuario == usuarioIdDB).First();
                return View(UsuarioDb);
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ActualizarDatosUCliente(Usuario usuario, HttpPostedFileBase file)
        {
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UsuarioDb = service.ObtenerListaUsuarios().Where(o => o.IdUsuario == usuarioIdDB).First();

            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/Perfiles"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                usuario.Perfil = "/Perfiles/" + Path.GetFileName(file.FileName);
                UsuarioDb.Perfil = usuario.Perfil;
            }

            validarActualizarDatos(usuario);
            if (ModelState.IsValid)
            {
                service.ActualizarDatosUsuario(usuario, UsuarioDb);
                Session["UsuarioNombre"] = usuario.Nombre;
                Session["UsuarioDNI"] = usuario.DNI;
                return RedirectToAction("VerMiCuenta");
            }
            return View(usuario);
        }
        [Authorize]
        [HttpGet]
        public ActionResult CambiarContraUsuario()
        {
            if (sessionService.EstaLogueadoComoCliente() == false) { return RedirectToAction("Index", "Error"); }
            return View(new Usuario());
        }
        [Authorize]
        [HttpPost]
        public ActionResult CambiarContraUsuario(Usuario usuario, string NuevaPassword, string RepitaPassword)
        {
            ValidarCambiarContra(usuario, NuevaPassword, RepitaPassword);
            if (ModelState.IsValid)
            { 
                var usuarioIdDB = Convert.ToInt32(Session["UsuarioId"]);
                var UsuarioDb = service.ObtenerListaUsuarios().Where(o => o.IdUsuario == usuarioIdDB).First();
                service.CambiarContraUsuario(NuevaPassword,UsuarioDb);
                return RedirectToAction("Logout", "Auth");
            }
            return View(usuario);
        }

        [Authorize]
        [HttpGet]
        public ActionResult VerMiCuentaAdmin()
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UsuarioDb = service.ObtenerListaUsuarios().Where(o => o.IdUsuario == usuarioIdDB).First();
            return View(UsuarioDb);
        }
        [Authorize]
        [HttpGet]
        public ActionResult ActualizarDatosUAdmin()
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UsuarioDb = service.ObtenerListaUsuarios().Where(o => o.IdUsuario == usuarioIdDB).First();
            return View(UsuarioDb);
        }
        [Authorize]
        [HttpPost]
        public ActionResult ActualizarDatosUAdmin(Usuario usuario, HttpPostedFileBase file)
        {

            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UsuarioDb = service.ObtenerListaUsuarios().Where(o => o.IdUsuario == usuarioIdDB).First();

            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/Perfiles"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                usuario.Perfil = "/Perfiles/" + Path.GetFileName(file.FileName);
                UsuarioDb.Perfil = usuario.Perfil;
            }
            validarActualizarDatos(usuario);
            if (ModelState.IsValid)
            {
                service.ActualizarDatosUsuario(usuario, UsuarioDb);
                Session["UsuarioNombre"] = usuario.Nombre;
                Session["UsuarioDNI"] = usuario.DNI;
                return RedirectToAction("VerMiCuentaAdmin");
            }
            return View(usuario);
        }
        [Authorize]
        [HttpGet]
        public ActionResult CambiarContraUsuarioAdmin()
        {
            if (sessionService.EstaLogueadoComoAdministrador() == false) { return RedirectToAction("Index", "Error"); }
            return View(new Usuario());
        }
        [Authorize]
        [HttpPost]
        public ActionResult CambiarContraUsuarioAdmin(Usuario usuario, string NuevaPassword, string RepitaPassword)
        {
            ValidarCambiarContra(usuario, NuevaPassword, RepitaPassword);
            if (ModelState.IsValid)
            {
                var usuarioIdDB = service.BuscarIdUsuarioSession();
                var UsuarioDb = service.ObtenerListaUsuarios().Where(o => o.IdUsuario == usuarioIdDB).First();
                service.CambiarContraUsuario(NuevaPassword, UsuarioDb);
                return RedirectToAction("Logout", "Auth");
            }
            return View(usuario);
        }


        //VALIDACIONES
        private AppConexionDB conexion = new AppConexionDB();
        public void ValidarCambiarContra(Usuario usuario, string NuevaPassword, string RepitaPassword)
        {
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UExiste = conexion.Usuarios.Count(u => u.IdUsuario == usuarioIdDB && u.Password == usuario.Password);
            if (usuario.Password != null && usuario.Password != "")
            {
                if (UExiste == 0)
                {
                    ModelState.AddModelError("Password", "Contraseña no encontrada");
                }
            }
            if (usuario.Password == null || usuario.Password == "")
                ModelState.AddModelError("Password", "Este campo es obligatorio");
            if (usuario.Password == null || usuario.Password == "")
            {
                if (NuevaPassword == null || NuevaPassword == "")
                    ModelState.AddModelError("NuevaPassword", "Este campo es obligatorio");
                if (RepitaPassword == null || RepitaPassword == "")
                    ModelState.AddModelError("RepitaPassword", "Este campo es obligatorio");
            }
            if (usuario.Password != null && usuario.Password != "")
            {
                if (UExiste != 0)
                {
                    if (usuario.Password != NuevaPassword)
                    {
                        if (NuevaPassword == null || NuevaPassword == "")
                            ModelState.AddModelError("NuevaPassword", "Este campo es obligatorio");
                        if (RepitaPassword == null || RepitaPassword == "")
                            ModelState.AddModelError("RepitaPassword", "Este campo es obligatorio");
                        if (NuevaPassword != null && NuevaPassword != "" && RepitaPassword != null && RepitaPassword != "")
                        {
                            if (NuevaPassword != RepitaPassword)
                                ModelState.AddModelError("RepitaPassword", "Las contraseñas no coinciden");
                        }
                    }
                }
            }
            if (usuario.Password != null && usuario.Password != "")
            {
                if (UExiste != 0)
                {
                    if (usuario.Password == NuevaPassword)
                    {
                        ModelState.AddModelError("NuevaPassword", "Debe ser diferente a la actual");
                    }
                }
            }
        }
        public void validarActualizarDatos(Usuario usuario)
        {
            if (usuario.Nombre == null || usuario.Nombre == "")
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
            if (usuario.Nombre != null)
            {
                if (!Regex.IsMatch(usuario.Nombre, @"^[a-zA-Z ]*$"))
                    ModelState.AddModelError("Nombre", "El campo nombre solo acepta letras");
            }

            if (usuario.Apellido == null || usuario.Apellido == "")
                ModelState.AddModelError("Apellido", "El apellido es obligatorio");
            if (usuario.Apellido != null)
            {
                if (!Regex.IsMatch(usuario.Apellido, @"^[a-zA-Z ]*$"))
                    ModelState.AddModelError("Apellido", "El campo apellido solo acepta letras");
            }

            if (usuario.DNI == null || usuario.DNI == "")
                ModelState.AddModelError("DNI", "El DNI es obligatorio");

            if (usuario.DNI != null)
            {
                if (!Regex.IsMatch(usuario.DNI, "^\\d+$"))
                    ModelState.AddModelError("DNI", "El campo dni solo acepta numeros");
            }

            if (usuario.DNI != null)
            {
                if (Regex.IsMatch(usuario.DNI, "^\\d+$"))
                {
                    if (usuario.DNI.Length != 8)
                        ModelState.AddModelError("DNI", "El campo dni debe de tener 8 numeros");
                }
            }
            if (usuario.DNI != null && usuario.DNI != "")
            {
                if (Regex.IsMatch(usuario.DNI, "^\\d+$"))
                {
                    if (usuario.DNI.Length == 8)
                    {
                        var DNIUsuarioDB = Convert.ToString(Session["UsuarioDNI"]);
                        var UsuDNI = conexion.Usuarios.Where(a => a.DNI == DNIUsuarioDB).First();
                        if (UsuDNI.DNI != usuario.DNI)
                        {
                            var usuarioDB = conexion.Usuarios.Any(t => t.DNI == usuario.DNI);
                            if (usuarioDB)
                            {
                                ModelState.AddModelError("DNI", "Este DNI ya existe");
                            }
                        }
                    }
                }
            }


            if (usuario.Celular == null || usuario.Celular == "")
                ModelState.AddModelError("Celular", "El celular es obligatorio");

            if (usuario.Celular != null)
            {
                if (!Regex.IsMatch(usuario.Celular, "^\\d+$"))
                    ModelState.AddModelError("Celular", "El campo celular solo acepta numeros");
            }

            if (usuario.Celular != null)
            {
                if (!Regex.IsMatch(usuario.Celular, "^\\d+$"))
                {
                    if (usuario.Celular.Length != 9)
                        ModelState.AddModelError("Celular", "El campo celular debe de tener 9 numeros");
                }
            }

            if (usuario.Direccion == null || usuario.Direccion == "")
                ModelState.AddModelError("Direccion", "La direccion es obligatorio");
            //if (usuario.Direccion != null)
            //{
            //    if (Regex.IsMatch(usuario.Direccion, @"^[a-zA-Z0-9""'\s.#]*$"))
            //        ModelState.AddModelError("Direccion", "Ejemplo Jr. La paz #121");
            //}
        }
        public void validar(Usuario usuario, string RepitaPassword)
        {


            if (usuario.Nombre == null || usuario.Nombre == "")
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
            if (usuario.Nombre != null)
            {
                if (!Regex.IsMatch(usuario.Nombre, @"^[a-zA-Z ]*$"))
                    ModelState.AddModelError("Nombre", "El campo nombre solo acepta letras");
            }

            if (usuario.Apellido == null || usuario.Apellido == "")
                ModelState.AddModelError("Apellido", "El apellido es obligatorio");
            if (usuario.Apellido != null)
            {
                if (!Regex.IsMatch(usuario.Apellido, @"^[a-zA-Z ]*$"))
                    ModelState.AddModelError("Apellido", "El campo apellido solo acepta letras");
            }

            if (usuario.DNI == null || usuario.DNI == "")
                ModelState.AddModelError("DNI", "El DNI es obligatorio");

            if (usuario.DNI != null)
            {
                if (!Regex.IsMatch(usuario.DNI, "^\\d+$"))
                    ModelState.AddModelError("DNI", "El campo dni solo acepta numeros");
            }

            if (usuario.DNI != null)
            {
                if (Regex.IsMatch(usuario.DNI, "^\\d+$"))
                {
                    if (usuario.DNI.Length != 8)
                        ModelState.AddModelError("DNI", "El campo dni debe de tener 8 numeros");
                }
            }
            if (usuario.DNI != null)
            {
                if (Regex.IsMatch(usuario.DNI, "^\\d+$"))
                {
                    if (usuario.DNI.Length == 8)
                    {
                        var usuarioDB = conexion.Usuarios.Any(t => t.DNI == usuario.DNI);
                        if (usuarioDB)
                        {
                            ModelState.AddModelError("DNI", "Este DNI ya existe");
                        }
                    }
                }
            }


            if (usuario.Celular == null || usuario.Celular == "")
                ModelState.AddModelError("Celular", "El celular es obligatorio");

            if (usuario.Celular != null)
            {
                if (!Regex.IsMatch(usuario.Celular, "^\\d+$"))
                    ModelState.AddModelError("Celular", "El campo celular solo acepta numeros");
            }

            if (usuario.Celular != null)
            {
                if (!Regex.IsMatch(usuario.Celular, "^\\d+$"))
                {
                    if (usuario.Celular.Length != 9)
                        ModelState.AddModelError("Celular", "El campo celular debe de tener 9 numeros");
                }
            }


            if (usuario.Correo == null || usuario.Correo == "")
                ModelState.AddModelError("Correo", "El correo es obligatorio");

            if (usuario.Correo != null)
            {
                if (!Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                    ModelState.AddModelError("Correo", "El formato debe ser de correo");
            }

            if (usuario.Correo != null)
            {
                if (Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                {
                    var usuarioCorreoDB = conexion.Usuarios.Any(t => t.Correo == usuario.Correo);
                    if (usuarioCorreoDB)
                    {
                        ModelState.AddModelError("Correo", "Este correo ya existe");
                    }
                }
            }


            if (usuario.Direccion == null || usuario.Direccion == "")
                ModelState.AddModelError("Direccion", "La direccion es obligatorio");

            if (usuario.Password == null || usuario.Password == "")
                ModelState.AddModelError("Password", "El pasword es obligatorio");

            if (RepitaPassword == null || RepitaPassword == "")
                ModelState.AddModelError("RepitaPassword", "Este campo es obligatorio");

            if (usuario.Password != null || RepitaPassword != null)
            {
                if (usuario.Password != RepitaPassword)
                {
                    ModelState.AddModelError("RepitaPassword", "Los passwords no coinciden");
                }
            }
            if (usuario.Perfil == null)
            {
                ModelState.AddModelError("Perfil", "El campo perfil no puede ser vacio");
            }
            if (usuario.IdTipoUsuario == 0)
            {
                ModelState.AddModelError("TipoUsuario", "Seleccione un campo valido");
            }
        }
        public void validarEditarUsuario(Usuario usuario, int? id)
        {


            if (usuario.Nombre == null || usuario.Nombre == "")
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
            if (usuario.Nombre != null)
            {
                if (!Regex.IsMatch(usuario.Nombre, @"^[a-zA-Z ]*$"))
                    ModelState.AddModelError("Nombre", "El campo nombre solo acepta letras");
            }

            if (usuario.Apellido == null || usuario.Apellido == "")
                ModelState.AddModelError("Apellido", "El apellido es obligatorio");
            if (usuario.Apellido != null)
            {
                if (!Regex.IsMatch(usuario.Apellido, @"^[a-zA-Z ]*$"))
                    ModelState.AddModelError("Apellido", "El campo apellido solo acepta letras");
            }

            if (usuario.DNI == null || usuario.DNI == "")
                ModelState.AddModelError("DNI", "El DNI es obligatorio");

            if (usuario.DNI != null)
            {
                if (!Regex.IsMatch(usuario.DNI, "^\\d+$"))
                    ModelState.AddModelError("DNI", "El campo dni solo acepta numeros");
            }

            if (usuario.DNI != null)
            {
                if (Regex.IsMatch(usuario.DNI, "^\\d+$"))
                {
                    if (usuario.DNI.Length != 8)
                        ModelState.AddModelError("DNI", "El campo dni debe de tener 8 numeros");
                }
            }


            if (usuario.Celular == null || usuario.Celular == "")
                ModelState.AddModelError("Celular", "El celular es obligatorio");

            if (usuario.Celular != null)
            {
                if (!Regex.IsMatch(usuario.Celular, "^\\d+$"))
                    ModelState.AddModelError("Celular", "El campo celular solo acepta numeros");
            }

            if (usuario.Celular != null)
            {
                if (!Regex.IsMatch(usuario.Celular, "^\\d+$"))
                {
                    if (usuario.Celular.Length != 9)
                        ModelState.AddModelError("Celular", "El campo celular debe de tener 9 numeros");
                }
            }


            if (usuario.Correo == null || usuario.Correo == "")
                ModelState.AddModelError("Correo", "El correo es obligatorio");

            if (usuario.Correo != null)
            {
                if (!Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                    ModelState.AddModelError("Correo", "El formato debe ser de correo");
            }


            if (usuario.Direccion == null || usuario.Direccion == "")
                ModelState.AddModelError("Direccion", "La direccion es obligatorio");

            if (usuario.Password == null || usuario.Password == "")
                ModelState.AddModelError("Password", "El pasword es obligatorio");

            if (usuario.DNI != null && usuario.DNI != "")
            {
                if (Regex.IsMatch(usuario.DNI, "^\\d+$"))
                {
                    if (usuario.DNI.Length == 8)
                    {
                        var IdUsuarioDB = conexion.Usuarios.Where(a => a.IdUsuario == id).First();
                        var UsuDNI = conexion.Usuarios.Where(a => a.DNI == IdUsuarioDB.DNI).First();
                        if (UsuDNI.DNI != usuario.DNI)
                        {
                            var usuarioDB = conexion.Usuarios.Any(t => t.DNI == usuario.DNI);
                            if (usuarioDB)
                            {
                                ModelState.AddModelError("DNI", "Este DNI ya existe");
                            }
                        }
                    }
                }
            }
            if (usuario.Correo != null)
            {
                if (Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                {
                    var UsuarioDBC = conexion.Usuarios.Where(a => a.IdUsuario == id).First();
                    var UsuCorreo = conexion.Usuarios.Where(a => a.Correo == UsuarioDBC.Correo).First();
                    if (UsuCorreo.Correo != usuario.Correo)
                    {
                        var usuarioDB = conexion.Usuarios.Any(t => t.Correo == usuario.Correo);
                        if (usuarioDB)
                        {
                            ModelState.AddModelError("Correo", "Este Correo ya existe");
                        }
                    }
                }
            }
        }
    }
}