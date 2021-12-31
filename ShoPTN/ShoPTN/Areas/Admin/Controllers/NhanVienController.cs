using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoPTN.Data;
using ShoPTN.Models;

namespace ShoPTN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly BanLapTopContext _context;

        public NhanVienController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: NhanVien
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhanViens.ToListAsync());
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
        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.IdStaff == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            ModelState.AddModelError("ErrorExit", "");
            ModelState.AddModelError("ErrorExitTDN", "");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file,[Bind("IdStaff,HoVaTen,DienThoai,DiaChi,TenDangNhap,MatKhau,Avartar,Quyen")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                var check = await _context.NhanViens.Where(m => m.IdStaff != nhanVien.IdStaff).ToListAsync();
                foreach (var item in check)
                {
                    if (item.TenDangNhap == nhanVien.TenDangNhap)
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
                    if (i == 1 && file == null)
                    {
                        ModelState.AddModelError("ErrorExit", "Vui lòng chọn hình ảnh");
                        ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                        return View(nhanVien);
                    }
                    else if (i == 1 || file == null)
                    {
                        if (file == null)
                        {
                            ModelState.AddModelError("ErrorExit", "Vui lòng chọn hình ảnh");
                            ModelState.AddModelError("ErrorExitTDN", "");
                            return View(nhanVien);
                        }
                        else if (i == 1)
                        {
                            ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                            ModelState.AddModelError("ErrorExit", "");
                            return View(nhanVien);
                        }
                    }
                }
                nhanVien.MatKhau = HashSh1(nhanVien.MatKhau);
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            ModelState.AddModelError("ErrorExit", "");
            ModelState.AddModelError("ErrorExitTDN", "");
            return View(nhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Pass_Old, IFormFile file, [Bind("IdStaff,HoVaTen,DienThoai,DiaChi,TenDangNhap,MatKhau,Avartar,Quyen")] NhanVien nhanVien)
        {
            if (id != nhanVien.IdStaff)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int i = 0;
                    var check = await _context.NhanViens.Where(m => m.IdStaff != nhanVien.IdStaff).ToListAsync();
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
                            if (nhanVien.Avartar == "NoImage.jpg") nhanVien.Avartar = Upload(file);
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
                    if (nhanVien.MatKhau == Pass_Old) nhanVien.MatKhau = Pass_Old;
                    else nhanVien.MatKhau = HashSh1(nhanVien.MatKhau);
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.IdStaff))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.IdStaff == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanViens.Any(e => e.IdStaff == id);
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
            else
            {
                TempData["ErrorImage"] = "Vui lòng chọn hình ảnh";
                ViewBag.SuccessBody = TempData["ErrorImage"];
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
