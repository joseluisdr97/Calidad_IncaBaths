using PROYECTO_INCABATHS.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_INCABATHS
{
    public interface IServicioService
    {
            List<Servicio> ObtenerListaServicios();
            Servicio ObtenerServicioPorId(int? id);
            void EditarServicio(int? id, Servicio servicio);
            void GuardarServicio(Servicio servicio);
            int ContarTurnosDelServicio(int? id);
            List<Turno> ListaDeTurnos();
            Turno ObtenerTurnoPorId(int? id);
            void EliminarTurnosDelServicio(int? id);
            void EliminarServicio(int? id);

    }
}
