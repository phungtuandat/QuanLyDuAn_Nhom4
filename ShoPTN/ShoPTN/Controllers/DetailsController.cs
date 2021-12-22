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
    public class DetailsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BanLapTopContext _context;

        public DetailsController(ILogger<HomeController> logger, BanLapTopContext db)
        {
            _logger = logger;
            _context = db;

        }

        public IActionResult ChiTiet(int id)
        {
            ViewBag.ProductList = _context.SanPhams.ToList();
            ViewBag.ListComment = _context.Comments.Include(m => m.Customer).ToList();
            return View(_context.SanPhams.Where(m=>m.IdProduct == id).Include(m=>m.ImagesProducts).Include(m=>m.Thongtinkythuatlaptops).Include(m => m.Comments).FirstOrDefault());
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(string noidung,IFormFile file,int id)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment();
                if (file != null)
                {
                    
                    comment.NgayDang = DateTime.Now;
                    comment.CustomerId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                    comment.ProductId = id;
                    comment.TinhTrang = 0;
                    comment.NoiDung = noidung;
                    comment.ImagesPost = Upload(file);
                    // nhân viên sẻ tạo tài khoản khách hàng được
                    /* 0 là mới đăng chưa xét duyệt , 1 đã xét duyệt , 2 xóa bình luận
                    comment.TinhTrang = 0;*/
                }
                else
                {
                    comment.NgayDang = DateTime.Now;
                    comment.CustomerId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                    comment.ProductId = id;
                    comment.TinhTrang = 0;
                    comment.NoiDung = noidung;

                    comment.ImagesPost = "NoImage.jpg";
                }
                _context.Add(comment);
                await _context.SaveChangesAsync();
                // action/controller/id
                return RedirectToAction("ChiTiet", "Details",new{ id = id });
            }
            return RedirectToAction("ChiTiet", "Details", new{ id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int Id_Comment, int Id_Product)
        {
            var t = _context.Comments.Where(m => m.IdCommnet == Id_Comment).FirstOrDefault();
            DeleteImages(t.ImagesPost);
            _context.Remove(t);
            await _context.SaveChangesAsync();
            return RedirectToAction("ChiTiet", "Details",new { id = Id_Product });
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
            if (name != null)
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
