using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopKnjigaGlavni.DataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private ApplicationDbContext _context;

    public ShoppingCartRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(ShoppingCart shoppingCart)
    {
        _context.Update(shoppingCart);
    }
}
