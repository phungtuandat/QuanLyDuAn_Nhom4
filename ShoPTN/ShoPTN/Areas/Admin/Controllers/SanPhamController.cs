using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
    public class SanPhamController : Controller
    {
        private readonly BanLapTopContext _context;

        public SanPhamController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: Admin/SanPham
        public async Task<IActionResult> Index()
        {
            var banLapTopContext = _context.SanPhams.Include(s => s.CateChildNavigation).Include(s => s.HangSx).Include(s => s.IdNhanVienNavigation).Include(s => s.IdTinhTrangNavigation);
            return View(await banLapTopContext.ToListAsync());
        }

        // GET: Admin/SanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.CateChildNavigation)
                .Include(s => s.HangSx)
                .Include(s => s.IdNhanVienNavigation)
                .Include(s => s.IdTinhTrangNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/SanPham/Create
        public IActionResult Create()
        {
            ViewData["CateChild"] = new SelectList(_context.DanhMucCons, "CatelogyChild", "TenDanhMuc");
            ViewData["HangSxId"] = new SelectList(_context.HangSxes, "Id", "TenHang");
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdStaff", "HoVaTen");
            ViewData["IdTinhTrang"] = new SelectList(_context.TinhTrangs, "MaTt", "TenTt");
            ModelState.AddModelError("ErrorExit","");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file,[Bind("IdProduct,HangSxId,CateChild,IdNhanVien,IdTinhTrang,HinhAnh,GiaBan,GiaKhuyenMai,OutOfSock,TrangThai,ProductNew,ProductHighlights,SoLuong,FolderName,TenSanPham,MoTa")] SanPham sanPham)
        {

            ViewData["CateChild"] = new SelectList(_context.DanhMucCons, "CatelogyChild", "TenDanhMuc", sanPham.CateChild);
            ViewData["HangSxId"] = new SelectList(_context.HangSxes, "Id", "TenHang", sanPham.HangSxId);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdStaff", "HoVaTen", sanPham.IdNhanVien);
            ViewData["IdTinhTrang"] = new SelectList(_context.TinhTrangs, "MaTt", "TenTt", sanPham.IdTinhTrang);
            if (ModelState.IsValid)
            {
                var check = await _context.SanPhams.Where(m => m.IdProduct != sanPham.IdProduct).ToListAsync();
                if (file != null)
                {
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(sanPham);
                    }
                    else if (file.ContentType.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(sanPham);
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorExit", "");
                        int i = 0;
                        foreach (var item in check)
                        {
                            if (item.TenSanPham == sanPham.TenSanPham)
                            {
                                // nếu như  có tồn tại 
                                item.SoLuong += sanPham.SoLuong;
                                // lưu vào csdl
                                await _context.SaveChangesAsync();
                                i++;
                                // trả về trang index
                                return RedirectToAction(nameof(Index));
                            }
                            else i = 0;
                        }
                        if (i == 0)
                        {
                            ModelState.AddModelError("ErrorExit","");
                            sanPham.FolderName = Create_Folder(sanPham.TenSanPham);
                            string foldername = sanPham.FolderName;
                            sanPham.HinhAnh = Upload(file, foldername);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("ErrorExit", "Vui lòng chọn hình ảnh");
                    return View(sanPham);
                }
                if(sanPham.SoLuong > 0) sanPham.OutOfSock = 2;
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["CateChild"] = new SelectList(_context.DanhMucCons, "CatelogyChild", "TenDanhMuc", sanPham.CateChild);
            ViewData["HangSxId"] = new SelectList(_context.HangSxes, "Id", "TenHang", sanPham.HangSxId);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdStaff", "HoVaTen", sanPham.IdNhanVien);
            ViewData["IdTinhTrang"] = new SelectList(_context.TinhTrangs, "MaTt", "TenTt", sanPham.IdTinhTrang);
            ModelState.AddModelError("ErrorExit", "");
            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("IdProduct,HangSxId,CateChild,IdNhanVien,IdTinhTrang,HinhAnh,GiaBan,GiaKhuyenMai,MoTa,OutOfSock,TrangThai,ProductNew,ProductHighlights,SoLuong,FolderName,TenSanPham,MoTa")] SanPham sanPham)
        {
            if (id != sanPham.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var check = await _context.SanPhams.Where(m => m.IdProduct != sanPham.IdProduct).ToListAsync();
                    if (file != null)
                    {
                        var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        string fileExtension = Path.GetExtension(file.FileName).ToLower();
                        if (!fileTypeSupported.Contains(fileExtension))
                        {
                            ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                            return View(sanPham);
                        }
                        else if (file.ContentType.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                            return View(sanPham);
                        }
                        else
                        {
                            ModelState.AddModelError("ErrorExit", "");
                            string foldername = sanPham.FolderName = sanPham.FolderName;
                            // lay ten folder o replace , ten file o sanpham.hinh anh de biet xoa hinh anh nao trong folder do
                            DeleteImages(sanPham.FolderName.Replace("wwwroot/Images/Products/", ""), sanPham.HinhAnh);
                            sanPham.HinhAnh = Upload(file, sanPham.FolderName);
                        }
                    }
                    if (sanPham.SoLuong > 0) sanPham.OutOfSock = 2;
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.IdProduct))
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
            ViewData["CateChild"] = new SelectList(_context.DanhMucCons, "CatelogyChild", "TenDanhMuc", sanPham.CateChild);
            ViewData["HangSxId"] = new SelectList(_context.HangSxes, "Id", "TenHang", sanPham.HangSxId);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdStaff", "HoVaTen", sanPham.IdNhanVien);
            ViewData["IdTinhTrang"] = new SelectList(_context.TinhTrangs, "MaTt", "TenTt", sanPham.IdTinhTrang);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.CateChildNavigation)
                .Include(s => s.HangSx)
                .Include(s => s.IdNhanVienNavigation)
                .Include(s => s.IdTinhTrangNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            DeleteFolder(sanPham.FolderName);
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.IdProduct == id);
        }

        // XỬ LÝ ẢNH
        public string Create_Folder(string FolderName)
        {
            FolderName = $"wwwroot/Images/Products/{FolderName.Replace("/", "_").Replace(" ","_")}";
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }
            else
            {
                Directory.CreateDirectory(FolderName + DateTime.Today.ToShortDateString());
            }
            return FolderName;
        }
        public string Upload(IFormFile file, string folder_name)
        {
            //string error = "";
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"{folder_name}//{UploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return UploadFileName;
        }

        public void DeleteImages(string namefolder, string name)
        {
            // lay duong dan anh
            var path = $"wwwroot\\Images\\Products\\{namefolder}\\{ name}";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public void DeleteFolder(string name)
        {
            // lien quan den folder co file thi phai su dung Thread
            Thread.Sleep(0);
            Directory.Delete(name, true);
        }
    }
}
