using Books.Dal.Repository.Interfaces;
using Books.Models;
using Books.Models.ViewModels;
using Books.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.RoleAdmin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="page"></param>
       /// <returns></returns>
        public async Task<IActionResult> Index(int page = 1)
        {
            //IEnumerable<Category>? categories = _unitOfWork.Category.GetAll();
            CategoryViewModel model = new CategoryViewModel
            {
                Categories = await _unitOfWork.Category.GetAllAsync()
            };

            var count = model.Categories.Count();
            model.Categories = model.Categories.OrderBy(p => p.Name)
                .Skip((page - 1) * 2).Take(2).ToList();

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 2,
                TotalItem = count,
                UrlParam = "/Admin/Category/Index?page=:"
            };

            return View(model);
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
                _unitOfWork.Category.Add(model);
                _unitOfWork.Save();
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
            var model = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);

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
                _unitOfWork.Category.Update(model);
                _unitOfWork.Save();
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
            var model = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);

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
            var model = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(model);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
