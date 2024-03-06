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
    public class RolController : Controller
    {
        //Instancia de acceso a los metodos de la clase RolBL
        RolBL rolBL = new RolBL();

        // GET: Accion que muestra la pagina principal para roles
        public async Task<IActionResult> Index(Rol pRol = null)
        {
            if (pRol == null)
                pRol = new Rol();

            if (pRol.top_aux == 0)
                pRol.top_aux = 10;

            else if (pRol.top_aux == -1)
                pRol.top_aux = 0;

            var roles = await rolBL.BuscarAsync(pRol);
            ViewBag.Top = pRol.top_aux;

            return View(roles);
        }

        // GET: Accion que muestra el detalle de un registro existente
        public async Task<ActionResult> Details(int id)
        {
            var rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = id });
            return View(rol);
        }

        // GET: Accion que muestra el formulario para agregar un nuevo rol
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: Accion que recibe los datos del formulario y los envia hacia la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Rol pRol)
        {
            try
            {
                int result = await rolBL.CrearAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Accion que muestra el formulario con los datos cargados para edutarlos
        public async Task<ActionResult> Edit(Rol pRol)
        {
            var rol = await rolBL.ObtenerPorIdAsync(pRol);
            ViewBag.Error = "";
            return View(rol); 
        }

        // POST: Accion que recibe los datos modificados y los envia a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol pRol)
        {
            try
            {
                int result = await rolBL.ModificarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

        // GET: Accion que muestra los datos del registro para confirmar la eliminacion
        public async Task<IActionResult> Delete(Rol pRol)
        {
            var rol = await rolBL.ObtenerPorIdAsync(pRol);
            ViewBag.Error = "";
            return View(rol) ;
        }

        // POST: Accion que recibe la confirmacion para eliminar el regisro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Rol pRol)
        {
            try
            {
                int result = await rolBL.EliminarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }
    }
}
