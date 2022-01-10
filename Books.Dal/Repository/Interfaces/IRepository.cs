using System.Linq.Expressions;

namespace Books.Dal.Repository.Interfaces
{
	public interface IRepository<T> where T : class
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="includeProperties"></param>
		/// <param name="tracked"></param>
		/// <returns></returns>
		T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="includeProperties"></param>
		/// <returns></returns>
		IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		void Add(T entity);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		void Remove(T entity);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		void RemoveRange(IEnumerable<T> entity);
	}
}
