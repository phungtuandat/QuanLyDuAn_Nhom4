using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class KhachHangController : Controller
    {
        private readonly BanLapTopContext _context;

        public KhachHangController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhachHangs.ToListAsync());
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.IdCustomer == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            ModelState.AddModelError("ErrorExit", "");
            ModelState.AddModelError("ErrorExitTDN", "");
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file,[Bind("IdCustomer,HoVaTen,DienThoai,DiaChi,TenDangNhap,MatKhau,Avartar")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                var check = await _context.KhachHangs.Where(m => m.IdCustomer != khachHang.IdCustomer).ToListAsync();
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
                    if (i == 1 && file == null)
                    {
                        ModelState.AddModelError("ErrorExit", "Vui lòng chọn hình ảnh");
                        ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                        return View(khachHang);
                    }
                    else if (i == 1 || file == null)
                    {
                        if (file == null)
                        {
                            ModelState.AddModelError("ErrorExit", "Vui lòng chọn hình ảnh");
                            ModelState.AddModelError("ErrorExitTDN", "");
                            return View(khachHang);
                        }
                        else if (i == 1)
                        {
                            ModelState.AddModelError("ErrorExitTDN", "Tên đăng nhập đã tồn tại");
                            ModelState.AddModelError("ErrorExit", "");
                            return View(khachHang);
                        }
                    }
                }
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            ModelState.AddModelError("ErrorExit", "");
            ModelState.AddModelError("ErrorExitTDN", "");
            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file ,[Bind("IdCustomer,HoVaTen,DienThoai,DiaChi,TenDangNhap,MatKhau,Avartar")] KhachHang khachHang)
        {
            if (id != khachHang.IdCustomer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int i = 0;
                    var check = await _context.KhachHangs.Where(m => m.IdCustomer != khachHang.IdCustomer).ToListAsync();
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
                            if (khachHang.Avartar == "NoImage.jpg") khachHang.Avartar = Upload(file);
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
                        // nếu tên đăng nhập không trùng mà ảnh rổng
                        else if (i == 0 && file == null)
                        {
                            khachHang.Avartar = khachHang.Avartar;
                        }
                    }
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.IdCustomer))
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
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.IdCustomer == id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs.Any(e => e.IdCustomer == id);
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
