using Books.Models;

namespace Books.Dal.Repository.Interfaces
{
	public interface ICoverTypeRepository : IRepository<CoverType>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="coverType"></param>
		void Update(CoverType coverType);
	}
}
