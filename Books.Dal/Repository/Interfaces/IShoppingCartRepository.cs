using Books.Models;

namespace Books.Dal.Repository.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int IncrementCount(ShoppingCart shoppingCart, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
