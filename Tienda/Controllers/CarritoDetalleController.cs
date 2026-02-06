using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Tienda.Models.data;
using Tienda.Models.DTO;
using Tienda.Services;

namespace Tienda.Controllers
{
	public class CarritoDetalleController : Controller
	{
		private IDbService<CarritoDetalle> _detalleService;
		private IDbService<Carrito> _carritoService;
		private IDbService<Producto> _productoService;

		public CarritoDetalleController(IDbService<CarritoDetalle> detalleService, IDbService<Carrito> carritoService, IDbService<Producto> productoService)
		{
			_detalleService = detalleService;
			_carritoService = carritoService;
			_productoService = productoService;
		}

		[HttpGet("DetalleCarrito")]
		public IActionResult ObtenerDetalle()
		{
			string? carritoJson = HttpContext.Session.GetString("carrito");
			if (carritoJson == null)
			{
				return BadRequest(new { message = "No hay productos a mostrar" });
			}
			Carrito carrito = JsonSerializer.Deserialize<Carrito>(carritoJson);

			try
			{
				carrito = _carritoService.GetById(carrito.CarritoId);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
			return PartialView("Views/PartialViews/_CarritoPartial.cshtml", carrito.CarritoDetalles);
		}

		[HttpPost("DetalleCarrito")]
		public IActionResult AnadirDetalle([FromBody] DetalleCarritoInDTO dto)
		{
			string? carritoJson = HttpContext.Session.GetString("carrito");
			if (carritoJson == null)
			{
				Carrito nuevoCarrito = CrearCarrito();
				HttpContext.Session.SetString("carrito", JsonSerializer.Serialize<Carrito>(nuevoCarrito));
				carritoJson = HttpContext.Session.GetString("carrito");
			}
			Carrito carrito = JsonSerializer.Deserialize<Carrito>(carritoJson);
			Producto producto = _productoService.GetById(dto.ProductoId);

			CarritoDetalle detalle = new CarritoDetalle
			{
				CarritoId = carrito.CarritoId,
				ProductoId = producto.ProductoId,
				Cantidad = dto.Cantidad,
				PrecioUnitario = producto.Precio
			};
			
			try
			{
				CarritoDetalle? detalleCarrito = ContieneArticulo(carrito, detalle);
				if (detalleCarrito != null)
				{
					detalleCarrito.Cantidad += detalle.Cantidad;
					detalle = _detalleService.Update(detalleCarrito);
				}
				else
				{
					detalle = _detalleService.Insert(detalle);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			return Created();
		}

		[HttpPatch("DetalleCarrito")]
		public IActionResult ModificarCantidad([FromBody] DetalleCarritoInDTO dto)
		{
			string? carritoJson = HttpContext.Session.GetString("carrito");
			if (carritoJson == null)
			{
				return NotFound(new { message = "No existe un carrito a actualizar" });
			}
			Carrito carrito = JsonSerializer.Deserialize<Carrito>(carritoJson);
			Producto producto = _productoService.GetById(dto.ProductoId);

			CarritoDetalle detalle = new CarritoDetalle
			{
				CarritoId = carrito.CarritoId,
				ProductoId = producto.ProductoId,
				Cantidad = dto.Cantidad,
				PrecioUnitario = producto.Precio
			};

			try
			{
				CarritoDetalle? detalleCarrito = ContieneArticulo(carrito, detalle);
				if (detalleCarrito == null)
				{
					throw new Exception("No se tiene el elemento en el carrito");
				}
				detalleCarrito.Cantidad = detalle.Cantidad;
				detalle = _detalleService.Update(detalleCarrito);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			return Ok();
		}
		[HttpDelete("DetalleCarrito")]
		public IActionResult EliminarDetalleCarrito([FromBody] DetalleCarritoInDTO dto)
		{
			string? carritoJson = HttpContext.Session.GetString("carrito");
			if (carritoJson == null)
			{
				return NotFound(new { message = "No existe un carrito a actualizar" });
			}			

			try
			{
				CarritoDetalle detalle = _detalleService.GetById(dto.DetalleId);
				if (detalle == null)
				{
					throw new Exception("No se tiene el elemento en el carrito");
				}
				
				detalle = _detalleService.Delete(dto.DetalleId);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			return Ok();
		}
		private CarritoDetalle ContieneArticulo(Carrito carrito, CarritoDetalle detalle)
		{
			CarritoDetalle? existe = _detalleService.GetAll().Where(det => det.CarritoId == carrito.CarritoId && det.ProductoId == detalle.ProductoId).FirstOrDefault();
			return existe;
		}

		public Carrito CrearCarrito()
		{
			Carrito carrito = new Carrito
			{
				FechaCreacion = DateTime.Now,
				Estado = "Pendiente"
			};

			try
			{
				carrito = _carritoService.Insert(carrito);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return carrito;
		}

	}
}
