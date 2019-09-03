using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Data;

namespace UI.ViewComponents
{
    public class CategoryMenu : ViewComponent
    {
        private readonly MyDbContext _context;
        public CategoryMenu(MyDbContext db)
        {
            _context = db;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
    }
}
