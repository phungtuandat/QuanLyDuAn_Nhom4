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
using Microsoft.AspNetCore.Http;

namespace ShoPTN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BanLapTopContext _context;

        public HomeController(ILogger<HomeController> logger, BanLapTopContext db)
        {
            _logger = logger;
            _context = db;
        }

        public IActionResult Index()
        {
            //var product_hightlight = _context.SanPhams.Where(m => m.ProductHighlights == 1).Include(m=>m.Thongtinkythuatlaptops).ToList();
            //  ViewBag.product_hightlight = product_hightlight;
            // ở view nào thì lấy ViewBag ngay view đó
            ViewBag.Category = _context.DanhMucSanPhams.ToList();
            return View(_context.SanPhams.Include(m=>m.HangSx).ToList());
        }

        public async Task<IActionResult> Catelogy()
        {
            var t = _context.DanhMucSanPhams.ToListAsync();
            ViewBag.DanhMuc = t ;

            ViewBag.CateChild = _context.DanhMucCons.ToListAsync();

            var product = _context.SanPhams.Include(b => b.CateChild);
            return View(await product.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Get Login
        // GET: InfomationTechologies/Create
        public IActionResult Login()
        {
            ModelState.AddModelError("LoginError", "");
            return View();
        }


        //POST LOgin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Customer_Login nhanvien)
        {
            if (ModelState.IsValid)
            {
                // m = nhân viên
                var t = _context.KhachHangs.Where(m => m.TenDangNhap == nhanvien.TenDangNhap && m.MatKhau == nhanvien.MatKhau).FirstOrDefault();
                if (t != null)
                {
                    ModelState.AddModelError("LoginError", "");
                    // đăng ký seesion va luu gia tri 
                    HttpContext.Session.SetInt32("Id", t.IdCustomer);
                    HttpContext.Session.SetString("FullName", t.HoVaTen);
                    HttpContext.Session.SetString("UserName", t.TenDangNhap);
                    HttpContext.Session.SetString("Avatar", t.Avartar);
                    // chuyển hướng đến action nào
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // trả về lỗi
                    ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không đúng");
                    return View(nhanvien);
                }
            }
            return View(nhanvien);
        }

        // đăng xuất
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        public IActionResult SearchDanhMuc(int id)
        {
            // lấy danh sách sản phẩm ở CateChild có mã chứa IdDanhMucSanPham = Id truyền vào ở bảng CateChild
            // 5 = 5 truy xuất ở 2 bảng
            var product = _context.SanPhams.Where(m => m.CateChildNavigation.IdDanhMucSanPham == id).Include(m=>m.HangSx).ToList();
            return View(product);
        }

    }
}
