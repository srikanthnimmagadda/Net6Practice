using Books.Models;

namespace Books.Dal.Repository.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetail"></param>
        void Update(OrderDetail orderDetail);
    }
}
