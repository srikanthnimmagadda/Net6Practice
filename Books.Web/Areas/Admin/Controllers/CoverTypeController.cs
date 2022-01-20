using Books.Dal.Repository.Interfaces;
using Books.Models;
using Books.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.RoleAdmin)]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitOfWork"></param>
    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        IEnumerable<CoverType> coverTypes = _unitOfWork.CoverType.GetAll();
        return View(coverTypes);
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
    public IActionResult Create(CoverType model)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(model);
            _unitOfWork.Save();
            TempData["success"] = "CoverType created successfully";
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
        CoverType? model = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        return model == null ? NotFound() : View(model);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType model)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(model);
            _unitOfWork.Save();
            TempData["success"] = "CoverType updated successfully";
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
        CoverType? model = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        return model == null ? NotFound() : View(model);
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
        var model = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        if (model == null)
        {
            return NotFound();
        }

        _unitOfWork.CoverType.Remove(model);
        _unitOfWork.Save();
        TempData["success"] = "CoverType deleted successfully";
        return RedirectToAction("Index");
    }
}
