using Books.Models;

namespace Books.Dal.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        void Update(Product product);
    }
}
