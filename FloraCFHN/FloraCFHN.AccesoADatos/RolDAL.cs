using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FloraCFHN.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace FloraCFHN.AccesoADatos
{
    public class RolDAL
    {
        public static async Task<int> CrearAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pRol);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }


        public static async Task<int> ModificarAsync(Rol pRol)
        {
            int result = 0;
            using(var bdContexto = new BDContexto())
            {
                var rol = await bdContexto.Roles.FirstOrDefaultAsync(r => r.Id == pRol.Id);

                rol.Nombre = pRol.Nombre;

                
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }


        public static async Task<int> EliminarAsync(Rol pRol)
        {
            int result = 0;
            using(var bdContexto = new BDContexto())
            {
                var rol = await bdContexto.Roles.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                bdContexto.Roles.Remove(rol);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }


        public static async Task<Rol> ObtenerPorIdAsync(Rol pRol)
        {
            var rol = new Rol();
            using(var bdContexto= new BDContexto())
            {
                rol = await bdContexto.Roles.FirstOrDefaultAsync(r => r.Id == pRol.Id);
            }

            return rol;
        }


        public static async Task<List<Rol>> ObtenerTodosAsync()
        {
            var roles = new List<Rol>();
            using (var bdContexto = new BDContexto())
            {
                roles = await bdContexto.Roles.ToListAsync();
            }

            return roles;
        }


        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id > 0)
            {
                pQuery = pQuery.Where(r => r.Id == pRol.Id);
            }

            if (!string.IsNullOrEmpty(pRol.Nombre))
                pQuery = pQuery.Where(r => r.Nombre.Contains(pRol.Nombre));

            pQuery = pQuery.OrderByDescending(r => r.Id).AsQueryable();

            if (pRol.top_aux > 0)
                pQuery = pQuery.Take(pRol.top_aux).AsQueryable();

            return pQuery;
        }


        public static async Task<List<Rol>> BuscarAsync(Rol pRol)
        {
            var roles = new List<Rol>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Roles.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }

            return roles;
        }
    }
}
