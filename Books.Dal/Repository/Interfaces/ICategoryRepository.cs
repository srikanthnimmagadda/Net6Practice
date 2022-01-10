using Books.Models;

namespace Books.Dal.Repository.Interfaces
{
	public interface ICategoryRepository : IRepository<Category>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="category"></param>
		void Update(Category category);
	}
}
