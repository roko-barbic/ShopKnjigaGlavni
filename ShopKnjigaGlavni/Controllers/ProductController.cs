using Microsoft.AspNetCore.Mvc;
using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.Models.Models;

namespace ShopKnjigaGlavni.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
                _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        
        public IActionResult Create()
        {
            List<Category> categories = _context.Categories.ToList();
            
            return View(categories);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            List<Product> categoryList = _context.Products.ToList();

            foreach (var item in categoryList)
            {
                if (item.Name == product.Name)
                {
                    ModelState.AddModelError("Name", "Name vec postoji");
                }
                if (item.Author == product.Author)
                {
                    ModelState.AddModelError("DisplayOrder", "Display order vec postoji");
                }
            }

            if (ModelState.IsValid)
            {

                _context.Products.Add(product);
                _context.SaveChanges();  //potrebno da je se spremi na bazu
                TempData["success"] = "Uspijeh";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }
    }
}
