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

        private List<int> GetListCategoryId(int cateId)
        {
            var result = new List<int>();

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(cateId);

            while(queue.Count > 0)
            {
                //lấy 1 phần tử ra để xét
                int tmp = queue.Dequeue();

                //thêm ptu vừa lấy vào tập kết quả
                result.Add(tmp);

                //kiếm xem có con ko để thêm vào danh sách
                var cateIdChild = _context.Categories.Where(p => p.ParentCategoryId == tmp).Select(p => p.CategoryId).ToList();
                foreach(var item in cateIdChild)
                {
                    queue.Enqueue(item);
                }
            }

            return result;
        }

        public IActionResult Index(int? cateId)
        {
            var category = new Category {
                CategoryName = "Tất cả"
            };
            var data = _context.Products.AsQueryable();
            if (cateId.HasValue)
            {
                category = _context.Categories.SingleOrDefault(p => p.CategoryId == cateId);
                //danh sách chứa các mã loại (cha + con)
                List<int> cateIdList = GetListCategoryId(cateId.Value);

                data = data.Where(p => p.CategoryId.HasValue && cateIdList.Contains(p.CategoryId.Value));
            }

            ViewBag.Category = category;

            return View(data);
        }

        public IActionResult Detail(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            if(product == null)//tìm ko có
            {
                return RedirectToAction("Index");
            }

            //lấy hình phụ
            product.ProductImages = _context.ProductImages.Where(p => p.ProductId == product.ProductId).ToList();

            return View(product);
        }

        public IActionResult Preview(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.ProductId == id);
            return PartialView(product);
        }

        public IActionResult Discount()
        {
            var data = _context.Products.Where(p => p.HasDiscount).OrderByDescending(p=> p.Discount);

            var category = new Category
            {
                CategoryName = "Khuyến mãi"
            };
            ViewBag.Category = category;

            return View("Index", data);
        }
    }
}