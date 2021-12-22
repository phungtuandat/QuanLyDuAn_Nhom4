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

        public IActionResult CheckOut()
        {
            return View(GetCartItems());
        }

        [HttpPost]
        public IActionResult Orders(string address,string phone)
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("ShopCart");
            var cartItem = JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            // lấy session khách hàng
            int Customer = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            // tạo đại diện cho đặt hàng
            var order = new DatHang();
            order.KhachHangId = Customer;
            order.NgayDatHang = DateTime.Now;
            order.TinhTrang = 0;
            order.DiaChiGiaoHang = address;
            order.DienThoaiGiaoHang = phone;
            order.TongTien = 0;
            // phải lưu đơn hàng vào database sau đó thực hiện tiếp không thì thêm vào nó sẻ lỗi vì không tồn tại ID_DatHang
            _context.DatHangs.Add(order);
            _context.SaveChanges();
            // ĐẶT HÀNG CHI TIẾT
            int sum_money = 0;
            foreach (var item in cartItem)
            {
                // có thể có nhiều đặt hàng chi tiết nên khởi tạo đối tượng trong foearch chạy nhiều lần
                var order_details = new DatHangChiTiet();
                // thêm sản phẩm vào đặt hàng chi tiết
                order_details.DatHangId = order.Id;
                order_details.LapTopId = item.Products.IdProduct;
                order_details.DonGia = item.Products.GiaBan;
                order_details.SoLuong = (short?)item.Quantity;
                order_details.ThanhTien = item.Products.GiaBan * (short?)item.Quantity;
                sum_money += Convert.ToInt32(order_details.ThanhTien);
                _context.DatHangChiTiets.Add(order_details);
                // cập nhật xóa số lượng
                var id_pro = _context.SanPhams.Where(m => m.IdProduct == item.Products.IdProduct).FirstOrDefault();
                id_pro.SoLuong = id_pro.SoLuong - item.Quantity;
                _context.SanPhams.Update(id_pro);
            }
            order.TongTien = sum_money;
            _context.DatHangs.Update(order);
            _context.SaveChanges();
            // xóa session giỏ hàng
            HttpContext.Session.Remove("ShopCart");
            return View();
        }

    }
}
