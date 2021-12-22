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
            // lấy các danh mục con thuộc cái id này
            ViewBag.CateChild = _context.DanhMucCons.Where(m=>m.IdDanhMucSanPham == id).ToList();
            var product = _context.SanPhams.Where(m => m.CateChildNavigation.IdDanhMucSanPham == id).Include(m=>m.HangSx).ToList();
            return View(product);
        }

        public IActionResult Product_Catechild(int id)
        {
            // lấy danh sách sản phẩm ở CateChild có mã chứa IdDanhMucSanPham = Id truyền vào ở bảng CateChild
            // 5 = 5 truy xuất ở 2 bảng
            // lấy các danh mục con thuộc cái id này
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            ViewBag.Id = _context.DanhMucCons.Where(m=>m.CatelogyChild == id).FirstOrDefault();
            var product = _context.SanPhams.Where(m => m.CateChild==id).Include(m => m.HangSx).ToList();
            return View(product);
        }

        // Lọc sản phẩm
        [HttpPost]
        public IActionResult FitterProduct(int IdHangsx, int LoaiSanPham, int TinhTrang, int id)
        {
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            if (IdHangsx == 0 && LoaiSanPham == 0 && TinhTrang == 0)
            {
                return View();
            }
            else
            {
                if (IdHangsx != 0 && LoaiSanPham != 0 && TinhTrang != 0)
                {
                    // trả về trang VỚI  ACTION
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.CateChild == LoaiSanPham && m.HangSxId == IdHangsx && m.CateChild == id).Include(m=>m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && LoaiSanPham == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == id).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (LoaiSanPham != 0 && IdHangsx == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.CateChild == LoaiSanPham && m.CateChild == id).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (LoaiSanPham == 0 && IdHangsx == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.CateChild == id).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && LoaiSanPham == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.IdTinhTrang == TinhTrang && m.CateChild == id).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && LoaiSanPham != 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == LoaiSanPham && m.CateChild == id).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx == 0 && LoaiSanPham != 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == LoaiSanPham && m.CateChild == id).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else
                {
                    var t = _context.SanPhams.Where(m => m.CateChild == LoaiSanPham && m.IdTinhTrang == TinhTrang && m.CateChild == id).Include(m => m.HangSx).ToList();
                    return View(t);
                }
            }
        }


        public IActionResult OrderList()
        {
            var id_customer = HttpContext.Session.GetInt32("Id");
            return View(_context.DatHangs.Where(m=>m.KhachHangId == id_customer).Include(m=>m.DatHangChiTiets).Include(m=>m.KhachHang).ToList());
        }

        public IActionResult CancelOrder(int id)
        {
            var order = _context.DatHangs.Where(m=>m.Id == id).FirstOrDefault();
            var order_details = _context.DatHangChiTiets.Where(m => m.DatHangId == id).ToList();
            order.TinhTrang = 2;
            foreach(var item in order_details)
            {
                // lấy sản phẩm trong đặt hàng chi tiết
                var product = _context.SanPhams.Where(m => m.IdProduct == item.LapTopId).FirstOrDefault();
                // cập nhật số lượng trong kho trong bảng Đặt hàng chi tiết
                product.SoLuong += item.SoLuong;
                // update và Lưu
                _context.SanPhams.Update(product);
                _context.SaveChanges();
            }
            _context.DatHangs.Update(order);
            _context.SaveChanges();
            return RedirectToAction("OrderList");
        }

        public IActionResult OrderDetail(int id)
        {
            var order = _context.DatHangChiTiets.Where(m => m.DatHangId == id).Include(m=>m.LapTop).Include(m=>m.DatHang).ToList();
            foreach(var item in order)
            {
                ViewBag.InfoProduct = _context.Thongtinkythuatlaptops.Where(m => m.IdProduct == item.LapTopId).ToList();
            }    
            return View(order);
        }
    }
}
