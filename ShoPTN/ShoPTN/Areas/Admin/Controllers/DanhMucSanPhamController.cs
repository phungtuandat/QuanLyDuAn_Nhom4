using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoPTN.Data;
using ShoPTN.Models;

namespace ShoPTN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucSanPhamController : Controller
    {
        private readonly BanLapTopContext _context;

        public DanhMucSanPhamController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: DanhMucSanPham
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhMucSanPhams.ToListAsync());
        }

        // GET: DanhMucSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPhams
                .FirstOrDefaultAsync(m => m.IdDanhMuc == id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            return View(danhMucSanPham);
        }

        // GET: DanhMucSanPham/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDanhMuc,TenDanhMuc,TinhTrang,ChuThich")] DanhMucSanPham danhMucSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucSanPham);
        }

        // GET: DanhMucSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPhams.FindAsync(id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }
            return View(danhMucSanPham);
        }

        // POST: DanhMucSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDanhMuc,TenDanhMuc,TinhTrang,ChuThich")] DanhMucSanPham danhMucSanPham)
        {
            if (id != danhMucSanPham.IdDanhMuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMucSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucSanPhamExists(danhMucSanPham.IdDanhMuc))
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
            return View(danhMucSanPham);
        }

        // GET: DanhMucSanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucSanPham = await _context.DanhMucSanPhams
                .FirstOrDefaultAsync(m => m.IdDanhMuc == id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            return View(danhMucSanPham);
        }

        // POST: DanhMucSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMucSanPham = await _context.DanhMucSanPhams.FindAsync(id);
            _context.DanhMucSanPhams.Remove(danhMucSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucSanPhamExists(int id)
        {
            return _context.DanhMucSanPhams.Any(e => e.IdDanhMuc == id);
        }
    }
}
