using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Text.Json;
using Tienda.Models;
using Tienda.Models.data;
using Tienda.Services;

namespace Tienda.Controllers
{
	public class CarritosController : Controller
	{
		private IDbService<Carrito> _carritoService;
		public CarritosController(IDbService<Carrito> carritoService)
		{
			_carritoService = carritoService;
		}

		[HttpGet("Carritos")]
		public IActionResult GetCarrito()
		{
			string carritoSesion = HttpContext.Session.GetString("carrito");
			Carrito carritoAObtener;
			try
			{
				if(carritoSesion == null)
				{
					throw new ArgumentNullException("No existe un carrito a mostrar");
				}
				carritoAObtener = JsonSerializer.Deserialize<Carrito>(carritoSesion);
			}
			catch (ArgumentNullException ex)
			{
				return BadRequest(new { message = ex.Message });
			}

			Carrito carrito = new Carrito();
			try
			{
				carrito = _carritoService.GetById(carritoAObtener.CarritoId);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			return PartialView("../PartialViews/_CarritoPartial", carrito);
		}

		[HttpPost("Carritos")]
		public IActionResult AgregarCarrito()
		{
			Carrito carrito = new Carrito
			{
				FechaCreacion = DateTime.Now,
				Estado = "Pendiente"
			};

			try
			{
				carrito = _carritoService.Insert(carrito);
			}catch(Exception ex)
			{
				return BadRequest(new {message = ex.Message});
			}

			HttpContext.Session.SetString("carrito", JsonSerializer.Serialize<Carrito>(carrito));

			return Ok(carrito);
		}
	}
}
