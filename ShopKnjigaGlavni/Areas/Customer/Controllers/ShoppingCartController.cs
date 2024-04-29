using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;
using ShopKnjigaGlavni.Utility;


namespace ShopKnjigaGlavni.Areas.Customer.Controllers;
[Area("Customer")]
[Authorize(Roles = Role.Role_Customer)]
public class ShoppingCartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
   
    public ShoppingCartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<ShoppingCart> shoppingList = _unitOfWork.ShoppingCart.GetAll(includeProperties: "Product").ToList();
        return View(shoppingList);
    }
   
    [HttpPost]
    public IActionResult AddToCart(Product product)
    {
        List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();

        
        return View();
    }

    public IActionResult Edit(int? shoppingCartId)
    {
        if (shoppingCartId == null && shoppingCartId == 0)
        {
            return NotFound();
        }
        ShoppingCart? shoppingCart = _unitOfWork.ShoppingCart.Get(c => c.Id == shoppingCartId);

        if (shoppingCart == null)
        {
            return NotFound();
        }
        return View(shoppingCart);
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
    public IActionResult DeletePOST(int? shoppingCartId)
    {
        ShoppingCart? shoppingCart = _unitOfWork.ShoppingCart.Get(c => c.Id == shoppingCartId);

        if (shoppingCart == null)
        {
            return NotFound();
        }
        _unitOfWork.ShoppingCart.Delete(shoppingCart);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index", "Category");
    }
}
