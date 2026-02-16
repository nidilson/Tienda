using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Models.data;
using Tienda.Services;

namespace Tienda.Controllers
{
	public class ProductoController : Controller
	{
		private ProductoService _productoService;
		public ProductoController(ProductoService prodService)
		{
			_productoService = prodService;
		}

		[HttpGet("Productos/all")]
		public ActionResult GetAllProducts()
		{
			List<Producto> products = new List<Producto>();
			try
			{
				products = _productoService.GetAll();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			return Ok(products);
		}
		[HttpGet("Productos/{id}")]
		public ActionResult GetProduct(string id)
		{
			int idProducto = 0;
			Producto product;
			try
			{
				idProducto = int.Parse(id);
				product = _productoService.GetById(idProducto);
			}
			catch (ArgumentNullException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (FormatException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (OverflowException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			return Ok(product);
		}
	}
}
