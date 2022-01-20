using Books.Dal.Repository.Interfaces;
using Books.Models;
using Books.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.RoleAdmin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitOfWork"></param>
    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        Company company = new();

        if (!id.HasValue)
        {
            return View(company);
        }
        else
        {
            company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            return View(company);
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
    public IActionResult Upsert(Company model)
    {
        if (ModelState.IsValid)
        {
            if (model.Id == 0)
            {
                _unitOfWork.Company.Add(model);
                TempData["success"] = "Company created successfully";
            }
            else
            {
                _unitOfWork.Company.Update(model);
                TempData["success"] = "Company updated successfully";
            }
            _unitOfWork.Save();

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
        IEnumerable<Company>? companies = _unitOfWork.Company.GetAll();
        return Json(new { data = companies });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        Company? company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
        if (company == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        _unitOfWork.Company.Remove(company);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
    }
}
