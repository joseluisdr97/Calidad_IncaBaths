using PROYECTO_INCABATHS.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_INCABATHS.Interfaces
{
    public interface ITurnoService
    {
        List<Turno> ObtenerTurnos();
        Servicio ObtenerServicioPorId(int? id);
        void GuardarTurno(int? id, Turno turno);
        void EditarTurno(Turno turno, Turno DbTurno);
        Turno ObtenerTurnoPorId(int? id);
        void EliminarTurno(Turno turno);
    }
}
