using PROYECTO_INCABATHS.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_INCABATHS.Interfaces
{
    public interface IUsuarioService
    {
        List<Usuario> ObtenerListaUsuarios();
        List<TipoUsuario> ObtenerListaDeTipoUsuarios();
        void CrearUsuario(Usuario usuario);
        Usuario ObtenerUsuarioPorId(int? id);
        void EditarUsuario(Usuario usuario, Usuario UsuarioDb);
        void EliminarUsuario(int? id);
        void ActualizarDatosUsuario(Usuario usuario, Usuario UsuarioDb);
        void CambiarContraUsuario(string NuevaPassword, Usuario UsuarioDb);
        int BuscarIdUsuarioSession();
    }
}
