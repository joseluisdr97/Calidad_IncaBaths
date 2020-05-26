using PROYECTO_INCABATHS.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_INCABATHS.Interfaces
{
    public interface IAdminService
    {
        List<Usuario> ObtenerListaUsuarios();
        List<Reserva> ObtenerListReservas();
        List<TipoUsuario> ObtenerListaTipoUsuarios();
        List<Servicio> ObtenerListaServicios();
        List<Turno> ObtenerListaTurnos();
        void CrearUsuario(Usuario usuario);
    }
}
