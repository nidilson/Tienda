using Tienda.Models;
using Tienda.Models.data;

namespace Tienda.Services
{
	public class CarritoService: IDbService<Carrito>
	{
		private TiendaContext _context;

		public CarritoService(TiendaContext context)
		{
			_context = context;
		}

		public Carrito Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<Carrito> GetAll()
		{
			throw new NotImplementedException();
		}

		public Carrito GetById(int id)
		{
			Carrito carrito = new Carrito();
			try
			{
				carrito = _context.Carritos.Where(c => c.CarritoId == id).Select(c => new Carrito
				{
					CarritoId = c.CarritoId,
					FechaCreacion = c.FechaCreacion,
					Estado = c.Estado,
					CarritoDetalles = _context.CarritoDetalles.Where(cd => cd.CarritoId == c.CarritoId).Select(cd => new CarritoDetalle
					{
						CarritoDetalleId = cd.CarritoDetalleId,
						CarritoId = cd.CarritoId,
						ProductoId = cd.ProductoId,
						Cantidad = cd.Cantidad,
						PrecioUnitario = cd.PrecioUnitario,
						Producto = cd.Producto,
					}).ToList(),
				}).First();
			}catch(ArgumentNullException ex)
			{ 
				throw new ArgumentNullException("No se halló ningún carrito con el id provisto");
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return carrito;
		}

		public Carrito Insert(Carrito entity)
		{
			
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					_context.Carritos.Add(entity);
					_context.SaveChanges();
					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
					throw e;
				}
			}

			return entity;
		}

		public Carrito Update(Carrito entity)
		{
			throw new NotImplementedException();
		}
	}
}
