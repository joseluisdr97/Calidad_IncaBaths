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
    public class TurnoService:ITurnoService
    {
        private AppConexionDB conexion;

        public TurnoService()
        {
            this.conexion = new AppConexionDB();
        }
        public List<Turno> ObtenerTurnos()
        {
            return conexion.Turnos.Include(t => t.Servicio).ToList();
        }
        public Servicio ObtenerServicioPorId(int? id)
        {
            if (id == null) return null;
            Servicio servicio = conexion.Servicios.Where(a=>a.IdServicio==id).FirstOrDefault();
            return servicio;
        }
        public void GuardarTurno(int? id,Turno turno)
        {
            turno.IdServicio = Convert.ToInt32(id);

            conexion.Turnos.Add(turno);
            turno.Activo_Inactivo = true;
            conexion.SaveChanges();
        }
        public void EditarTurno(Turno turno,Turno DbTurno)
        {
            DbTurno.HoraInicio = turno.HoraInicio;
            DbTurno.HoraFin = turno.HoraFin;
            DbTurno.Fecha = turno.Fecha;
            conexion.SaveChanges();
        }
        public Turno ObtenerTurnoPorId(int? id)
        {
            if (id == null) return null;
            return conexion.Turnos.Find(id);
        }
        public void EliminarTurno(Turno turno)
        {
            turno.Activo_Inactivo = false;
            conexion.SaveChanges();
        }
    }
}