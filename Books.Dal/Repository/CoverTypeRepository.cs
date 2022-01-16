using Books.Dal.Data;
using Books.Dal.Repository.Interfaces;
using Books.Models;

namespace Books.Dal.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly BooksDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public CoverTypeRepository(BooksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coverType"></param>
        public void Update(CoverType coverType)
        {
            _dbContext.CoverTypes.Update(coverType);
        }
    }
}
