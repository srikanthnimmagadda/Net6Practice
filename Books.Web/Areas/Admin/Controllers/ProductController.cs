using Books.Dal.Repository.Interfaces;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SysIo = System.IO;

namespace Books.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="hostEnvironment"></param>
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult Upsert(int? id)
    {
        ProductViewModel model = new()
        {
            Product = new(),
            Categories = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypes = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };

        if (!id.HasValue)
        {
            return View(model);
        }
        else
        {
            model.Product = _unitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
            return View(model);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductViewModel model, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);

                if (model.Product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, model.Product.ImageUrl.TrimStart('\\'));
                    if (SysIo.File.Exists(oldImagePath))
                    {
                        SysIo.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                model.Product.ImageUrl = @"\images\products\" + fileName + extension;

            }

            if (model.Product.Id == 0)
            {
                _unitOfWork.Product.Add(model.Product);
            }
            else
            {
                _unitOfWork.Product.Update(model.Product);
            }
            _unitOfWork.Save();

            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        return View(model);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
        return Json(new { data = products });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
        if (product == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
        if (SysIo.File.Exists(oldImagePath))
        {
            SysIo.File.Delete(oldImagePath);
        }

        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
    }
}
