using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using PROYECTO_INCABATHS.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROYECTO_INCABATHS.Servicios
{
    public class ReservaService:IReservaService
    {
        private AppConexionDB conexion;
        public ReservaService()
        {
            this.conexion = new AppConexionDB();
        }
        public List<Reserva> ObtenerListaReservas()
        {
            return conexion.Reservas.Include(u => u.Usuario).ToList();
        }
        public List<Usuario> ObtenerListaUsuarios()
        {
            return conexion.Usuarios.ToList();
        }
        public List<Servicio> ObtenerListaServicios()
        {
            return conexion.Servicios.ToList();
        }
        public void CrearReserva(int idUsuario,Reserva reserva)
        {
            reserva.IdUsuario = idUsuario;
            reserva.IdModoPago = 1;
            reserva.Fecha = DateTime.Now;
            reserva.Activo_Inactivo = true;
            for (int i = 0; i < reserva.DetalleReservas.Count; i++)
            {
                reserva.DetalleReservas[i].Activo_Inactivo = true;
            }
            conexion.Reservas.Add(reserva);
            conexion.SaveChanges();
        }
        public List<DetalleReserva> ObtenerDetalleReserva()
        {
            return conexion.DetalleReservas.Include(o => o.Servicio).Include(t => t.Turno).ToList();
        }
        public List<Turno> ObtenerListaTurnos()
        {
            return  conexion.Turnos.ToList();
        }
        public void EliminarReserva(Reserva DbReserva)
        {
            DbReserva.Activo_Inactivo = false;
            conexion.SaveChanges();
        }
        public void EliminarDetalleReserva(DetalleReserva ReservaDb)
        {
            ReservaDb.Activo_Inactivo = false;
            conexion.SaveChanges();
        }
    }
}