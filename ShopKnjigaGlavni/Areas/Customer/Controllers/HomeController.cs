using Microsoft.AspNetCore.Mvc;
using ShopKnjigaGlavni.Models.Models;
//using WebShopBooks.DataAccess.Repository.IRepository;
using System.Diagnostics;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;

namespace ShopKnjigaGlavni.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return View(productList);
    }

    public IActionResult Details(int productId)
    {
        ShoppingCart shoppingCart = new()
        {
            Product = _unitOfWork.Product.Get(p => p.Id == productId, includeProperties: "Category"),
            ProductId = productId,
            Count = 1
        };
        return View(shoppingCart);
    }
    [HttpPost]
    [Authorize]
    public IActionResult AddToCart(ShoppingCart cart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCart newShoppingCart = new ShoppingCart();

        newShoppingCart.ProductId = cart.ProductId;
        newShoppingCart.Product = _unitOfWork.Product.Get(i => i.Id == cart.ProductId);
        newShoppingCart.Count = cart.Count;
        newShoppingCart.ApplicationUserId = userId;
        newShoppingCart.ApplicationUser = _unitOfWork.ApplicationUser.Get(j => j.Id == userId);

        if(_unitOfWork.ShoppingCart.Get(p => p.Product.Id == cart.ProductId, includeProperties: "Product") != null)
        {
            _unitOfWork.ShoppingCart.Update(newShoppingCart);
        }
        else
        {
            _unitOfWork.ShoppingCart.Add(newShoppingCart);
        }
        _unitOfWork.Save();
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
