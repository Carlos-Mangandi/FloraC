using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FloraCFHN.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace FloraCFHN.AccesoADatos
{
    public class PlantaDAL
    {

        //Crear metodo de crear
        public static async Task<int> CrearAsync(Planta pPlanta)
        {
            int result = 0;
            using(var bdContexto = new BDContexto())
            {
                bdContexto.Add(pPlanta);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }


        public static async Task<int> ModificarAsync(Planta pPlanta)
        {
            int result = 0;
            using(var bdContexto = new BDContexto())
            {
                var planta = await bdContexto.Plantas.FirstOrDefaultAsync(p => p.Id == pPlanta.Id);
                {
                    planta.Nombre = pPlanta.Nombre;
                    planta.Descripcion = pPlanta.Descripcion;
                    planta.ImagenUrl = pPlanta.ImagenUrl;
                    planta.CategoriaId = pPlanta.CategoriaId;


                    bdContexto.Update(planta);
                    result = await bdContexto.SaveChangesAsync();
                }

                return result;
            }
        }


        public static async Task<int> EliminarAsync(Planta pPlanta)
        {
            int result = 0;
            using(var bdContexto = new BDContexto())
            {
                var planta = await bdContexto.Plantas.FirstOrDefaultAsync(p => p.Id == pPlanta.Id);
                bdContexto.Plantas.Remove(planta);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }


        public static async Task<Planta> ObtenerPorIdAsync(Planta pPlanta)
        {
            var planta = new Planta();
            using(var bdContexto = new BDContexto())
            {
                planta = await bdContexto.Plantas.FirstOrDefaultAsync(p => p.Id == pPlanta.Id);
            }

            return planta;
        }


        public static async Task<List<Planta>> ObtenerTodosAsync()
        {
            var plantas = new List<Planta>();
            using (var bdContexto = new BDContexto())
            {
                plantas = await bdContexto.Plantas.ToListAsync();
            }

            return plantas;
        }


        internal static IQueryable<Planta> QuerySelect(IQueryable<Planta> pQuery, Planta pPlanta)
        {
            if (pPlanta.Id > 0)
                pQuery = pQuery.Where(p => p.Id == pPlanta.Id);

            if (pPlanta.CategoriaId > 0)
                pQuery = pQuery.Where(p => p.CategoriaId == pPlanta.CategoriaId);

            if (!string.IsNullOrEmpty(pPlanta.Nombre))
                pQuery = pQuery.Where(p => p.Nombre.Contains(pPlanta.Nombre));
            
            if (!string.IsNullOrEmpty(pPlanta.Descripcion))
                pQuery = pQuery.Where(p => p.Descripcion.Contains(pPlanta.Descripcion));

            //if (!string.IsNullOrEmpty(pPlanta.ImagenUrl))
            //    pQuery = pQuery.Where(p => p.ImagenUrl.Contains(pPlanta.ImagenUrl));

            pQuery = pQuery.OrderByDescending(p => p.Id).AsQueryable();

            if (pPlanta.top_aux > 0)
                pQuery = pQuery.Take(pPlanta.top_aux).AsQueryable();

            return pQuery;
        }


        public static async Task<List<Planta>> BuscarAsync(Planta pPlanta)
        {
            var plantas = new List<Planta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Plantas.AsQueryable();
                select = QuerySelect(select, pPlanta);
                plantas = await select.ToListAsync();
            }

            return plantas;
        }


        public static async Task<List<Planta>> BuscarIncluirCategoriaAsync(Planta pPlanta)
        {
            var plantas = new List<Planta>();
            using(var bdContexto = new BDContexto())
            {
                var select = bdContexto.Plantas.AsQueryable();
                select = QuerySelect(select, pPlanta).Include(p => p.Categoria).AsQueryable();
                plantas = await select.ToListAsync();
            }

            return plantas;
        }
    }
}
