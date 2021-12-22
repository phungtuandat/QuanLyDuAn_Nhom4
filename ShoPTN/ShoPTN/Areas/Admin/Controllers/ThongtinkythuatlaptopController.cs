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
    public class ThongtinkythuatlaptopController : Controller
    {
        private readonly BanLapTopContext _context;

        public ThongtinkythuatlaptopController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: Thongtinkythuatlaptops
        public async Task<IActionResult> Index()
        {
            var banLapTopContext = _context.Thongtinkythuatlaptops.Include(t => t.IdProductNavigation);
            return View(await banLapTopContext.ToListAsync());
        }

        // GET: Thongtinkythuatlaptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtinkythuatlaptop = await _context.Thongtinkythuatlaptops
                .Include(t => t.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdThongTin == id);
            if (thongtinkythuatlaptop == null)
            {
                return NotFound();
            }

            return View(thongtinkythuatlaptop);
        }

        // GET: Thongtinkythuatlaptops/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham");
            return View();
        }

        // POST: Thongtinkythuatlaptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdThongTin,IdProduct,Cpu,Ram,ManHinh,Gpu,Rom,KichThuoc,XuatXu,NamRaMat")] Thongtinkythuatlaptop thongtinkythuatlaptop)
        {
            ViewData["IdProduct"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham", thongtinkythuatlaptop.IdProduct);
            if (ModelState.IsValid)
            {
                _context.Add(thongtinkythuatlaptop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thongtinkythuatlaptop);
        }

        // GET: Thongtinkythuatlaptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtinkythuatlaptop = await _context.Thongtinkythuatlaptops.FindAsync(id);
            if (thongtinkythuatlaptop == null)
            {
                return NotFound();
            }
            ViewData["IdProduct"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham", thongtinkythuatlaptop.IdProduct);
            return View(thongtinkythuatlaptop);
        }

        // POST: Thongtinkythuatlaptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdThongTin,IdProduct,Cpu,Ram,ManHinh,Gpu,Rom,KichThuoc,XuatXu,NamRaMat")] Thongtinkythuatlaptop thongtinkythuatlaptop)
        {
            ViewData["IdProduct"] = new SelectList(_context.SanPhams, "IdProduct", "TenSanPham", thongtinkythuatlaptop.IdProduct);
            if (id != thongtinkythuatlaptop.IdThongTin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongtinkythuatlaptop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongtinkythuatlaptopExists(thongtinkythuatlaptop.IdThongTin))
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
            return View(thongtinkythuatlaptop);
        }

        // GET: Thongtinkythuatlaptops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtinkythuatlaptop = await _context.Thongtinkythuatlaptops
                .Include(t => t.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdThongTin == id);
            if (thongtinkythuatlaptop == null)
            {
                return NotFound();
            }

            return View(thongtinkythuatlaptop);
        }

        // POST: Thongtinkythuatlaptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thongtinkythuatlaptop = await _context.Thongtinkythuatlaptops.FindAsync(id);
            _context.Thongtinkythuatlaptops.Remove(thongtinkythuatlaptop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongtinkythuatlaptopExists(int id)
        {
            return _context.Thongtinkythuatlaptops.Any(e => e.IdThongTin == id);
        }
    }
}
