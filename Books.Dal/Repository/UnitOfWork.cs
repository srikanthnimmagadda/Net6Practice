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
			Product = new ProductRepository(_dbContext);
			Company = new CompanyRepository(_dbContext);
			ApplicationUser = new ApplicationUserRepository(_dbContext);
			ShoppingCart = new ShoppingCartRepository(_dbContext);
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
        public IProductRepository Product { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public ICompanyRepository Company { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IShoppingCartRepository ShoppingCart { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IApplicationUserRepository ApplicationUser { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
