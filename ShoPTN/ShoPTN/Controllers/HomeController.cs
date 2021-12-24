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
using System.IO;

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
        public IActionResult Login(Customer_Login khachHang)
        {
            if (ModelState.IsValid)
            {
                // m = nhân viên
                var t = _context.KhachHangs.Where(m => m.TenDangNhap == khachHang.TenDangNhap && m.MatKhau == khachHang.MatKhau).FirstOrDefault();
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
                    return View(khachHang);
                }
            }
            return View(khachHang);
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
            ViewBag.InfoPro = _context.Thongtinkythuatlaptops.ToList();
            return View(order);
        }

        // Get lấy dữ liệu đưa lên form 
        public IActionResult Profile()
        {
            var id_customer = HttpContext.Session.GetInt32("Id");
            var profile = _context.KhachHangs.Where(m => m.IdCustomer == id_customer).FirstOrDefault();
            return View(profile);
        }

        //Post lấy dữ liệu từ form của get đưa vào csdl
        [HttpPost]
        public IActionResult Profile(IFormFile file, KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                    int i = 0;
                    var check = _context.KhachHangs.Where(m => m.IdCustomer != khachHang.IdCustomer).ToList();
                    foreach (var item in check)
                    {
                        if (item.TenDangNhap == khachHang.TenDangNhap)
                        {
                            i = 1;
                        }
                        else i = 0;
                    }
                    if (i == 0 && file != null)
                    {
                        var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        string fileExtension = Path.GetExtension(file.FileName).ToLower();
                        if (!fileTypeSupported.Contains(fileExtension))
                        {
                            ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                            ModelState.AddModelError("ErrorExitTDN", "");
                            return View(file);
                        }
                        else if (file.ContentType.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                            ModelState.AddModelError("ErrorExitTDN", "");
                            return View(file);
                        }
                        else if (check != null && !fileTypeSupported.Contains(fileExtension))
                        {
                            ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                            ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                            return View(khachHang);
                        }
                        else if (file.ContentType.Length > 2 * 1024 * 1024 && check != null)
                        {
                            ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                            ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                            return View(file);
                        }
                        else
                        {
                            ModelState.AddModelError("ErrorExit", "");
                            ModelState.AddModelError("ErrorExitTDN", "");
                            if(khachHang.Avartar == "NoImage.jpg") khachHang.Avartar = Upload(file);
                            else
                            {
                                DeleteImages(khachHang.Avartar);
                                khachHang.Avartar = Upload(file);
                            }
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                            ModelState.AddModelError("ErrorExit", "");
                            return View(khachHang);
                        }
                        else if (i == 0 && file == null)
                        {
                            // nếu tên đăng nhập không tồn tại và ảnh bằng rổng
                            khachHang.Avartar = khachHang.Avartar;
                        }
                    }
                _context.Update(khachHang);
                _context.SaveChanges();
            }
            return RedirectToAction("DangXuat");
        }

        //  Get 
        public IActionResult CreateAccount()
        {
            // GET: KhachHang/Create
            ModelState.AddModelError("ErrorExit", "");
            ModelState.AddModelError("ErrorExitTDN", "");
            return View();
        }


        [HttpPost]
        public IActionResult CreateAccount(IFormFile file,KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                var check = _context.KhachHangs.Where(m => m.IdCustomer != khachHang.IdCustomer).ToList();
                foreach (var item in check)
                {
                    if (item.TenDangNhap == khachHang.TenDangNhap)
                    {
                        // nếu như  có tồn tại 
                        i = 1;
                    }
                }
                if (i == 0 && file != null)
                {
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        ModelState.AddModelError("ErrorExitTDN", "");
                        return View(file);
                    }
                    else if (file.ContentType.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        ModelState.AddModelError("ErrorExitTDN", "");
                        return View(file);
                    }
                    else if (check != null && !fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                        return View(khachHang);
                    }
                    else if (file.ContentType.Length > 2 * 1024 * 1024 && check != null)
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                        return View(file);
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorExit", "");
                        ModelState.AddModelError("ErrorExitTDN", "");
                        khachHang.Avartar = Upload(file);
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        ModelState.AddModelError("ErrorExit", "");
                        ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                        return View(khachHang);
                    }
                    ModelState.AddModelError("ErrorExit", "");
                    khachHang.Avartar = "NoImage.jpg";
                }
                _context.Add(khachHang);
                _context.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
            return View(khachHang);
        }

        // XỬ LÝ ẢNH
        public string Upload(IFormFile file)
        {
            //string error = "";        
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\Images\\Customer\\{ UploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return UploadFileName;
        }

        public void DeleteImages(string name)
        {
            // lay duong dan anh
            var path = $"wwwroot\\Images\\Customer\\" + name;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
