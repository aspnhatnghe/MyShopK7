using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UI.Common;
using UI.Data;
using UI.Helpers;
using UI.Models;
using UI.ViewModels;

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

        public IActionResult Checkout()
        {
            if (_cartService.GetCart().Count > 0)
            {
                return View();
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            var message = string.Empty;
            if (ModelState.IsValid)
            {
                var khachHang = HttpContext.Session.Get<Customer>("KhachHang");

                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //nếu chưa có khách hàng thì tạo
                        if (khachHang == null)
                        {
                            khachHang = new Customer
                            {
                                FullName = model.FullName,
                                Address = model.Address,
                                Phone = model.Phone,
                                Email = model.Email,
                                Username = Regex.Replace(model.FullName.ToUrlFriendly(), @"-", "") + DateTime.Now.Ticks.ToString(),
                                Password = "CreateNew"
                            };
                            _context.Add(khachHang);
                            _context.SaveChanges();
                        }

                        var order = new Order
                        {
                            CustomerId = khachHang.CustomerId,
                            OrderedDate = DateTime.Now,
                            OrderStatus = OrderStatus.Open,
                            PaymentMethod = PaymentMethod.COD,
                            Price = _cartService.GetCart().Sum(p => p.ThanhTien)
                        };

                        var coupon = HttpContext.Session.Get<Coupon>("Coupon");
                        if (coupon != null)
                        {
                            order.CouponCode = coupon.CouponCode;
                            var disPer = coupon.Discount ?? 0 * order.Price;
                            var vourche = coupon.Voucher ?? 0;
                            order.Discount = Math.Max(disPer, vourche);
                        }

                        _context.Add(order);
                        _context.SaveChanges();

                        foreach(var item in _cartService.GetCart())
                        {
                            var orderDetail = new OrderDetail
                            {
                                OrderId = order.Id,
                                ProductId = item.MaHh,
                                UnitPrice = item.DonGia,
                                Discount = item.GiamGia,
                                Quantity = item.SoLuong
                            };
                            _context.Add(orderDetail);
                        }
                        _context.SaveChanges();

                        #region Send mail
                        #endregion

                        _cartService.RemoveAll();
                        if(coupon != null)
                        {
                            coupon.Status = CouponStatus.Used;
                            _context.Update(coupon);
                            _context.SaveChanges();
                        }
                        trans.Commit();
                        message = "Đơn hàng của bạn được xử lý thành công";
                    }
                    catch {
                        trans.Rollback();
                        message = "Xử lý đặt hàng không thành công";
                    }
                }

            }

            return View("Inform", message);
        }
    }
}