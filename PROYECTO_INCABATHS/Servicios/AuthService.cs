using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PROYECTO_INCABATHS.Servicios
{
    public class AuthService: IAuthService
    {
        private AppConexionDB conexion;
        public AuthService()
        {
            this.conexion = new AppConexionDB();
        }
        public List<Usuario> ObtenerListaUsuarios()
        {
            return conexion.Usuarios.ToList();
        }
        public void GuardarCookie(string Correo)
        {
            FormsAuthentication.SetAuthCookie(Correo, false);
        }
    }
}