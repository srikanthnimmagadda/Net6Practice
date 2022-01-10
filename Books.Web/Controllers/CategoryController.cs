using Books.Web.Data;
using Books.Web.Models;
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
            List<Category>? categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(model);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Categories.Find(id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(model);
                _dbContext.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            var model = _dbContext.Categories.Find(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var model = _dbContext.Categories.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(model);
            _dbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
