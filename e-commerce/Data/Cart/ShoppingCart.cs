using e_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };

        }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public void AddShoppingCartItem(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.shoppingCartId == ShoppingCartId && n.Movie.Id == movie.Id);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    shoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
                _context.SaveChanges();
            }
            else
            {
                shoppingCartItem.Amount++;
                _context.SaveChanges();

            }

        }
         public void RemoveShoppingCartItem(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.shoppingCartId == ShoppingCartId && n.Movie.Id == movie.Id);
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
            }
            else
            {
                _context.Remove(shoppingCartItem);

            }
            _context.SaveChanges();
        }



        //services that belongs to the class only 
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.shoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }
        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(n => n.shoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync(List<ShoppingCartItem> items)
        {
            _context.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
