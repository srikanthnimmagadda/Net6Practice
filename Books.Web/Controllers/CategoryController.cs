using Books.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BooksDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public CategoryController(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<Models.Category>? categories = _dbContext.Categories.ToList();
            return View(categories);
        }
    }
}
