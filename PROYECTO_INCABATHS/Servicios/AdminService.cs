using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_INCABATHS.Servicios
{
    public class AdminService: IAdminService
    {
        private readonly AppConexionDB conexion;
        public AdminService()
        {
            this.conexion = new AppConexionDB();
        }
        public List<Usuario> ObtenerListaUsuarios()
        {
            return conexion.Usuarios.ToList();
        }
        public List<Reserva> ObtenerListReservas()
        {
            return conexion.Reservas.ToList();
        }
        public List<TipoUsuario> ObtenerListaTipoUsuarios()
        {
            return conexion.TipoUsuarios.ToList();
        }
        public List<Servicio> ObtenerListaServicios()
        {
            return conexion.Servicios.ToList();
        }
        public List<Turno> ObtenerListaTurnos()
        {
            return conexion.Turnos.ToList();
        }
        public void CrearUsuario(Usuario usuario)
        {
            conexion.Usuarios.Add(usuario);
            usuario.Activo_Inactivo = true;
            conexion.SaveChanges();
        }
    }
}