using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UI.Common;
using UI.Data;
using UI.Helpers;
using UI.Models;

namespace UI.Controllers
{
    public class CartController : Controller
    {
        public readonly ICartService _cartService;
        public readonly MyDbContext _context;
        public CartController(ICartService service, MyDbContext context)
        {
            _cartService = service; _context = context;
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

        public IActionResult ApplyCoupon(string CouponCode)
        {
            var coupon = _context.Coupons.SingleOrDefault(p => p.CouponCode == CouponCode);
            if (coupon == null)
            {
                ViewBag.CouponMessage = "Mã không tồn tại";
            }
            else if (coupon.Status == CouponStatus.Used)
            {
                ViewBag.CouponMessage = "Mã này đã dùng";
            }
            else if (coupon.ExpireDate < DateTime.Now)
            {
                ViewBag.CouponMessage = "Mã đã hết hạn";
                coupon.Status = CouponStatus.Expired;
                _context.SaveChanges();
            }
            else
            {
                //hợp lệ
                HttpContext.Session.Set("Coupon", coupon);
            }

            return View("Index", _cartService.GetCart());
        }
    }
}