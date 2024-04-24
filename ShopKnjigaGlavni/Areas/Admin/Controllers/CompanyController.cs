using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;
using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;
using ShopKnjigaGlavni.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ShopKnjigaGlavni.Utility;

namespace ShopKnjigaGlavni.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = Role.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        List<Company> companyList = _unitOfWork.Company.GetAll().ToList();
        return View(companyList);
    }

    public IActionResult Upsert(int? id)
    {
        Company? company = new Company();

        if (id == null || id == 0)
        {
            // Create
            return View(company);
        }
        else
        {
            // Update
            company = _unitOfWork.Company.Get(p => p.Id == id);
            return View(company);
        }
    }

    [HttpPost]
    public IActionResult Upsert(Company company)
    {
        if (ModelState.IsValid)
        {
            if (company.Id == 0)
            {
                _unitOfWork.Company.Add(company);
            }
            else
            {
                _unitOfWork.Company.Update(company);
            }

            _unitOfWork.Save();
            return RedirectToAction("Index", "Company");
        }

        return View(company);
    }

    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> companyList = _unitOfWork.Company.GetAll().ToList();
        return Json(new { data = companyList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var company = _unitOfWork.Company.Get(p => p.Id == id);

        if (company == null)
        {
            return Json(new { success = false, message = "Errow while deleting" });
        }

        _unitOfWork.Company.Delete(company);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
}