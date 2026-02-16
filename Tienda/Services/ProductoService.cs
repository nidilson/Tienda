
using Microsoft.EntityFrameworkCore;
using System;
using Tienda.Models;
using Tienda.Models.data;

namespace Tienda.Services
{
	public class ProductoService : IDbService<Producto>
	{
		private TiendaContext _context;
		public ProductoService(TiendaContext context)
		{
			_context = context;
		}

		public Producto Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<Producto> GetAll()
		{
			List<Producto> products = new List<Producto>();
			try
			{
				products = _context.Productos.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return products;
		}

		public Producto GetById(int id)
		{
			Producto product;
			try
			{
				product = _context.Productos.Where(prod => prod.ProductoId == id).First();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return product;
		}

		public Producto Insert(Producto entity)
		{
			throw new NotImplementedException();
		}

		public Producto Update(Producto entity)
		{
			throw new NotImplementedException();
		}
	}
}
