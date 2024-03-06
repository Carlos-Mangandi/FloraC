using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FloraCFHN.EntidadesDeNegocio;
using FloraCFHN.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FloraCFHN.InterfazGraficaMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CatalogoController : Controller
    {
        PlantaBL plantaBL = new PlantaBL();
        CategoriaBL categoriaBL = new CategoriaBL();

        // GET: CatalogoController
        public async Task<IActionResult> Index(Planta pPlanta = null)
        {
            
            if (pPlanta == null)
                pPlanta = new Planta();

            if (pPlanta.top_aux == 0)
                pPlanta.top_aux = 10;

            else if (pPlanta.top_aux == -1)
                pPlanta.top_aux = 0;

            var plantas = await plantaBL.BuscarIncluirCategoriaAsync(pPlanta);

            ViewBag.Top = pPlanta.top_aux;
            ViewBag.Categorias = await categoriaBL.ObtenerTodosAsync();

            return View(plantas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var planta = await plantaBL.ObtenerPorIdAsync(new Planta { Id = id });
            planta.Categoria = await categoriaBL.ObtenerPorIdAsync(new Categoria { Id = planta.CategoriaId });
            return View(planta);
        }
    }
}
