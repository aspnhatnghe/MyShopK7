using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.ViewComponents
{
    public class YourOrder : ViewComponent
    {
        public readonly ICartService _cartService;
        public YourOrder(ICartService service)
        {
            _cartService = service;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cartService.GetCart());
        }
    }
}
