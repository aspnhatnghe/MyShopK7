using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class CartController : Controller
    {
        public readonly ICartService _cartService;
        public CartController(ICartService service)
        {
            _cartService = service;
        }
        public IActionResult Index()
        {
            return View(_cartService.GetCart());
        }

        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            _cartService.AddToCart(productId, quantity);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCartItem(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToAction("Index");
        }
    }
}