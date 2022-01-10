using Books.Dal.Data;
using Books.Dal.Repository.Interfaces;

namespace Books.Dal.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly BooksDbContext _dbContext;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dbContext"></param>
		public UnitOfWork(BooksDbContext dbContext)
		{
			_dbContext = dbContext;
			Category = new CategoryRepository(_dbContext);
			CoverType = new CoverTypeRepository(_dbContext);
		}

		/// <summary>
		/// 
		/// </summary>
		public ICategoryRepository Category { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public ICoverTypeRepository CoverType { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
