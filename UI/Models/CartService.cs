using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UI.Data;
using UI.Helpers;

namespace UI.Models
{
    public interface ICartService
    {
        void AddToCart(int productId, int quantity = 1);
        void RemoveItem(int productId);
        List<CartItem> GetCart();
    }

    public class CartService : ICartService
    {
        private readonly MyDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        public CartService(MyDbContext db, IHttpContextAccessor httpContext, IMapper mapper)
        {
            _context = db; _httpContext = httpContext;
            _mapper = mapper;
        }
        public void AddToCart(int productId, int quantity = 1)
        {
            var carts = Carts;
            var item = carts.SingleOrDefault(p => p.MaHh == productId);
            if (item != null)//đã có
            {
                item.SoLuong += quantity;
            }
            else
            {
                var product = _context.Products.SingleOrDefault(p => p.ProductId == productId);
                if(product != null)
                {
                    //map product ---> cartItem
                    item = _mapper.Map<CartItem>(product);
                    item.SoLuong = quantity;

                    carts.Add(item);
                }
            }

            //update Session
            _httpContext.HttpContext.Session.Set("Cart", carts);
        }

        public List<CartItem> Carts
        {
            get
            {
                var carts = _httpContext.HttpContext.Session.Get<List<CartItem>>("Cart");
                if (carts == null)
                    carts = new List<CartItem>();
                return carts;
            }
        }
        public List<CartItem> GetCart()
        {
            return Carts;
        }

        public void RemoveItem(int productId)
        {
            var carts = Carts;
            var item = carts.SingleOrDefault(p => p.MaHh == productId);
            if(item != null)
            {
                carts.Remove(item);
                //update Session
                _httpContext.HttpContext.Session.Set("Cart", carts);
            }
        }
    }
}
