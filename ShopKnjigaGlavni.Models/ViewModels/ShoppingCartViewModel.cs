using ShopKnjigaGlavni.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopKnjigaGlavni.Models.ViewModels;

public class ShoppingCartViewModel
{
    public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
    public double TotalPrice { get; set; }
    public ShoppingCartViewModel(List<ShoppingCart> shoppingCarts)
    {
        double totalPrice = 0;
        foreach(ShoppingCart cart in shoppingCarts)
        {
            if(cart.Count > 100) {
              totalPrice += cart.Product.Price100 * cart.Count;
            }
            else if(cart.Count > 50)
            {
                totalPrice += cart.Product.Price50 * cart.Count;
            }
            else
            {
                totalPrice += cart.Product.Price * cart.Count;
            }
        }
        TotalPrice = totalPrice;
        ShoppingCarts = shoppingCarts;
    }
}
