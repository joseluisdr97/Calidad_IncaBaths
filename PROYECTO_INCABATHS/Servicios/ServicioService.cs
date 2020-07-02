using PROYECTO_INCABATHS.Clases;
using PROYECTO_INCABATHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_INCABATHS.Servicios
{

    public class ServicioService:IServicioService
    {
        private readonly AppConexionDB conexion;

        public ServicioService()
        {
            this.conexion = new AppConexionDB();
        }
        public List<Servicio> ObtenerListaServicios()
        {
            return conexion.Servicios.ToList();
        }

        public Servicio ObtenerServicioPorId(int? id)
        {
            return conexion.Servicios.Find(id);
        }

        public void EditarServicio(int? id,Servicio servicio)
        {
            var DBCategoria = ObtenerServicioPorId(id);
            DBCategoria.Nombre = servicio.Nombre;
            DBCategoria.Precio = servicio.Precio;
            DBCategoria.Aforo = servicio.Aforo;
            conexion.SaveChanges();
        }

        public void GuardarServicio(Servicio servicio)
        {
            conexion.Servicios.Add(servicio);
            servicio.Activo_Inactivo = true;
            conexion.SaveChanges();
        }

        public int ContarTurnosDelServicio(int? id)
        {
            return conexion.Turnos.Count(o => o.IdServicio == id && o.Activo_Inactivo);
        }

        public List<Turno> ListaDeTurnos()
        {
            return conexion.Turnos.ToList();
        }
        public Turno ObtenerTurnoPorId(int? id)
        {
            return conexion.Turnos.Where(o => o.IdTurno == id).First();
        }
        public void EliminarTurnosDelServicio(int? id)
        {
            var turnoDb1 = ListaDeTurnos().Where(o => o.IdServicio == id && o.Activo_Inactivo).ToList();

            for (int i = 0; i < turnoDb1.Count; i++)
            {
                var idturno = turnoDb1[i].IdTurno;
                var turnoDb = ObtenerTurnoPorId(idturno);
                turnoDb.Activo_Inactivo = false;
                conexion.SaveChanges();
            }
        }
        public void EliminarServicio(int? id)
        {
            var servicioDb = ObtenerServicioPorId(id);
            servicioDb.Activo_Inactivo = false;
            conexion.SaveChanges();
        }
    }
}