using e_commerce.Data.Cart;
using e_commerce.Data.Services;
using e_commerce.Data.ViewModels;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IMoviesService _moviesService;
        private readonly IOrdersService _ordersService;
        public OrdersController(ShoppingCart shoppingCart, IMoviesService moviesService, IOrdersService ordersService)
        {
            _shoppingCart = shoppingCart;
            _moviesService = moviesService;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "";
            var items = await _ordersService.GetOrdersByUserIdAsync(userId);
            return View(items);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }
        [HttpPost]
        public async  Task<IActionResult> AddItemToShoppingCart(int id)
        {

            var item = await _moviesService.GetMovieById(id);
            if (item != null)
            {
                _shoppingCart.AddShoppingCartItem(item);

            }
            return RedirectToAction(nameof(ShoppingCart));
            
        }
        public async Task<IActionResult> DeleteItemFromShoppingCart (int id)
        {
            var item = await _moviesService.GetMovieById(id);
            if (item != null)
            {
                _shoppingCart.RemoveShoppingCartItem(item);

            }
            return RedirectToAction(nameof(ShoppingCart));

        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";
            await _ordersService.StoreOrdersAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync(items);
            return View();
        }

    }
}
