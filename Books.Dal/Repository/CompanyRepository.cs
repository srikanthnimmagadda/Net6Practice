using Books.Dal.Data;
using Books.Dal.Repository.Interfaces;
using Books.Models;

namespace Books.Dal.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly BooksDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public CompanyRepository(BooksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        public void Update(Company company)
        {
            _dbContext.Companies.Update(company);
        }
    }
}
