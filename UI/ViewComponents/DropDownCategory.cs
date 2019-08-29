﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Data;

namespace UI.ViewComponents
{
    public class DropDownCategory : ViewComponent
    {
        private readonly MyDbContext _context;
        public DropDownCategory(MyDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int? selectedCategory)
        {
            ViewBag.SelectedCategory = selectedCategory;
            return View("Default", _context.Categories.ToList());
        }
    }
}
