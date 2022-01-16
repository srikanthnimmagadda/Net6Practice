using Books.Dal.Data;
using Books.Dal.Repository.Interfaces;
using Books.Models;

namespace Books.Dal.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly BooksDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public OrderDetailRepository(BooksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetail"></param>
        public void Update(OrderDetail orderDetail)
        {
            _dbContext.OrderDetail.Update(orderDetail);
        }
    }
}
