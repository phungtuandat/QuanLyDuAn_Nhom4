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
    public class DanhMucConsController : Controller
    {
        private readonly BanLapTopContext _context;

        public DanhMucConsController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: Admin/DanhMucCons
        public async Task<IActionResult> Index()
        {
            var banLapTopContext = _context.DanhMucCons.Include(d => d.IdDanhMucSanPhamNavigation);
            return View(await banLapTopContext.ToListAsync());
        }

        // GET: Admin/DanhMucCons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucCon = await _context.DanhMucCons
                .Include(d => d.IdDanhMucSanPhamNavigation)
                .FirstOrDefaultAsync(m => m.CatelogyChild == id);
            if (danhMucCon == null)
            {
                return NotFound();
            }

            return View(danhMucCon);
        }

        // GET: Admin/DanhMucCons/Create
        public IActionResult Create()
        {
            ViewData["IdDanhMucSanPham"] = new SelectList(_context.DanhMucSanPhams.Where(m=>m.TinhTrang == 2), "IdDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: Admin/DanhMucCons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatelogyChild,IdDanhMucSanPham,TenDanhMuc,TinhTrang,ChuThich")] DanhMucCon danhMucCon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucCon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDanhMucSanPham"] = new SelectList(_context.DanhMucSanPhams.Where(m => m.TinhTrang == 2), "IdDanhMuc", "TenDanhMuc");
            return View(danhMucCon);
        }

        // GET: Admin/DanhMucCons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucCon = await _context.DanhMucCons.FindAsync(id);
            if (danhMucCon == null)
            {
                return NotFound();
            }
            ViewData["IdDanhMucSanPham"] = new SelectList(_context.DanhMucSanPhams.Where(m => m.TinhTrang == 2), "IdDanhMuc", "TenDanhMuc");
            return View(danhMucCon);
        }

        // POST: Admin/DanhMucCons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatelogyChild,IdDanhMucSanPham,TenDanhMuc,TinhTrang,ChuThich")] DanhMucCon danhMucCon)
        {
            if (id != danhMucCon.CatelogyChild)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMucCon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucConExists(danhMucCon.CatelogyChild))
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
            ViewData["IdDanhMucSanPham"] = new SelectList(_context.DanhMucSanPhams.Where(m => m.TinhTrang == 2), "IdDanhMuc", "TenDanhMuc");
            return View(danhMucCon);
        }

        // GET: Admin/DanhMucCons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucCon = await _context.DanhMucCons
                .Include(d => d.IdDanhMucSanPhamNavigation)
                .FirstOrDefaultAsync(m => m.CatelogyChild == id);
            if (danhMucCon == null)
            {
                return NotFound();
            }

            return View(danhMucCon);
        }

        // POST: Admin/DanhMucCons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMucCon = await _context.DanhMucCons.FindAsync(id);
            _context.DanhMucCons.Remove(danhMucCon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucConExists(int id)
        {
            return _context.DanhMucCons.Any(e => e.CatelogyChild == id);
        }
    }
}
