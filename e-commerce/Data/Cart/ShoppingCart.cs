using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }
        public string shoppingCartId { get; set; }

        public List<ShoppingCartItem> shoppingCartItems { get; set; }

        //services that belongs to the class only 
       /* public List<ShoppingCartItem> GetShoppingCartItems ()
        {
          //  return shoppingCartItems ?? (shoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == shoppingCartId).Include(n => n.Movie).ToList());
        }
       */

    }
}
