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
    public class InfomationTechologiesController : Controller
    {
        private readonly BanLapTopContext _context;

        public InfomationTechologiesController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: InfomationTechologies
        public async Task<IActionResult> Index()
        {
            return View(await _context.InfomationTechologies.ToListAsync());
        }

        // GET: InfomationTechologies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infomationTechology = await _context.InfomationTechologies
                .FirstOrDefaultAsync(m => m.IdBaiViet == id);
            if (infomationTechology == null)
            {
                return NotFound();
            }

            return View(infomationTechology);
        }

        // GET: InfomationTechologies/Create
        public IActionResult Create()
        {
            ModelState.AddModelError("ErrorExit", "");
            return View();
        }

        // POST: InfomationTechologies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file,[Bind("IdBaiViet,Title,NgayDang,LuotXem,NoiDung,Avatar,TinhTrang")] InfomationTechology infomationTechology)
        {
            if (ModelState.IsValid)
            {
                infomationTechology.LuotXem = 0;
                infomationTechology.NgayDang = DateTime.Now;
                //2 hiện 1 ẩn:
                infomationTechology.TinhTrang = 2;
                if (file != null)
                {
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(infomationTechology);
                    }
                    else if (file.ContentType.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(infomationTechology);
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorExit", "");
                        infomationTechology.Avatar = Upload(file);
                        // nhân viên sẻ tạo tài khoản khách hàng được
                        /* 0 là mới đăng chưa xét duyệt , 1 đã xét duyệt , 2 xóa bình luận
                        comment.TinhTrang = 0;*/
                    }

                }
                else
                {
                    ModelState.AddModelError("ErrorExit", "Vui lòng chọn hình ảnh");
                    return View(infomationTechology);
                }
                _context.Add(infomationTechology);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(infomationTechology);
        }

        // GET: InfomationTechologies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infomationTechology = await _context.InfomationTechologies.FindAsync(id);
            if (infomationTechology == null)
            {
                return NotFound();
            }
            ModelState.AddModelError("ErrorExit", "");
            return View(infomationTechology);
        }

        // POST: InfomationTechologies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("IdBaiViet,Title,NgayDang,LuotXem,NoiDung,Avatar,TinhTrang")] InfomationTechology infomationTechology)
        {
            if (id != infomationTechology.IdBaiViet)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (file != null)
                        {
                            var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                            string fileExtension = Path.GetExtension(file.FileName).ToLower();
                            if (!fileTypeSupported.Contains(fileExtension))
                            {
                                ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                                return View(infomationTechology);
                            }
                            else if (file.ContentType.Length > 2 * 1024 * 1024)
                            {
                                ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                                return View(infomationTechology);
                            }
                            else
                            {
                                ModelState.AddModelError("ErrorExit", "");
                                DeleteImages(infomationTechology.Avatar);
                                infomationTechology.Avatar = Upload(file);
                                // nhân viên sẻ tạo tài khoản khách hàng được
                                /* 0 là mới đăng chưa xét duyệt , 1 đã xét duyệt , 2 xóa bình luận
                                comment.TinhTrang = 0;*/
                            }
                        }
                    }
                    _context.Update(infomationTechology);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfomationTechologyExists(infomationTechology.IdBaiViet))
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
            return View(infomationTechology);
        }

        // GET: InfomationTechologies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infomationTechology = await _context.InfomationTechologies
                .FirstOrDefaultAsync(m => m.IdBaiViet == id);
            if (infomationTechology == null)
            {
                return NotFound();
            }

            return View(infomationTechology);
        }

        // POST: InfomationTechologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var infomationTechology = await _context.InfomationTechologies.FindAsync(id);
            DeleteImages(infomationTechology.Avatar);
            _context.InfomationTechologies.Remove(infomationTechology);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfomationTechologyExists(int id)
        {
            return _context.InfomationTechologies.Any(e => e.IdBaiViet == id);
        }

        public string Upload(IFormFile file)
        {
            //string error = "";        
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "BaiViet", UploadFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return UploadFileName;
        }


        public void DeleteImages(string name)
        {
            if (name != null)
            {
                // lay duong dan anh
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "BaiViet", name);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
    }
}
