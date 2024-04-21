using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopKnjigaGlavni.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(Product product)
    {
        var productInDb = _context.Products.FirstOrDefault(p => p.Id == product.Id);

        if (productInDb != null)
        {
            productInDb.Title = product.Title;
            productInDb.Author = product.Author;
            productInDb.Description = product.Description;
            productInDb.Price = product.Price;
            productInDb.ListPrice = product.ListPrice;
            productInDb.Price50 = product.Price50;
            productInDb.Price100 = product.Price100;
            productInDb.CategoryId = product.CategoryId;

            if (productInDb.ImageUrl != null)
            {
                productInDb.ImageUrl = product.ImageUrl;
            }

            // _context.Products.Update(productInDb);
        }
    }
}
