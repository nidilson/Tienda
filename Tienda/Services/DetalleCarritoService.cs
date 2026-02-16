using Microsoft.EntityFrameworkCore;
using Tienda.Models;
using Tienda.Models.data;

namespace Tienda.Services
{
	public class DetalleCarritoService: IDbService<CarritoDetalle>
	{
		private TiendaContext _context;

		public DetalleCarritoService(TiendaContext context)
		{
			_context = context;
		}

		public CarritoDetalle Delete(int id)
		{
			CarritoDetalle detalle = this.GetById(id);
			using(var transaccion = _context.Database.BeginTransaction())
			{
				try
				{
					_context.CarritoDetalles.Remove(detalle);
					_context.SaveChanges();
					transaccion.Commit();

				}
				catch (Exception ex)
				{
					transaccion.Rollback();
					throw ex;
				}
			}
			return detalle;
		}

		public List<CarritoDetalle> GetAll()
		{
			List<CarritoDetalle> carritoDetalles = new List<CarritoDetalle>();
			try
			{
				carritoDetalles = _context.CarritoDetalles.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return carritoDetalles;
		}

		public CarritoDetalle GetById(int id)
		{
			CarritoDetalle detalle = new CarritoDetalle();

			try
			{
				detalle = _context.CarritoDetalles.Where(det => det.CarritoDetalleId == id).Select(det => new CarritoDetalle
				{
					CarritoDetalleId = det.CarritoDetalleId,
					CarritoId = det.CarritoId,
					ProductoId = det.ProductoId,
					Cantidad = det.Cantidad,
					PrecioUnitario = det.PrecioUnitario,
					Carrito = det.Carrito,
					Producto = det.Producto
				}).First();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return detalle;
		}

		public CarritoDetalle Insert(CarritoDetalle entity)
		{
			using (var transaccion = _context.Database.BeginTransaction())
			{
				try
				{
					_context.CarritoDetalles.Add(entity);
					_context.SaveChanges();
					transaccion.Commit();
				}
				catch (Exception ex)
				{
					transaccion.Rollback();
					throw ex;
				}
			}
			return entity;
		}

		public CarritoDetalle Update(CarritoDetalle entity)
		{
			using (var transaccion = _context.Database.BeginTransaction())
			{
				try
				{
					CarritoDetalle registroOriginal = _context.CarritoDetalles.Find(entity.CarritoDetalleId);

					if (registroOriginal == null)
						throw new Exception("Producto no existe");

					registroOriginal.CarritoId = entity.CarritoId;
					registroOriginal.ProductoId = entity.ProductoId;
					registroOriginal.Cantidad = entity.Cantidad;
					registroOriginal.PrecioUnitario = entity.PrecioUnitario;

					_context.SaveChanges();
					transaccion.Commit();
				}
				catch (Exception ex)
				{
					transaccion.Rollback();
					throw ex;
				}
			}
			return entity;
		}
	}
}
