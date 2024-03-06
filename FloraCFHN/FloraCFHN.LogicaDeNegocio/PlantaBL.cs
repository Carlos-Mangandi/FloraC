using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FloraCFHN.AccesoADatos;
using FloraCFHN.EntidadesDeNegocio;

namespace FloraCFHN.LogicaDeNegocio
{
    public class PlantaBL
    {
        public async Task<int> CrearAsync(Planta pPlanta)
        {
            return await PlantaDAL.CrearAsync(pPlanta);
        }

        public async Task<int> ModificarAsync(Planta pPlanta)
        {
            return await PlantaDAL.ModificarAsync(pPlanta);
        }

        public async Task<int> EliminarAsync(Planta pPlanta)
        {
            return await PlantaDAL.EliminarAsync(pPlanta);
        }

        public async Task<Planta> ObtenerPorIdAsync(Planta pPlanta)
        {
            return await PlantaDAL.ObtenerPorIdAsync(pPlanta);
        }

        public async Task<List<Planta>> ObtenerTodosAsync()
        {
            return await PlantaDAL.ObtenerTodosAsync();
        }

        public async Task<List<Planta>> BuscarAsync(Planta pPlanta)
        {
            return await PlantaDAL.BuscarAsync(pPlanta);
        }

        public async Task<List<Planta>> BuscarIncluirCategoriaAsync(Planta pPlanta)
        {
            return await PlantaDAL.BuscarIncluirCategoriaAsync(pPlanta);
        }
    }
}
