using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoPTN.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ShoPTN.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace ShoPTN.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly BanLapTopContext _context;

        public CartController(ILogger<CartController> logger, BanLapTopContext db)
        {
            _logger = logger;
            _context = db;
        }


        public IActionResult Index()
        {
            // return về trang Có Action Index
            // TRẢ VỀ DANH SÁCH GIỎ HÀNG
            return View(GetCartItems());
        }

        // Lưu danh sách CartItem trong giỏ hàng vào session
        void SaveCartSession(List<Cart> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("ShopCart", jsoncart);
        }

        // Đọc danh sách CartItem từ session
        List<Cart> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("ShopCart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            }
            return new List<Cart>();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        // Cho hàng vào giỏ
        public async Task<IActionResult> AddToCart(int id,int quanlity)
        {
            var product = await _context.SanPhams
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }
            var cart = GetCartItems();
            var item = cart.Find(p => p.Products.IdProduct == id);
            if (item != null)
            {
                if (quanlity == 0) item.Quantity++;
                else item.Quantity += quanlity;
            }
            else
            {
                if(quanlity == 0)
                {
                    cart.Add(new Cart() { Products = product, Quantity = 1 });
                }
                else
                    cart.Add(new Cart() { Products = product, Quantity = quanlity });

            }
            SaveCartSession(cart);
            return RedirectToAction("Index", "Cart");
        }

        // Chuyển đến view xem giỏ hàng
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        // Xóa một mặt hàng khỏi giỏ
        public IActionResult RemoveItem(int id)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Products.IdProduct == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCartSession(cart);
            return RedirectToAction("Index", "Cart");
        }

        // Cập nhật số lượng một mặt hàng trong giỏ
        public IActionResult UpdateItem(int id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Products.IdProduct == id);
            if (item != null)
            {
                item.Quantity = quantity;
            }
            SaveCartSession(cart);
            return RedirectToAction("Index", "Cart");
        }

    }
}
