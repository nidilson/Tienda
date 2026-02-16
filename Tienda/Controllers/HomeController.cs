using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Tienda.Models;
using Tienda.Models.data;
using Tienda.Services;

namespace Tienda.Controllers
{
    public class HomeController : Controller
    {
        private IDbService<Producto> _productoService;
		public HomeController(IDbService<Producto> prodService)
		{
            _productoService = prodService;
		}
		public IActionResult Index()
        {
            ViewBag.Productos = _productoService.GetAll();
            
			return View(new Carrito());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
