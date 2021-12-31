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
    public class CommentsController : Controller
    {
        private readonly BanLapTopContext _context;

        public CommentsController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var banLapTopContext = _context.Comments.Include(c => c.Customer).Include(c => c.Product);
            return View(await banLapTopContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.IdCommnet == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.KhachHangs, "IdCustomer", "HoVaTen");
            ViewData["ProductId"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham");
            ModelState.AddModelError("ErrorExit", "");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file,[Bind("IdCommnet,CustomerId,ProductId,NgayDang,TinhTrang,ImagesPost,NoiDung")] Comment comment)
        {
            ViewData["CustomerId"] = new SelectList(_context.KhachHangs, "IdCustomer", "HoVaTen", comment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham", comment.ProductId);
            if (ModelState.IsValid)
            {
                if(file!=null)
                {
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(comment);
                    }
                    else if (file.ContentType.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(comment);
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorExit", "");
                        comment.NgayDang = DateTime.Now;
                        comment.ImagesPost = Upload(file);
                        // nhân viên sẻ tạo tài khoản khách hàng được
                        /* 0 là mới đăng chưa xét duyệt , 1 đã xét duyệt , 2 xóa bình luận
                        comment.TinhTrang = 0;*/
                    }

                }
                else
                {
                    comment.ImagesPost = "NoImage.jpg";
                }                
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.KhachHangs, "IdCustomer", "HoVaTen", comment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham", comment.ProductId);
            ModelState.AddModelError("ErrorExit", "");
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("IdCommnet,CustomerId,ProductId,NgayDang,TinhTrang,ImagesPost,NoiDung")] Comment comment)
        {
            if (id != comment.IdCommnet)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        string fileExtension = Path.GetExtension(file.FileName).ToLower();
                        if (!fileTypeSupported.Contains(fileExtension))
                        {
                            ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                            return View(comment);
                        }
                        else if (file.ContentType.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("ErrorExit", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                            return View(comment);
                        }
                        else
                        {
                            ModelState.AddModelError("ErrorExit", "");
                            comment.NgayDang = DateTime.Now;
                            DeleteImages(comment.ImagesPost);
                            comment.ImagesPost = Upload(file);
                            // nhân viên sẻ tạo tài khoản khách hàng được
                            /* 0 là mới đăng chưa xét duyệt , 1 đã xét duyệt , 2 xóa bình luận
                            comment.TinhTrang = 0;*/
                        }
                    }
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.IdCommnet))
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
            ViewData["CustomerId"] = new SelectList(_context.KhachHangs, "IdCustomer", "HoVaTen", comment.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.SanPhams, "IdProduct", "", comment.ProductId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.IdCommnet == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            DeleteImages(comment.ImagesPost);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.IdCommnet == id);
        }

        public string Upload(IFormFile file)
        {
            //string error = "";        
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Comment", UploadFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return UploadFileName;
        }


        public void DeleteImages(string name)
        {
            if(name != null)
            {
                // lay duong dan anh
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Comment", name);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
    }
}
