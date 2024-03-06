using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FloraCFHN.AccesoADatos;
using FloraCFHN.EntidadesDeNegocio;

namespace FloraCFHN.LogicaDeNegocio
{
    public class UsuarioBL
    {
        public async Task<int> CrearAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.CrearAsync(pUsuario);
        }

        public async Task<int> ModificarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.ModificarAsync(pUsuario);
        }

        public async Task<int> EliminarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.EliminarAsync(pUsuario);
        }

        public async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.ObtenerPorIdAsync(pUsuario);
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await UsuarioDAL.ObtenerTodosAsync();
        }

        public async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.BuscarAsync(pUsuario);
        }

        public async Task<List<Usuario>> BuscarIncluirRolAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.BuscarIncluirRolAsync(pUsuario);

        }

        public async Task<Usuario> LoginAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.LoginAsync(pUsuario);
        }

        public async Task<int> CambiarPasswordAsync(Usuario pUsuario, string pPasswordActual)
        {
            return await UsuarioDAL.CambiarPasswordAsync(pUsuario, pPasswordActual);
        }
    }
}
