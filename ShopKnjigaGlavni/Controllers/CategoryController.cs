using Microsoft.AspNetCore.Mvc;
using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.Models.Models;

namespace ShopKnjigaGlavni.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _context.Categories.ToList();
            return View(categoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            List<Category> categoryList = _context.Categories.ToList();

            foreach (var item in categoryList)
            {
                if (item.Name == category.Name)
                {
                    ModelState.AddModelError("Name", "Name vec postoji");
                }
                if(item.DisplayOrder == category.DisplayOrder)
                {
                    ModelState.AddModelError("DisplayOrder", "Display order vec postoji");
                }
            }

            if (ModelState.IsValid)
            {
               
                _context.Categories.Add(category);
                _context.SaveChanges();  //potrebno da je se spremi na bazu
                TempData["success"] = "Uspijeh";
                return RedirectToAction("Index", "Category");
            }
           
            return View();
        }

        public IActionResult Edit(int? categoryId)
        {
            if(categoryId == null && categoryId == 0)
            {
                return NotFound();
            }
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            if(category == null)
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

                _context.Categories.Update(category);
                _context.SaveChanges();  //potrebno da je se spremi na bazu
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
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? categoryId)
        {
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            if(category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index", "Category");
        }

    }
}
