using Books.Dal.Data;
using Books.Dal.Repository.Interfaces;
using Books.Models;

namespace Books.Dal.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly BooksDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public CategoryRepository(BooksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="category"></param>
		public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }
    }
}
