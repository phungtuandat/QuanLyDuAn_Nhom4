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
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        // danh sách sản phẩm còn IActionResult: 1 sản phẩm
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("_UserName");
            // neu nhu ton tai dang nhap thi cho vao trang chinh con khong thi tra ve trang login
            if (username != null)
            {
                var t = db.DatHangs.Include(m => m.DatHangChiTiets).ToList();
                List<DataPoint> dataPoints = new List<DataPoint>();
                foreach(var item in t)
                {
                    dataPoints.Add(new DataPoint(item.NgayDatHang.ToString(), item.TongTien));
                }
                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
                //
                //
                ViewBag.OrderDetalis = db.DatHangChiTiets.ToList();
                ViewBag.SanPham = db.SanPhams.ToList();
                ViewBag.Staff = db.NhanViens.ToList();
                ViewBag.Customer = db.KhachHangs.ToList();
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
            HttpContext.Session.SetInt32("OnlineStaff", 0);
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
                // m = nhân viên/
                if(nhanvien.TenDangNhap == null || nhanvien.MatKhau == null)
                {
                    return View();
                }
                else
                {
                    var t = db.NhanViens.Where(m => m.TenDangNhap == nhanvien.TenDangNhap && m.MatKhau == HashSh1(nhanvien.MatKhau)).FirstOrDefault();
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
                        Int32 online = Convert.ToInt32(HttpContext.Session.GetInt32("OnlineStaff"));
                        online++;
                        HttpContext.Session.SetInt32("OnlineStaff", online);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // trả về lỗi
                        ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không đúng");
                        return View(nhanvien);
                    }
                }
            }
            return View(nhanvien);
        }


        public IActionResult DangXuat()
        {
            Int32 online = Convert.ToInt32(HttpContext.Session.GetInt32("OnlineStaff"));
            online--;
            HttpContext.Session.SetInt32("OnlineStaff", online);
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
            ModelState.AddModelError("ErrorPass", "");
            return View(t);
        }
        // tai khoan
        //GET: NhanVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profiles(string Old_Pass, string checkpass, IFormFile file, NhanVien nhanVien)
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
                    if (nhanVien.Avartar == "NoImage.jpg")
                    {
                        nhanVien.Avartar = Upload(file);
                    }

                    else
                    {
                        DeleteImages(nhanVien.Avartar);
                        nhanVien.Avartar = Upload(file);
                    }
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
            }
            if (checkpass != null && nhanVien.MatKhau != null)
            {
                string check_pass = HashSh1(checkpass);
                if (Old_Pass == check_pass)
                {
                    if (Old_Pass == nhanVien.MatKhau) nhanVien.MatKhau = Old_Pass;
                    else nhanVien.MatKhau = HashSh1(nhanVien.MatKhau);
                    db.Update(nhanVien);
                    db.SaveChanges();
                    return RedirectToAction("DangXuat");
                }
                else
                {
                    ModelState.AddModelError("ErrorPass", "Mật khẩu củ không đúng");
                    return View(nhanVien);
                }
            }
            else nhanVien.MatKhau = Old_Pass;
            HttpContext.Session.SetInt32(Staff_Id, nhanVien.IdStaff);
            HttpContext.Session.SetString(Staff_HoTen, nhanVien.HoVaTen);
            HttpContext.Session.SetString(Staff_UserNamee, nhanVien.TenDangNhap);
            HttpContext.Session.SetString(Staff_Avatar, nhanVien.Avartar);
            db.Update(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        public static string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }
    }
}
