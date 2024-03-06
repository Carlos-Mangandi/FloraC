using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FloraCFHN.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FloraCFHN.AccesoADatos
{
    public class UsuarioDAL
    {
        private static void EncriptarMD5(Usuario pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pUsuario.Password));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pUsuario.Password = strEncriptar;
            }
        }

        private static async Task<bool> ExisteLogin(Usuario pUsuario, BDContexto pDbContext)
        {
            bool result = false;
            var loginUsuarioExiste = await pDbContext.Usuarios.FirstOrDefaultAsync(u => u.Login == pUsuario.Login && u.Id != pUsuario.Id);
            if (loginUsuarioExiste != null && loginUsuarioExiste.Id > 0 && loginUsuarioExiste.Login == pUsuario.Login)
                result = true;
            return result;
        }


        public static async Task<int> CrearAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, bdContexto);
                if (existeLogin == false)
                {
                    pUsuario.Estatus = (byte)Estatus_Usuario.ACTIVO;
                    pUsuario.FechaRegistro = DateTime.Now;
                    EncriptarMD5(pUsuario);
                    bdContexto.Add(pUsuario);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");

            }

            return result;
        }


        public static async Task<int> ModificarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, bdContexto);
                if (existeLogin == false)
                {
                    var usuario = await bdContexto.Usuarios.FirstOrDefaultAsync(u => u.Id == pUsuario.Id);
                    usuario.RolId = pUsuario.RolId;
                    usuario.Nombre = pUsuario.Nombre;
                    usuario.Apellido = pUsuario.Apellido;
                    usuario.Telefono = pUsuario.Telefono;
                    usuario.Login = pUsuario.Login;
                    usuario.Estatus = pUsuario.Estatus;

                    // guardar los cambios
                    bdContexto.Update(usuario);
                    result = await bdContexto.SaveChangesAsync();

                }
                else
                    throw new Exception("Login ya existe");

            }
            return result;
        }


        public static async Task<int> EliminarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var usuario = await bdContexto.Usuarios.FirstOrDefaultAsync(u => u.Id == pUsuario.Id);
                bdContexto.Usuarios.Remove(usuario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }


        public static async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var bdContexto = new BDContexto())
            {
                usuario = await bdContexto.Usuarios.FirstOrDefaultAsync(u => u.Id == pUsuario.Id);
            }

            return usuario;
        }


        public static async Task<List<Usuario>> ObtenerTodosAsync()
        {
            var usuarios = new List<Usuario>();
            using (var bdContexto = new BDContexto())
            {
                usuarios = await bdContexto.Usuarios.ToListAsync();
            }

            return usuarios;
        }


        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario pUsuario)
        {
            if (pUsuario.Id > 0)
                pQuery = pQuery.Where(u => u.Id == pUsuario.Id);

            if (pUsuario.RolId > 0)
                pQuery = pQuery.Where(u => u.RolId == pUsuario.RolId);

            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
                pQuery = pQuery.Where(u => u.Nombre.Contains(pUsuario.Nombre));

            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
                pQuery = pQuery.Where(u => u.Apellido.Contains(pUsuario.Apellido));

            if (!string.IsNullOrWhiteSpace(pUsuario.Telefono))
                pQuery = pQuery.Where(u => u.Telefono.Contains(pUsuario.Telefono));

            if (!string.IsNullOrWhiteSpace(pUsuario.Login))
                pQuery = pQuery.Where(u => u.Login.Contains(pUsuario.Login));

            if (pUsuario.Estatus > 0)
                pQuery = pQuery.Where(u => u.Estatus == pUsuario.Estatus);

            if (pUsuario.FechaRegistro.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pUsuario.FechaRegistro.Year, pUsuario.FechaRegistro.Month, pUsuario.FechaRegistro.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaRegistro >= fechaInicial && s.FechaRegistro <= fechaFinal);
            }

            pQuery = pQuery.OrderByDescending(u => u.Id).AsQueryable();

            if (pUsuario.top_aux >= 0)
                pQuery = pQuery.Take(pUsuario.top_aux).AsQueryable();

            return pQuery;
        }


        public static async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Usuarios.AsQueryable();
                select = QuerySelect(select, pUsuario);
                usuarios = await select.ToListAsync();
            }

            return usuarios;
        }


        public static async Task<List<Usuario>> BuscarIncluirRolAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Usuarios.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(u => u.Rol).AsQueryable();
                usuarios = await select.ToListAsync();
            }

            return usuarios;
        }


        public static async Task<Usuario> LoginAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var bdContexto = new BDContexto())
            {
                EncriptarMD5(pUsuario);
                usuario = await bdContexto.Usuarios.FirstOrDefaultAsync(u => u.Login == pUsuario.Login &&
                u.Password == pUsuario.Password && u.Estatus == (byte)Estatus_Usuario.ACTIVO);
            }
            return usuario;
        }


        public static async Task<int> CambiarPasswordAsync(Usuario pUsuario, string pPasswordAnt)
        {
            int result = 0;
            var usuarioPassAnt = new Usuario { Password = pPasswordAnt };
            EncriptarMD5(usuarioPassAnt);
            using (var bdContexto = new BDContexto())
            {
                var usuario = await bdContexto.Usuarios.FirstOrDefaultAsync(u => u.Id == pUsuario.Id);
                if (usuarioPassAnt.Password == usuario.Password)
                {
                    EncriptarMD5(pUsuario);
                    usuario.Password = pUsuario.Password;
                    bdContexto.Update(usuario);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("El password actual es incorrecto");
            }
            return result;
        }

    }
}