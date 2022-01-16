using Books.Dal.Data;
using Books.Dal.Repository.Interfaces;
using Books.Models;

namespace Books.Dal.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly BooksDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public OrderHeaderRepository(BooksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderHeader"></param>
        public void Update(OrderHeader orderHeader)
        {
            _dbContext.OrderHeaders.Update(orderHeader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderStatus"></param>
        /// <param name="paymentStatus"></param>
        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderHeader = _dbContext.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderHeader != null)
            {
                orderHeader.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderHeader.PaymentStatus = paymentStatus;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessionId"></param>
        /// <param name="paymentItentId"></param>
        public void UpdateStripePaymentID(int id, string sessionId, string paymentItentId)
        {
            var orderHeader = _dbContext.OrderHeaders.FirstOrDefault(u => u.Id == id);
            orderHeader.PaymentDate = DateTime.Now;
            orderHeader.SessionId = sessionId;
            orderHeader.PaymentIntentId = paymentItentId;
        }
    }
}
