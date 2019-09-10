using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.ViewComponents
{
    public class Cart : ViewComponent
    {
        public ICartService _cartService;
        public Cart(ICartService service)
        {
            _cartService = service;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cartService.GetCart());
        }
    }
}
