using Books.Models;

namespace Books.Dal.Repository.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderHeader"></param>
        void Update(OrderHeader orderHeader);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderStatus"></param>
        /// <param name="paymentStatus"></param>
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessionId"></param>
        /// <param name="paymentItentId"></param>
        void UpdateStripePaymentID(int id, string sessionId, string paymentItentId);
    }
}
