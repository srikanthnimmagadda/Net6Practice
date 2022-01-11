using Books.Dal.Data;
using Books.Dal.Repository.Interfaces;
using Books.Models;
using BulkyBook.DataAccess.Repository;

namespace Books.Dal.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly BooksDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public ProductRepository(BooksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product)
        {
            var productDal = _dbContext.Products.FirstOrDefault(u => u.Id == product.Id);
            if (productDal != null)
            {
                productDal.Title = product.Title;
                productDal.ISBN = product.ISBN;
                productDal.Price = product.Price;
                productDal.Price50 = product.Price50;
                productDal.ListPrice = product.ListPrice;
                productDal.Price100 = product.Price100;
                productDal.Description = product.Description;
                productDal.CategoryId = product.CategoryId;
                productDal.Author = product.Author;
                productDal.CoverTypeId = product.CoverTypeId;
                if (product.ImageUrl != null)
                {
                    productDal.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
