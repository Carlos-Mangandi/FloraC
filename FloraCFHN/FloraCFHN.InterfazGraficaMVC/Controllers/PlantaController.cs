using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FloraCFHN.EntidadesDeNegocio;
using FloraCFHN.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace FloraCFHN.InterfazGraficaMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PlantaController : Controller
    {
        //Instancias de accesos a los metodos de las clases
        PlantaBL plantaBL = new PlantaBL();
        CategoriaBL categoriaBL = new CategoriaBL();

        // GET: Accion que muestra la pagina principal de Plantas
        public async Task<IActionResult> Index(Planta pPlanta = null)
        {
            if (pPlanta == null)
                pPlanta = new Planta();

            if (pPlanta.top_aux == 0)
                pPlanta.top_aux = 10;

            else if (pPlanta.top_aux == -1)
                pPlanta.top_aux = 0;

            var plantas = await plantaBL.BuscarIncluirCategoriaAsync(pPlanta);
            var categorias = await categoriaBL.ObtenerTodosAsync();

            ViewBag.Top = pPlanta.top_aux;
            ViewBag.Categorias = categorias;
            return View(plantas);
        }

        // GET: Accion que muestra el detalle de un registro existente
        public async Task<IActionResult> Details(int id)
        {
            var plantas = await plantaBL.ObtenerPorIdAsync(new Planta { Id = id });
            plantas.Categoria = await categoriaBL.ObtenerPorIdAsync(new Categoria { Id = plantas.CategoriaId });
            return View(plantas);
        }

        // GET: Accion que muestra el formulario para agregar un nueva planta
        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = await categoriaBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: Accion que recibe los datos del formulario para enviarlos a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Planta pPlanta)
        {
            try
            {
                int result = await plantaBL.CrearAsync(pPlanta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categorias = await categoriaBL.ObtenerTodosAsync();
                return View(pPlanta);
            }
        }

        // GET: Accion que muestra los datos cargados para editarlos
        public async Task<IActionResult> Edit(Planta pPlanta)
        {
            var planta = await plantaBL.ObtenerPorIdAsync(pPlanta);
            var categorias = await categoriaBL.ObtenerTodosAsync();

            ViewBag.Categorias = categorias;
            ViewBag.Error = "";
            return View(planta);
        }

        // POST: Accion que reCibe los datos modificados para enviarlos a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Planta pPlanta)
        {
            try
            {
                int result = await plantaBL.ModificarAsync(pPlanta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categorias = await categoriaBL.ObtenerTodosAsync();
                return View();
            }
        }

        // GET: Accion que muestra los datos del registro para confirmar la eliminacion
        public async Task<IActionResult> Delete(Planta pPlanta)
        {
            var planta = await plantaBL.ObtenerPorIdAsync(pPlanta);
            planta.Categoria = await categoriaBL.ObtenerPorIdAsync(new Categoria { Id = planta.CategoriaId });
            ViewBag.Error = "";
            return View(planta);
        }

        // POST: Accion que recibe la confirmacion para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Planta pPlanta)
        {
            try
            {
                int result = await plantaBL.EliminarAsync(pPlanta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var planta = await plantaBL.ObtenerPorIdAsync(pPlanta);
                if (planta == null)
                    planta = new Planta();

                if (planta.Id > 0)
                    planta.Categoria = await categoriaBL.ObtenerPorIdAsync(new Categoria { Id = planta.CategoriaId });
                return View(planta);
            }
        }
    }
}
