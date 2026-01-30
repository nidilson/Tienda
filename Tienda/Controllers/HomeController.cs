using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda.Models;
using Tienda.Models.data;

namespace Tienda.Controllers
{
    public class HomeController : Controller
    {
        private TiendaContext _dbcontext;
		public HomeController(TiendaContext db)
		{
            _dbcontext = db;
		}
		public IActionResult Index()
        {
            ViewBag.Productos = _dbcontext.Productos.ToList();
            return View();
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
