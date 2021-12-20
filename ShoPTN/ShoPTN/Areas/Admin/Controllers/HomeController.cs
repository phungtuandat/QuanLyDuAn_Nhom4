using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShoPTN.Data;
using ShoPTN.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoPTN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        // tên session gán cho giá trị bên dưới ở session 
        // const hằng số giá trị không thay đổi không có const vẫn được
        string Staff_Id = "_Id";
        string Staff_Avatar = "_Avatar";
        string Staff_UserNamee = "_UserName";
        string Staff_HoTen = "_HoTen";
        string Staff_Quyen = "_Quyen";
        private readonly ILogger<HomeController> _logger;
        private BanLapTopContext db = new BanLapTopContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // danh sách sản phẩm còn IActionResult: 1 sản phẩm
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("_UserName");
            // neu nhu ton tai dang nhap thi cho vao trang chinh con khong thi tra ve trang login
            if (username != null)
            {
                var t = db.SanPhams.ToList();
                return View(t);
            }
            return RedirectToAction("Login", "Home");

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
        public IActionResult Login(NhanVienLogin nhanvien)
        {
            if (ModelState.IsValid)
            {
                // m = nhân viên
                var t = db.NhanViens.Where(m => m.TenDangNhap == nhanvien.TenDangNhap && m.MatKhau == nhanvien.MatKhau).FirstOrDefault();
                if (t != null)
                {
                    ModelState.AddModelError("LoginError", "");
                    // đăng ký seesion va luu gia tri 
                    HttpContext.Session.SetInt32(Staff_Id, t.IdStaff);
                    HttpContext.Session.SetString(Staff_HoTen, t.HoVaTen);
                    HttpContext.Session.SetString(Staff_UserNamee, t.TenDangNhap);
                    HttpContext.Session.SetString(Staff_Avatar, t.Avartar);
                    HttpContext.Session.SetString(Staff_Quyen, t.Quyen.ToString());
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


        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Profiles(int? id)
        {
            var t = db.NhanViens.Where(m => m.IdStaff == id).FirstOrDefault();
            if (t == null)
            {
                return NotFound();
            }
            ModelState.AddModelError("ErrorExit", "");
            ModelState.AddModelError("ErrorExitTDN", "");
            return View(t);
        }
        // tai khoan
        //GET: NhanVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profiles(IFormFile file, NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                var check = db.NhanViens.Where(m => m.IdStaff != nhanVien.IdStaff).ToList();
                foreach (var item in check)
                {
                    if (item.TenDangNhap == nhanVien.TenDangNhap)
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
                        return View(nhanVien);
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
                        nhanVien.Avartar = Upload(file);
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                        ModelState.AddModelError("ErrorExit", "");
                        return View(nhanVien);
                    }
                    else if (i == 0 && file == null)
                    {
                        nhanVien.Avartar = nhanVien.Avartar;
                    }
                }
                db.Update(nhanVien);
                db.SaveChanges();
                HttpContext.Session.SetInt32(Staff_Id, nhanVien.IdStaff);
                HttpContext.Session.SetString(Staff_HoTen, nhanVien.HoVaTen);
                HttpContext.Session.SetString(Staff_UserNamee, nhanVien.TenDangNhap);
                HttpContext.Session.SetString(Staff_Avatar, nhanVien.Avartar);
                HttpContext.Session.SetString(Staff_Quyen, nhanVien.Quyen.ToString());
                //HttpContext.Session.SetString("_HoTen", nhanVien.HoVaTen);
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("DangXuat","Home");
        }




        public string Upload(IFormFile file)
        {
            //string error = "";        
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\Images\\Staff\\{ UploadFileName}";
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
            var path = $"wwwroot\\Images\\Staff\\" + name;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
