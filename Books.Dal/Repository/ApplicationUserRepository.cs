using Books.Dal.Data;
using Books.Dal.Repository.Interfaces;
using Books.Models;
using BulkyBook.DataAccess.Repository;

namespace Books.Dal.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly BooksDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public ApplicationUserRepository(BooksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
