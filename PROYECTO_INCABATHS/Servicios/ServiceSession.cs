using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_INCABATHS.Servicios
{
    public class ServiceSession:IServiceSession
    {
        private readonly AppConexionDB conexion;
        public ServiceSession()
        {
            this.conexion = new AppConexionDB();
        }
        public int BuscarIdUsuarioSession()
        {
            return Convert.ToInt32(HttpContext.Current.Session["UsuarioId"]);
        }
        public string BuscarNombreUsuarioSession()
        {
            return Convert.ToString(HttpContext.Current.Session["UsuarioNombre"]);
        }
        public void GuardarDatosUsuarioLogueado(Usuario UsuarioDB)
        {
            HttpContext.Current.Session["UsuarioId"] = UsuarioDB.IdUsuario;
            HttpContext.Current.Session["UsuarioNombre"] = UsuarioDB.Nombre;
            HttpContext.Current.Session["UsuarioPerfil"] = UsuarioDB.Perfil;
            HttpContext.Current.Session["UsuarioDNI"] = UsuarioDB.DNI;
            HttpContext.Current.Session["UsuarioTipoUsuario"] = UsuarioDB.IdTipoUsuario;
        }
        public bool EstaLogueado()
        {
            if (HttpContext.Current.Session["UsuarioNombre"] != null)
                return true;

            return false;
        }

        public bool EstaLogueadoComoAdministrador()
        {
            if (EstaLogueado())
            {
                int UsuarioId = BuscarIdUsuarioSession();
                Usuario usuario = conexion.Usuarios.Where(a=>a.IdUsuario==UsuarioId).First();
                if (usuario.IdTipoUsuario == 1)
                    return true;
            }

            return false;
        }
        public bool EstaLogueadoComoCajero()
        {
            if (EstaLogueado())
            {
                int UsuarioId = BuscarIdUsuarioSession();
                Usuario usuario = conexion.Usuarios.Where(a => a.IdUsuario == UsuarioId).First();
                if (usuario.IdTipoUsuario == 2)
                    return true;
            }

            return false;
        }
        public bool EstaLogueadoComoCliente()
        {
            if (EstaLogueado())
            {
                int UsuarioId = BuscarIdUsuarioSession();
                Usuario usuario = conexion.Usuarios.Where(a => a.IdUsuario == UsuarioId).First();
                if (usuario.IdTipoUsuario == 3)
                    return true;
            }
            return false;
        }
    }
}