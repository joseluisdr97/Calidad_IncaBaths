using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_INCABATHS.Servicios
{
    public class UsuarioService: IUsuarioService
    {
        private readonly AppConexionDB conexion;
        public UsuarioService()
        {
            this.conexion = new AppConexionDB();
        }
        public List<Usuario> ObtenerListaUsuarios()
        {
            return conexion.Usuarios.Include(u => u.TipoUsuario).ToList();
        }
        public List<TipoUsuario> ObtenerListaDeTipoUsuarios()
        {
            return conexion.TipoUsuarios.ToList();
        }
        public void CrearUsuario(Usuario usuario)
        {
            conexion.Usuarios.Add(usuario);
            usuario.Activo_Inactivo = true;
            conexion.SaveChanges();
        }
        public Usuario ObtenerUsuarioPorId(int? id)
        {
            return conexion.Usuarios.Find(id);
        }
        public void EditarUsuario(Usuario usuario, Usuario UsuarioDb)
        {
            UsuarioDb.Nombre = usuario.Nombre;
            UsuarioDb.IdTipoUsuario = usuario.IdTipoUsuario;
            UsuarioDb.Password = usuario.Password;
            UsuarioDb.Celular = usuario.Celular;
            UsuarioDb.Correo = usuario.Correo;
            UsuarioDb.DNI = usuario.DNI;
            UsuarioDb.Direccion = usuario.Direccion;
            conexion.SaveChanges();
        }
        public void EliminarUsuario(int? id)
        {
            var UsuarioDb = ObtenerUsuarioPorId(id);
            UsuarioDb.Activo_Inactivo = false;
            conexion.SaveChanges();
        }
        public void ActualizarDatosUsuario(Usuario usuario, Usuario UsuarioDb)
        {
            UsuarioDb.Nombre = usuario.Nombre;
            UsuarioDb.Apellido = usuario.Apellido;
            UsuarioDb.DNI = usuario.DNI;
            UsuarioDb.Direccion = usuario.Direccion;
            UsuarioDb.Celular = usuario.Celular;
            conexion.SaveChanges();
        }
        public void CambiarContraUsuario(string NuevaPassword,Usuario UsuarioDb)
        {
            UsuarioDb.Password = NuevaPassword;
            conexion.SaveChanges();
        }
        public int BuscarIdUsuarioSession()
        {
            return Convert.ToInt32(HttpContext.Current.Session["UsuarioId"]);
        }
        public int Existe(Usuario usuario,int usuarioIdDB)
        {
            return conexion.Usuarios.Count(u => u.IdUsuario == usuarioIdDB && u.Password == usuario.Password);
        }
    }
}