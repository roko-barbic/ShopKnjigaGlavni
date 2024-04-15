using Microsoft.AspNetCore.Mvc;
using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;

namespace ShopKnjigaGlavni.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    //private readonly ApplicationDbContext _context;
    //private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    //public CategoryController(ICategoryRepository categoryRepository, ApplicationDbContext context)
    //{
    //  _context = context; 
    //_categoryRepository = categoryRepository; 
    //}
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
        return View(categoryList);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();

        foreach (var item in categoryList)
        {
            if (item.Name == category.Name)
            {
                ModelState.AddModelError("Name", "Name vec postoji");
            }
            if (item.DisplayOrder == category.DisplayOrder)
            {
                ModelState.AddModelError("DisplayOrder", "Display order vec postoji");
            }
        }

        if (ModelState.IsValid)
        {

            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();  //potrebno da je se spremi na bazu
            TempData["success"] = "Uspijeh";
            return RedirectToAction("Index", "Category");
        }

        return View();
    }

    public IActionResult Edit(int? categoryId)
    {
        if (categoryId == null && categoryId == 0)
        {
            return NotFound();
        }
        Category? category = _unitOfWork.Category.Get(c => c.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {

        if (ModelState.IsValid)
        {

            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();  //potrebno da je se spremi na bazu
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("Index", "Category");
        }

        return View();
    }
    public IActionResult Delete(int? categoryId)
    {
        if (categoryId == null && categoryId == 0)
        {
            return NotFound();
        }
        Category? category = _unitOfWork.Category.Get(c => c.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? categoryId)
    {
        Category? category = _unitOfWork.Category.Get(c => c.Id == categoryId);

        if (category == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Delete(category);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index", "Category");
    }

}
