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
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Security.Cryptography;

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

        string HashSh1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

                // declare stringbuilder
                var sb = new StringBuilder(hashSh1.Length * 2);

                // computing hashSh1
                foreach (byte b in hashSh1)
                {
                    // "x2"
                    sb.Append(b.ToString("X2").ToLower());
                }
                return sb.ToString();
            }
        }

        public IActionResult Index()
        {
            //var product_hightlight = _context.SanPhams.Where(m => m.ProductHighlights == 1).Include(m=>m.Thongtinkythuatlaptops).ToList();
            //  ViewBag.product_hightlight = product_hightlight;
            // ở view nào thì lấy ViewBag ngay view đó
            ViewBag.Category = _context.DanhMucSanPhams.ToList();
            ViewBag.BaiViet = _context.InfomationTechologies.ToList();
            return View(_context.SanPhams.Where(m => m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList());
        }

        public async Task<IActionResult> Catelogy()
        {
            var t = _context.DanhMucSanPhams.Where(m => m.TinhTrang == 2).ToListAsync();
            ViewBag.DanhMuc = t;

            ViewBag.CateChild = _context.DanhMucCons.Where(m => m.TinhTrang == 2).ToListAsync();

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
            HttpContext.Session.SetInt32("OnlineCus", 0);
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
                var t = _context.KhachHangs.Where(m => m.TenDangNhap == khachHang.TenDangNhap && m.MatKhau == HashSh1(khachHang.MatKhau)).FirstOrDefault();
                if (t != null)
                {
                    ModelState.AddModelError("LoginError", "");
                    // đăng ký seesion va luu gia tri 
                    HttpContext.Session.SetInt32("Id", t.IdCustomer);
                    HttpContext.Session.SetString("FullName", t.HoVaTen);
                    HttpContext.Session.SetString("UserName", t.TenDangNhap);
                    HttpContext.Session.SetString("Avatar", t.Avartar);
                    // TÍNH SỐ LƯỢNG TRUY CẬP
                    Int32 online = Convert.ToInt32(HttpContext.Session.GetInt32("OnlineCus"));
                    online++;
                    HttpContext.Session.SetInt32("OnlineCus", online);
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
            Int32 online = Convert.ToInt32(HttpContext.Session.GetInt32("OnlineCus"));
            online--;
            HttpContext.Session.SetInt32("OnlineCus", online);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        public IActionResult SearchDanhMuc(int id)
        {
            // lấy danh sách sản phẩm ở CateChild có mã chứa IdDanhMucSanPham = Id truyền vào ở bảng CateChild
            // 5 = 5 truy xuất ở 2 bảng
            // lấy các danh mục con thuộc cái id này
            ViewBag.CateChild = _context.DanhMucCons.Where(m => m.IdDanhMucSanPham == id).ToList();
            var product = _context.SanPhams.Where(m => m.CateChildNavigation.IdDanhMucSanPham == id && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
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
            // lấy dữ liệu của DanhMucCon ở id truyền vào
            ViewBag.Id = _context.DanhMucCons.Where(m => m.CatelogyChild == id).FirstOrDefault();
            var product = _context.SanPhams.Where(m => m.CateChild == id && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
            return View(product);
        }

        // Lọc sản phẩm
        [HttpPost]
        public IActionResult FitterProduct(int IdHangsx, int price, int TinhTrang, int id)
        {
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            if (IdHangsx == 0 && price == 0 && TinhTrang == 0)
            {
                return View();
            }
            else
            {
                if (IdHangsx != 0 && price != 0 && TinhTrang != 0)
                {
                    // trả về trang VỚI  ACTION
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.CateChild == price && m.HangSxId == IdHangsx && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && price == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (price != 0 && IdHangsx == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.CateChild == price && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (price == 0 && IdHangsx == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && price == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && price != 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == price && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx == 0 && price != 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == price && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else
                {
                    var t = _context.SanPhams.Where(m => m.CateChild == price && m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
            }
        }


        public IActionResult OrderList()
        {
            var id_customer = HttpContext.Session.GetInt32("Id");
            ViewBag.OrderDetails = _context.DatHangChiTiets.Include(m => m.LapTop).ToList();
            return View(_context.DatHangs.Where(m => m.KhachHangId == id_customer).Include(m => m.DatHangChiTiets).Include(m => m.KhachHang).ToList());
        }

        public IActionResult CancelOrder(int id)
        {
            var order = _context.DatHangs.Where(m => m.Id == id).FirstOrDefault();
            var order_details = _context.DatHangChiTiets.Where(m => m.DatHangId == id).ToList();
            order.TinhTrang = 2;
            foreach (var item in order_details)
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
            var order = _context.DatHangChiTiets.Where(m => m.DatHangId == id).Include(m => m.LapTop).Include(m => m.DatHang).ToList();
            ViewBag.InfoPro = _context.Thongtinkythuatlaptops.ToList();
            return View(order);
        }

        // Get lấy dữ liệu đưa lên form 
        public IActionResult Profile()
        {
            var id_customer = HttpContext.Session.GetInt32("Id");
            var profile = _context.KhachHangs.Where(m => m.IdCustomer == id_customer).FirstOrDefault();
            ModelState.AddModelError("ErrorPass", "");
            return View(profile);
        }
        //Post lấy dữ liệu từ form của get đưa vào csdl
        [HttpPost]
        public IActionResult Profile(IFormFile file, string checkpass, string Old_Pass, KhachHang khachHang)
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
                    if (khachHang.Avartar == "NoImage.jpg")
                    {
                        khachHang.Avartar = Upload(file);
                        HttpContext.Session.SetString("Avatar", khachHang.Avartar);
                    }

                    else
                    {
                        DeleteImages(khachHang.Avartar);
                        khachHang.Avartar = Upload(file);
                        HttpContext.Session.SetString("Avatar", khachHang.Avartar);
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
            }
            if (checkpass != null && khachHang.MatKhau != null)
            {
                string check_pass = HashSh1(checkpass);
                if (Old_Pass == check_pass)
                {
                    if (Old_Pass == khachHang.MatKhau) khachHang.MatKhau = Old_Pass;
                    else khachHang.MatKhau = HashSh1(khachHang.MatKhau);
                    _context.Update(khachHang);
                    _context.SaveChanges();
                    return RedirectToAction("DangXuat");
                }
                else
                {
                    ModelState.AddModelError("ErrorPass", "Mật khẩu củ không đúng");
                    return View(khachHang);
                }
            }
            else khachHang.MatKhau = Old_Pass;
            HttpContext.Session.SetInt32("Id", khachHang.IdCustomer);
            HttpContext.Session.SetString("FullName", khachHang.HoVaTen);
            HttpContext.Session.SetString("UserName", khachHang.TenDangNhap);
            HttpContext.Session.SetString("Avatar", khachHang.Avartar);
            _context.Update(khachHang);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
                khachHang.MatKhau = HashSh1(khachHang.MatKhau);
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

        // danh sach bai viet
        public IActionResult BaiViet()
        {
            return View(_context.InfomationTechologies.Where(m=>m.TinhTrang == 2).ToList());
        }
        public IActionResult ChiTietBaiViet(int id)
        {
            var info = _context.InfomationTechologies.Where(m => m.IdBaiViet == id).FirstOrDefault();
            info.LuotXem++;
            _context.InfomationTechologies.Update(info);
            _context.SaveChanges();
            return View(_context.InfomationTechologies.Where(m=>m.IdBaiViet == id ).FirstOrDefault());
        }

        public IActionResult AllProducts()
        {
            //var product_hightlight = _context.SanPhams.Where(m => m.ProductHighlights == 1).Include(m=>m.Thongtinkythuatlaptops).ToList();
            //  ViewBag.product_hightlight = product_hightlight;
            // ở view nào thì lấy ViewBag ngay view đó
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            return View(_context.SanPhams.Where(m => m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList());
        }



         // Lọc sản phẩm
        [HttpPost]
        public IActionResult FitterAllProduct(int IdHangsx, int price, int TinhTrang)
        {
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            if (IdHangsx == 0 && price == 0 && TinhTrang == 0)
            {
                return View();
            }
            else
            {
                if (IdHangsx != 0 && price != 0 && TinhTrang != 0)
                {
                    if (price == 1)
                    {
                        // trả về trang VỚI  ACTION
                        var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.GiaBan > 10000000 || m.GiaBan < 15000000 && m.HangSxId == IdHangsx && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 2)
                    {
                        var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.GiaBan > 15000000 || m.GiaBan < 30000000 && m.HangSxId == IdHangsx && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 3)
                    {
                        var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.GiaBan > 30000000 && m.HangSxId == IdHangsx && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 4)
                    {
                        var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.GiaBan < 10000000 && m.HangSxId == IdHangsx && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }

                }
                else if (IdHangsx != 0 && price == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (price != 0 && IdHangsx == 0 && TinhTrang == 0)
                {
                    if(price == 1)
                    {
                        var t = _context.SanPhams.Where(m => m.GiaBan > 10000000 && m.GiaBan < 15000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 2)
                    {
                        var t = _context.SanPhams.Where(m => m.GiaBan > 15000000 && m.GiaBan < 30000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 3)
                    {
                        var t = _context.SanPhams.Where(m => m.GiaBan > 30000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 4)
                    {
                        var t = _context.SanPhams.Where(m => m.GiaBan < 10000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }

                }
                else if (price == 0 && IdHangsx == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && price == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && price != 0 && TinhTrang == 0)
                {
                    if(price == 1)
                    {
                        var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.GiaBan > 10000000 && m.GiaBan < 15000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 2)
                    {
                        var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.GiaBan > 15000000 && m.GiaBan < 30000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 3)
                    {
                        var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.GiaBan > 30000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 4)
                    {
                        var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.GiaBan < 10000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }

                }
                else if (IdHangsx == 0 && price != 0 && TinhTrang != 0)
                {
                    if(price == 1)
                    {
                        var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.GiaBan > 10000000 && m.GiaBan < 15000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 2)
                    {
                        var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.GiaBan > 15000000 && m.GiaBan < 30000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 3)
                    {
                        var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.GiaBan > 30000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 4)
                    {
                        var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.GiaBan < 10000000 && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                }
                else
                {
                    if(price == 1)
                    {
                        var t = _context.SanPhams.Where(m => m.GiaBan > 10000000 && m.GiaBan < 15000000 && m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 2)
                    {
                        var t = _context.SanPhams.Where(m => m.GiaBan > 15000000 && m.GiaBan < 30000000 && m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 3)
                    {
                        var t = _context.SanPhams.Where(m => m.GiaBan > 30000000 && m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                    if (price == 4)
                    {
                        var t = _context.SanPhams.Where(m => m.GiaBan < 10000000 && m.IdTinhTrang == TinhTrang && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList();
                        return View(t);
                    }
                }
                return View();
            }
        }


        public IActionResult Search(string Key)
        {
            //var product_hightlight = _context.SanPhams.Where(m => m.ProductHighlights == 1).Include(m=>m.Thongtinkythuatlaptops).ToList();
            //  ViewBag.product_hightlight = product_hightlight;
            // ở view nào thì lấy ViewBag ngay view đó
            return View(_context.SanPhams.Where(m => m.TenSanPham.Contains(Key) || m.HangSx.TenHang.Contains(Key) && m.OutOfSock == 2 && m.SoLuong > 0 && m.TrangThai == 2).Include(m => m.HangSx).ToList());
        }

    }
}
