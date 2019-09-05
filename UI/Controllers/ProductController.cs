using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UI.Data;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyDbContext _context;
        public ProductController(MyDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            var data = _context.Products.ToList();

            return View(data);
        }
    }
}