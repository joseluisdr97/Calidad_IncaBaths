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
        private const string RutaImagen = "~/Perfiles";
        private const string RutaI = "/Perfiles/";
        private readonly IServiceSession sessionService;
        private readonly IUsuarioService service;
        public UsuarioController(IUsuarioService service, IServiceSession sessionService)
        {
            this.service = service;
            this.sessionService = sessionService;
        }
           
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            var datos = service.ObtenerListaUsuarios().Where(o => o.Activo_Inactivo).ToList();
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult BuscarUsuario(string query)
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            List<Usuario> datos;
            if (query == null || query == "")
            {
                datos = service.ObtenerListaUsuarios().Where(o => o.Activo_Inactivo).ToList();
            }
            else
            {
                datos = service.ObtenerListaUsuarios().Where(o => o.DNI.Contains(query) && o.Activo_Inactivo).ToList();
            }
            ViewBag.datos = query;
            return View(datos);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear()
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            ViewBag.TipoUsuarios = service.ObtenerListaDeTipoUsuarios().ToList();
            return View(new Usuario());
        }
        [Authorize]
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
            validar(usuario,RepitaPassword);
            validar1(usuario, RepitaPassword);
            validar2(usuario, RepitaPassword);
            validar3(usuario, RepitaPassword);
            if (ModelState.IsValid)
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
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
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
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            if (id == 0 || id == null) { return RedirectToAction("Index", "Error"); }

            var UsuarioDb = service.ObtenerUsuarioPorId(id);
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath(RutaImagen), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                usuario.Perfil = RutaI + Path.GetFileName(file.FileName);
                UsuarioDb.Perfil = usuario.Perfil;
            }
            validarEditarUsuario(usuario, id);
            validarEditarUsuario1(usuario, id);
            validarEditarUsuario2(usuario, id);
            validarEditarUsuario3(usuario, id);
            if (ModelState.IsValid)
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
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            service.EliminarUsuario(id);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult VerMiCuenta()
        {
            if (!sessionService.EstaLogueadoComoCliente()) { return RedirectToAction("Index", "Error"); }
                var UsuarioDb = service.ObtenerUsuarioPorId(service.BuscarIdUsuarioSession());
                return View(UsuarioDb);
        }
        [Authorize]
        [HttpGet]
        public ActionResult ActualizarDatosUCliente()
        {
            if (!sessionService.EstaLogueadoComoCliente()) { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            if (usuarioIdDB != 0)
            {
                var UsuarioDb = service.ObtenerListaUsuarios().First(o => o.IdUsuario == usuarioIdDB);
                return View(UsuarioDb);
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ActualizarDatosUCliente(Usuario usuario, HttpPostedFileBase archivo)
        {
            if (!sessionService.EstaLogueadoComoCliente()) { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UsuarioDb = service.ObtenerListaUsuarios().First(o => o.IdUsuario == usuarioIdDB);

            if (archivo != null && archivo.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath(RutaImagen), Path.GetFileName(archivo.FileName));
                archivo.SaveAs(ruta);
                usuario.Perfil = RutaI + Path.GetFileName(archivo.FileName);
                UsuarioDb.Perfil = usuario.Perfil;
            }

            validarActualizarDatos(usuario);
            validarActualizarDatos1(usuario);
            validarActualizarDatos2(usuario);
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
            if (!sessionService.EstaLogueadoComoCliente()) { return RedirectToAction("Index", "Error"); }
            return View(new Usuario());
        }
        [Authorize]
        [HttpPost]
        public ActionResult CambiarContraUsuario(Usuario usuario, string NuevaPassword, string RepitaPassword)
        {
            ValidarCambiarContra(usuario, NuevaPassword, RepitaPassword);ValidarCambiarContra1(usuario, NuevaPassword, RepitaPassword);
            if (ModelState.IsValid)
            { 
                var usuarioIdDB = Convert.ToInt32(Session["UsuarioId"]);
                var UsuarioDb = service.ObtenerListaUsuarios().First(o => o.IdUsuario == usuarioIdDB);
                service.CambiarContraUsuario(NuevaPassword,UsuarioDb);
                return RedirectToAction("Logout", "Auth");
            }
            return View(usuario);
        }

        [Authorize]
        [HttpGet]
        public ActionResult VerMiCuentaAdmin()
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UsuarioDb = service.ObtenerListaUsuarios().First(o => o.IdUsuario == usuarioIdDB);
            return View(UsuarioDb);
        }
        [Authorize]
        [HttpGet]
        public ActionResult ActualizarDatosUAdmin()
        {
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UsuarioObtenido = service.ObtenerListaUsuarios().First(o => o.IdUsuario == usuarioIdDB);
            return View(UsuarioObtenido);
        }
        [Authorize]
        [HttpPost]
        public ActionResult ActualizarDatosUAdmin(Usuario usuario, HttpPostedFileBase file)
        {
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            var UsuarioDb = service.ObtenerListaUsuarios().First(o => o.IdUsuario == usuarioIdDB);

            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath(RutaImagen), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                usuario.Perfil = RutaI + Path.GetFileName(file.FileName);
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
            if (!sessionService.EstaLogueadoComoAdministrador()) { return RedirectToAction("Index", "Error"); }
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
                var UsuarioDb = service.ObtenerListaUsuarios().First(o => o.IdUsuario == usuarioIdDB);
                service.CambiarContraUsuario(NuevaPassword, UsuarioDb);
                return RedirectToAction("Logout", "Auth");
            }
            return View(usuario);
        }

        private readonly AppConexionDB conexion = new AppConexionDB();
        public void ValidarCambiarContra(Usuario usuario, string NuevaPassword, string RepitaPassword)
        {
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            if (usuario.Password != null && usuario.Password != "" && service.Existe(usuario, usuarioIdDB) == 0)
            {
                    ModelState.AddModelError("Password", "Contraseña no encontrada");
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
        }
        public void ValidarCambiarContra1(Usuario usuario, string NuevaPassword, string RepitaPassword)
        {
            var usuarioIdDB = service.BuscarIdUsuarioSession();
            if (usuario.Password != null && usuario.Password != "" && service.Existe(usuario, usuarioIdDB) != 0 && usuario.Password != NuevaPassword)
            {
                if (NuevaPassword == null || NuevaPassword == "")
                    ModelState.AddModelError("NuevaPassword", "Este campo es obligatorio");
                if (RepitaPassword == null || RepitaPassword == "")
                    ModelState.AddModelError("RepitaPassword", "Este campo es obligatorio");
                if (NuevaPassword != null && NuevaPassword != "" && RepitaPassword != null && RepitaPassword != "" && NuevaPassword != RepitaPassword)
                {
                    ModelState.AddModelError("RepitaPassword", "Las contraseñas no coinciden");
                }
            }
            if (usuario.Password != null && usuario.Password != "" && service.Existe(usuario, usuarioIdDB) != 0 && usuario.Password == NuevaPassword)
            {
                ModelState.AddModelError("NuevaPassword", "Debe ser diferente a la actual");
            }
        }



        public void validarActualizarDatos(Usuario usu)
        {
            if (usu.Nombre == null || usu.Nombre == "")
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
            if (usu.Nombre != null && !Regex.IsMatch(usu.Nombre, @"^[a-zA-Z ]*$"))
            {
                    ModelState.AddModelError("Nombre", "El campo nombre solo acepta letras");
            }

            if (usu.Apellido == null || usu.Apellido == "")
                ModelState.AddModelError("Apellido", "El apellido es obligatorio");
            if (usu.Apellido != null && !Regex.IsMatch(usu.Apellido, @"^[a-zA-Z ]*$"))
            {
                    ModelState.AddModelError("Apellido", "El campo apellido solo acepta letras");
            }

            if (usu.DNI == null || usu.DNI == "")
                ModelState.AddModelError("DNI", "El DNI es obligatorio");

            if (usu.DNI != null && !Regex.IsMatch(usu.DNI, "^\\d+$"))
            {
                    ModelState.AddModelError("DNI", "El campo dni solo acepta numeros");
            }

            if (usu.DNI != null && Regex.IsMatch(usu.DNI, "^\\d+$") && usu.DNI.Length != 8)
            {
                        ModelState.AddModelError("DNI", "El campo dni debe de tener 8 numeros");
            }
        }
        public void validarActualizarDatos1(Usuario usuario)
        {
            if (usuario.DNI != null && usuario.DNI != "" && Regex.IsMatch(usuario.DNI, "^\\d+$") && usuario.DNI.Length == 8)
            {
                var DNIUsuarioDB = Convert.ToString(Session["UsuarioDNI"]);
                var UsuDNI = conexion.Usuarios.First(a => a.DNI == DNIUsuarioDB);
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
        public void validarActualizarDatos2(Usuario usuario)
        {
            if (usuario.Celular == null || usuario.Celular == "")
                ModelState.AddModelError("Celular", "El celular es obligatorio");

            if (usuario.Celular != null && !Regex.IsMatch(usuario.Celular, "^\\d+$"))
            {
                ModelState.AddModelError("Celular", "El campo celular solo acepta numeros");
            }
        }

        public void validar(Usuario usuario, string RepitaPassword)
        {

            Usuario u1 = usuario,u2=usuario,u3=usuario,u4=usuario,u5=usuario,u6=usuario,u7=usuario;
            if (u1.Nombre == null || u1.Nombre == "")
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
            if (u2.Nombre != null && !Regex.IsMatch(u2.Nombre, @"^[a-zA-Z ]*$"))
            {
                    ModelState.AddModelError("Nombre", "El campo nombre solo acepta letras");
            }

            if (u3.Apellido == null || u3.Apellido == "")
                ModelState.AddModelError("Apellido", "El apellido es obligatorio");
            if (u4.Apellido != null && !Regex.IsMatch(u4.Apellido, @"^[a-zA-Z ]*$"))
            {
                    ModelState.AddModelError("Apellido", "El campo apellido solo acepta letras");
            }

            if (u5.DNI == null || u5.DNI == "")
                ModelState.AddModelError("DNI", "El DNI es obligatorio");

            if (u6.DNI != null && !Regex.IsMatch(u6.DNI, "^\\d+$"))
            {
                    ModelState.AddModelError("DNI", "El campo dni solo acepta numeros");
            }

            if (u7.DNI != null && Regex.IsMatch(u7.DNI, "^\\d+$") && usuario.DNI.Length != 8)
            {
                        ModelState.AddModelError("DNI", "El campo dni debe de tener 8 numeros");
            }
           
        }
        public void validar1(Usuario usuario, string RepitaPassword)
        {
            if (usuario.DNI != null && Regex.IsMatch(usuario.DNI, "^\\d+$") && usuario.DNI.Length == 8)
            {
                if (service.ExisteDNIUsuario(usuario))
                {
                    ModelState.AddModelError("DNI", "Este DNI ya existe");
                }
            }

            Usuario Vu1 = usuario, Vu2 = usuario, Vu3 = usuario, Vu4 = usuario, Vu5 = usuario;
            if (Vu1.Celular == null || Vu1.Celular == "")
                ModelState.AddModelError("Celular", "El celular es obligatorio");

            if (Vu2.Celular != null && !Regex.IsMatch(Vu2.Celular, "^\\d+$"))
            {
                ModelState.AddModelError("Celular", "El campo celular solo acepta numeros");
            }

            if (Vu3.Celular != null && !Regex.IsMatch(Vu3.Celular, "^\\d+$") && usuario.Celular.Length != 9)
            {
                ModelState.AddModelError("Celular", "El campo celular debe de tener 9 numeros");
            }

            if (Vu4.Celular != null && Regex.IsMatch(Vu4.Celular, "^\\d+$") && Vu4.Celular.Length == 9 && Vu4.Celular.Substring(0, 1) != "9")
            {
                ModelState.AddModelError("Celular", "El campo celular debe de empezar con 9");
            }

            if (Vu5.Correo == null || Vu5.Correo == "")
                ModelState.AddModelError("Correo", "El correo es obligatorio");

           
        }
        public void validar2(Usuario usuario, string RepitaPassword)
        {
            Usuario V2usu1 = usuario, V2usu2 = usuario;
            if (V2usu1.Correo != null && !Regex.IsMatch(V2usu1.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {
                ModelState.AddModelError("Correo", "El formato debe ser de correo");
            }

            if (V2usu2.Correo != null && Regex.IsMatch(V2usu2.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {
                if (service.ExisteCorreoUsuario(usuario))
                {
                    ModelState.AddModelError("Correo", "Este correo ya existe");
                }
            }

        }
        public void validar3(Usuario usuario, string RepitaPass)
        {
            Usuario V3usu1 = usuario, V3usu2 = usuario, V3usu3 = usuario;
            if (V3usu1.Direccion == null || V3usu1.Direccion == "")
                ModelState.AddModelError("Direccion", "La direccion es obligatorio");

            if (V3usu2.Password == null || V3usu2.Password == "")
                ModelState.AddModelError("Password", "El pasword es obligatorio");

            if (RepitaPass == null || RepitaPass == "")
                ModelState.AddModelError("RepitaPassword", "Este campo es obligatorio");

            if (V3usu3.Password != null && RepitaPass != null && V3usu3.Password != RepitaPass)
            {
                ModelState.AddModelError("RepitaPassword", "Los passwords no coinciden");
            }
        }
        public void validarEditarUsuario(Usuario usuario, int? id)
        {

            if (usuario.Nombre == null || usuario.Nombre == "")
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");
            if (usuario.Nombre != null && !Regex.IsMatch(usuario.Nombre, @"^[a-zA-Z ]*$"))
            {
                    ModelState.AddModelError("Nombre", "El campo nombre solo acepta letras");
            }

            if (usuario.Apellido == null || usuario.Apellido == "")
                ModelState.AddModelError("Apellido", "El apellido es obligatorio");
            if (usuario.Apellido != null && !Regex.IsMatch(usuario.Apellido, @"^[a-zA-Z ]*$"))
            {
                    ModelState.AddModelError("Apellido", "El campo apellido solo acepta letras");
            }

            if (usuario.DNI == null || usuario.DNI == "")
                ModelState.AddModelError("DNI", "El DNI es obligatorio");

            if (usuario.DNI != null && !Regex.IsMatch(usuario.DNI, "^\\d+$"))
            {
                    ModelState.AddModelError("DNI", "El campo dni solo acepta numeros");
            }

            if (usuario.DNI != null && Regex.IsMatch(usuario.DNI, "^\\d+$") && usuario.DNI.Length != 8)
            {
                        ModelState.AddModelError("DNI", "El campo dni debe de tener 8 numeros");
            }

        }

        public void validarEditarUsuario1(Usuario usuario, int? id)
        {
            Usuario VEu1 = usuario, VEu2 = usuario, VEu3 = usuario, VEu4 = usuario, VEu5 = usuario, VEu6=usuario,VEu7=usuario;
            if (VEu1.Celular == null || VEu1.Celular == "")
                ModelState.AddModelError("Celular", "El celular es obligatorio");

            if (VEu2.Celular != null && !Regex.IsMatch(VEu2.Celular, "^\\d+$"))
            {
                ModelState.AddModelError("Celular", "El campo celular solo acepta numeros");
            }

            if (VEu3.Celular != null && !Regex.IsMatch(VEu3.Celular, "^\\d+$") && usuario.Celular.Length != 9)
            {
                ModelState.AddModelError("Celular", "El campo celular debe de tener 9 numeros");
            }

            if (VEu4.Celular != null && Regex.IsMatch(VEu4.Celular, "^\\d+$") && VEu4.Celular.Length == 9 && VEu4.Celular.Substring(0, 1) != "9")
            {
                ModelState.AddModelError("Celular", "El campo celular debe de empezar con 9");
            }

            if (VEu5.Correo == null || VEu5.Correo == "")
                ModelState.AddModelError("Correo", "El correo es obligatorio");

            if (VEu6.Correo != null && !Regex.IsMatch(VEu6.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {
                ModelState.AddModelError("Correo", "El formato debe ser de correo");
            }


            if (VEu7.Direccion == null || VEu7.Direccion == "")
                ModelState.AddModelError("Direccion", "La direccion es obligatorio");

            
        }
        public void validarEditarUsuario2(Usuario usuario, int? id)
        {

            if (usuario.Password == null || usuario.Password == "")
                ModelState.AddModelError("Password", "El pasword es obligatorio");

            if (usuario.DNI != null && usuario.DNI != "" && Regex.IsMatch(usuario.DNI, "^\\d+$") && usuario.DNI.Length == 8)
            {
                
                var IdUsuarioDB = service.ObtenerUsuarioPorId(id);
                var UsuDNI = service.ObtenerUsuarioPorDNI(IdUsuarioDB);
                if (UsuDNI.DNI != usuario.DNI)
                {
                    if (service.ExisteDNIUsuario(usuario))
                    {
                        ModelState.AddModelError("DNI", "Este DNI ya existe");
                    }
                }
            }
            
        }
        public void validarEditarUsuario3(Usuario usuario, int? id)
        {
            if (usuario.Correo != null && Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {

                var UsuarioDBC = service.ObtenerUsuarioPorId(id);
                var UsuCorreo = service.ObtenerUsuarioPorCorreo(UsuarioDBC); 
                if (UsuCorreo.Correo != usuario.Correo)
                {
                    if (service.ExisteCorreoUsuario(usuario))
                    {
                        ModelState.AddModelError("Correo", "Este Correo ya existe");
                    }
                }
            }
        }
    }
}