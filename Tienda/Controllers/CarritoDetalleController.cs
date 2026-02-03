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
				if (ContieneArticulo(carrito, detalle, out detalle))
				{
					detalle = _detalleService.Update(detalle);
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

			DetalleCarritoOutDTO detalleADevolver = new DetalleCarritoOutDTO
			{
				Id = detalle.CarritoDetalleId,
				Producto = detalle.Producto.Nombre,
				Cantidad = detalle.Cantidad,
				Precio = detalle.PrecioUnitario
			};
			return Ok(detalleADevolver);
		}

		private bool ContieneArticulo(Carrito carrito, CarritoDetalle detalle, out CarritoDetalle detalleOut)
		{
			CarritoDetalle? existe = _detalleService.GetAll().Where(det => det.CarritoId == carrito.CarritoId && det.ProductoId == detalle.ProductoId).FirstOrDefault();
			if (existe == null)
			{
				detalleOut = detalle;
				return false;
			}
			else
			{
				existe.Cantidad += detalle.Cantidad;
				detalleOut = existe;
				return true;
			}
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
