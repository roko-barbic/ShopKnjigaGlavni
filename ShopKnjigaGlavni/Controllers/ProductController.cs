using Microsoft.AspNetCore.Mvc;
using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;

namespace ShopKnjigaGlavni.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            List<Product> productList = _unitOfWork.Product.GetAll().ToList();

            foreach (var item in productList)
            {
                if (item.Title == product.Title)
                {
                    ModelState.AddModelError("Title", "Title vec postoji");
                }
                if (item.Author == product.Author)
                {
                    ModelState.AddModelError("DisplayOrder", "Display order vec postoji");
                }
            }

            if (ModelState.IsValid)
            {

                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();  //potrebno da je se spremi na bazu
                TempData["success"] = "Uspijeh";
                return RedirectToAction("Index", "Product");
            }

            return View();
        }


        public IActionResult Edit(int? productId)
        {
            if (productId == null  || productId == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(c => c.Id == productId);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();  //potrebno da je se spremi na bazu
                TempData["success"] = "Product edited successfully";
                return RedirectToAction("Index", "Product");
            }

            return View();
        }
        public IActionResult Delete(int? productId)
        {
            if (productId == null && productId == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(c => c.Id == productId);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? productId)
        {
            Product? product = _unitOfWork.Product.Get(c => c.Id == productId);

            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Delete(product);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index", "Product");
        }
    }
}
