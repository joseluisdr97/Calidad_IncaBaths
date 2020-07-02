using PROYECTO_INCABATHS.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_INCABATHS.Interfaces
{
    public interface IAuthService
    {
        List<Usuario> ObtenerListaUsuarios();
        void GuardarCookie(string Correo);
        void CerrarSession();
    }
}
