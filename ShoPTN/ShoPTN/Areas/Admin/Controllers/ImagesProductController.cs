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
    public class ImagesProductController : Controller
    {
        private readonly BanLapTopContext _context;

        public ImagesProductController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: ImagesProduct
        public async Task<IActionResult> Index()
        {
            var banLapTopContext = _context.ImagesProducts.Include(i => i.IdProductNavigation);
            return View(await banLapTopContext.ToListAsync());
        }

        // GET: ImagesProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagesProduct = await _context.ImagesProducts
                .Include(i => i.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdImages == id);
            if (imagesProduct == null)
            {
                return NotFound();
            }

            return View(imagesProduct);
        }

        // GET: ImagesProduct/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham");
            ModelState.AddModelError("ErorrImages", "");
            return View();
        }

        // POST: ImagesProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("IdImages,IdProduct,HinhAnh,FOLDER")] ImagesProduct imagesProduct)
        {
            ViewData["IdProduct"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham", imagesProduct.IdProduct);
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ErorrImages", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(imagesProduct);
                    }
                    else if (file.ContentType.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ErorrImages", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(imagesProduct);
                    }
                    else
                    {
                        int i = 0;
                        int id_images = imagesProduct.IdImages;
                        var t = await _context.SanPhams.Where(m => m.IdProduct == imagesProduct.IdProduct).FirstOrDefaultAsync();
                        var banLapTopContext = _context.ImagesProducts.ToList();
                        foreach (var item in banLapTopContext)
                        {
                            if (imagesProduct.IdProduct == item.IdProduct)
                            {
                                imagesProduct.HinhAnh = UploadExits(file, item.Folder);
                                imagesProduct.Folder = item.Folder;
                                i = 1;
                            }
                            else i = 0;
                        }
                        if (i == 0)
                        {
                            ModelState.AddModelError("ErorrImages", "");
                            string name_foder_img = "Images_Prodduct" + "_" + imagesProduct.IdProduct;
                            imagesProduct.Folder = Create_Folder(t.FolderName, name_foder_img);
                            int id = imagesProduct.IdProduct;
                            imagesProduct.HinhAnh = Upload(file, t.FolderName, name_foder_img);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("ErorrImages", "Vui lòng chọn hình ảnh");
                    return View(imagesProduct);
                }
                _context.Add(imagesProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imagesProduct);
        }

        // GET: ImagesProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagesProduct = await _context.ImagesProducts.FindAsync(id);
            if (imagesProduct == null)
            {
                return NotFound();
            }
            ViewData["IdProduct"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham", imagesProduct.IdProduct);
            ModelState.AddModelError("ErorrImages", "");
            return View(imagesProduct);
        }

        // POST: ImagesProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("IdImages,IdProduct,HinhAnh,FOLDER")] ImagesProduct imagesProduct)
        {
            ViewData["IdProduct"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham", imagesProduct.IdProduct);
            if (id != imagesProduct.IdImages)
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
                            ModelState.AddModelError("ErorrImages", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                            return View(imagesProduct);
                        }
                        else if (file.ContentType.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("ErorrImages", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                            return View(imagesProduct);
                        }
                        else
                        {
                            var t = await _context.SanPhams.Where(m => m.IdProduct == imagesProduct.IdProduct).FirstOrDefaultAsync();
                            ModelState.AddModelError("ErorrImages", "");
                            string name_foder_img = "Images_Prodduct" + "_" + imagesProduct.IdProduct;
                            DeleteImages(imagesProduct.Folder, imagesProduct.HinhAnh);
                            imagesProduct.HinhAnh = Upload(file, t.FolderName, name_foder_img);
                        }
                        
                     }
                    _context.Update(imagesProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagesProductExists(imagesProduct.IdImages))
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
            return View(imagesProduct);
        }

        // GET: ImagesProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var imagesProduct = await _context.ImagesProducts
                .Include(i => i.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdImages == id);
            if (imagesProduct == null)
            {
                return NotFound();
            }

            return View(imagesProduct);
        }

        // POST: ImagesProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagesProduct = await _context.ImagesProducts.FindAsync(id);
            DeleteImages(imagesProduct.Folder, imagesProduct.HinhAnh);
            _context.ImagesProducts.Remove(imagesProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagesProductExists(int id)
        {
            return _context.ImagesProducts.Any(e => e.IdImages == id);
        }
        public string Create_Folder(string folder_product,string FolderName)
        {
            FolderName = $"{folder_product}//{FolderName}";
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
        public string Upload(IFormFile files,string folder_product, string foldername)
        {
            List<string> listname = new List<string>();
            //string error = "";        
            string UploadFileName = null;
            if (files != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                var path = $"{folder_product}\\{foldername}\\{UploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                files.CopyTo(stream);
                }
                listname.Add(UploadFileName);
            }
            return UploadFileName;
        }


        public string UploadExits(IFormFile files, string folder)
        {
            //string error = "";        
            string UploadFileName = null;
            if (files != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                var path = $"{folder}\\{UploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    files.CopyTo(stream);
                }
            }
            return UploadFileName;
        }

        public void DeleteImages(string folder,string name)
        {
            // lay duong dan anh
            var path = $"{folder}//{name}";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
