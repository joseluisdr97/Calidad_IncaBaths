using PROYECTO_INCABATHS.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_INCABATHS.Interfaces
{
    public interface IReservaService
    {
        List<Reserva> ObtenerListaReservas();
        List<Usuario> ObtenerListaUsuarios();
        List<Servicio> ObtenerListaServicios();
        void CrearReserva(int idUsuario, Reserva reserva);
        List<DetalleReserva> ObtenerDetalleReserva();
        List<Turno> ObtenerListaTurnos();
        void EliminarDetalleReserva(DetalleReserva ReservaDb);
        void EliminarReserva(Reserva ReservaDb);
    }
}
