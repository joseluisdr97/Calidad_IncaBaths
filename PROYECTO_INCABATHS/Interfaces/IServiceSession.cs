using PROYECTO_INCABATHS.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_INCABATHS.Interfaces
{
    public interface IServiceSession
    {
        int BuscarIdUsuarioSession();
        string BuscarNombreUsuarioSession();
        void GuardarDatosUsuarioLogueado(Usuario UsuarioDB);
        bool EstaLogueado();
        bool EstaLogueadoComoAdministrador();
        bool EstaLogueadoComoCajero();
        bool EstaLogueadoComoCliente();
    }
}
