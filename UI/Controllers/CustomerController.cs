using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UI.Data;
using UI.Models;
using UI.ViewModels;
using UI.Helpers;
using Microsoft.AspNetCore.Authentication;

namespace UI.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public CustomerController(MyDbContext context, IMapper mapper)
        {
            _context = context; _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult CheckEmailUnique(string Email)
        {
            var customer = _context.Customers.Where(p => p.Email == Email).ToList();

            return Json(customer.Count == 0);
        }

        [AllowAnonymous]
        public IActionResult CheckUsernameUnique(string Username)
        {
            var customer = _context.Customers.Where(p => p.Username == Username).ToList();

            return Json(customer.Count == 0);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Customer/Create
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FullName,Email,Address,Phone,Username,Password")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(model);
                customer.RandomKey = MyTools.GenerateRandomKey();
                customer.Password = (model.Password + customer.RandomKey).ToMD5();

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Product");
            }
            return View(model);
        }

        public IActionResult Profile()
        {
            var customer = HttpContext.Session.Get<Customer>("KhachHang");
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile([Bind("CustomerId,FullName,Email,EmailConfirmed,Address,Phone,PhoneConfirmed,Username,Password,RandomKey,IsActive")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Profile));
            }
            return View(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl = null)
        {
            var kh = _context.Customers.SingleOrDefault(p => p.Username == model.Username);

            //tự kiểm tra đã Active chưa?
            //if(!kh.IsActive)
            //{
            //    ModelState.AddModelError("loi", "Tài khoản chưa kích hoạt");
            //    return View();
            //}

            if (kh != null)
            {
                //kiễm tra xem mật khẩu đúng ko
                if(kh.Password == (model.Password + kh.RandomKey).ToMD5())
                {
                    //thành công
                    //Ghi nhận Session
                    HttpContext.Session.Set<Customer>("KhachHang", kh);

                    //khai báo thông tin Identity
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name,
kh.FullName),
                        new Claim(ClaimTypes.Email, kh.Email),
                        //new Claim(ClaimTypes.Role, "KhachHang")
                    };
                    // create identity
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);


                    //nếu có trang yêu cầu trước đó
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Profile", "Customer");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Product");
        }
    }
}
