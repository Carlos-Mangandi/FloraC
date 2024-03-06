using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FloraCFHN.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace FloraCFHN.AccesoADatos
{
    public class CategoriaDAL
    {
        public static async Task<int> CrearAsync(Categoria pCategoria)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pCategoria);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }


        public static async Task<int> ModificarAsync(Categoria pCategoria)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var categoria = await bdContexto.Categorias.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);

                categoria.Nombre = pCategoria.Nombre;

                
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }


        public static async Task<int> EliminarAsync(Categoria pCategoria)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var categoria = await bdContexto.Categorias.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
                bdContexto.Categorias.Remove(categoria);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }


        public static async Task<Categoria> ObtenerPorIdAsync(Categoria pCategoria)
        {
            var categoria = new Categoria();
            using (var bdContexto = new BDContexto())
            {
                categoria = await bdContexto.Categorias.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
            }

            return categoria;
        }


        public static async Task<List<Categoria>> ObtenerTodosAsync()
        {
            var categorias = new List<Categoria>();
            using (var bdContexto = new BDContexto())
            {
                categorias = await bdContexto.Categorias.ToListAsync();
            }

            return categorias;
        }


        internal static IQueryable<Categoria> QuerySelect(IQueryable<Categoria> pQuery, Categoria pCategoria)
        {
            if (pCategoria.Id > 0)
                pQuery = pQuery.Where(c => c.Id == pCategoria.Id);

            if (!string.IsNullOrEmpty(pCategoria.Nombre))
                pQuery = pQuery.Where(c => c.Nombre.Contains(pCategoria.Nombre));

            pQuery = pQuery.OrderByDescending(c => c.Id).AsQueryable();

            if (pCategoria.top_aux > 0)
                pQuery = pQuery.Take(pCategoria.top_aux).AsQueryable();

            return pQuery;
        }


        public static async Task<List<Categoria>> BuscarAsync(Categoria pCategoria)
        {
            var categorias = new List<Categoria>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Categorias.AsQueryable();
                select = QuerySelect(select, pCategoria);
                categorias = await select.ToListAsync();
            }

            return categorias;
        }
    }
}
