namespace Tienda.Services
{
	public interface IDbService<T> where T : class
	{
		public List<T> GetAll();
		public T GetById(int id);
		public T Insert(T entity);
		public T Update(T entity);
		public T Delete(int id);

	}
}
