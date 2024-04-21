﻿using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;
using ShopKnjigaGlavni.Models.ViewModels;

namespace ShopKnjigaGlavni.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IWebHostEnvironment _webHostEnvironment;   
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;

    }
    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return View(products);
    }

    public IActionResult Upsert(int? id)
    {
        IEnumerable<SelectListItem> categoryList= _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString(),
        });

        //ViewBag.CategoryList = categoryList; ovo je good
        //ViewData["CategoryList"] = categoryList; jebes ovo ovo je lakse

        ProductViewModel productViewModel = new ProductViewModel
        {
            CategoryList = categoryList,
            Product = new Product()
        };

        if(id == 0 || id == null)
        {
            return View(productViewModel);
        }
        else
        {
            productViewModel.Product = _unitOfWork.Product.Get(p => p.Id == id);
            return View(productViewModel);

        }
    }

    [HttpPost]
    public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
    {
        List<Product> productList = _unitOfWork.Product.GetAll().ToList();

        foreach (var item in productList)
        {
            if (item.Title == productViewModel.Product.Title)
            {
                ModelState.AddModelError("Title", "Title vec postoji");
            }
            if (item.Author == productViewModel.Product.Author)
            {
                ModelState.AddModelError("DisplayOrder", "Display order vec postoji");
            }
        }

        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if(file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); ;//samo nam daje neko ime da ne moramo izmisaljat
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if(string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);//upitno
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                productViewModel.Product.ImageUrl = @"\images\product\" + fileName;
            }
            if (productViewModel.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productViewModel.Product);

            }
            else
            {
                _unitOfWork.Product.Update(productViewModel.Product);

            }

            _unitOfWork.Save();  //potrebno da je se spremi na bazu
           // TempData["success"] = "Uspijeh";
            return RedirectToAction("Index", "Product");
        }
        else
        {
           
            productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });
            
            return View(productViewModel);
        }

    }

    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = productList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var product = _unitOfWork.Product.Get(p => p.Id == id);

        if (product == null)
        {
            return Json(new { success = false, message = "Errow while deleting" });
        }

        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.Trim('\\'));

        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _unitOfWork.Product.Delete(product);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete successful" });
    }

    #endregion
}
