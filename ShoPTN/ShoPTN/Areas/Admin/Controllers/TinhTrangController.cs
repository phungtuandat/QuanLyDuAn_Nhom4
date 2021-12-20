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
    public class TinhTrangController : Controller
    {
        private readonly BanLapTopContext _context;

        public TinhTrangController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: TinhTrang
        public async Task<IActionResult> Index()
        {
            return View(await _context.TinhTrangs.ToListAsync());
        }

        // GET: TinhTrang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinhTrang = await _context.TinhTrangs
                .FirstOrDefaultAsync(m => m.MaTt == id);
            if (tinhTrang == null)
            {
                return NotFound();
            }

            return View(tinhTrang);
        }

        // GET: TinhTrang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TinhTrang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTt,TenTt,ChuTich")] TinhTrang tinhTrang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tinhTrang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tinhTrang);
        }

        // GET: TinhTrang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinhTrang = await _context.TinhTrangs.FindAsync(id);
            if (tinhTrang == null)
            {
                return NotFound();
            }
            return View(tinhTrang);
        }

        // POST: TinhTrang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTt,TenTt,ChuTich")] TinhTrang tinhTrang)
        {
            if (id != tinhTrang.MaTt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tinhTrang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinhTrangExists(tinhTrang.MaTt))
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
            return View(tinhTrang);
        }

        // GET: TinhTrang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinhTrang = await _context.TinhTrangs
                .FirstOrDefaultAsync(m => m.MaTt == id);
            if (tinhTrang == null)
            {
                return NotFound();
            }

            return View(tinhTrang);
        }

        // POST: TinhTrang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinhTrang = await _context.TinhTrangs.FindAsync(id);
            _context.TinhTrangs.Remove(tinhTrang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinhTrangExists(int id)
        {
            return _context.TinhTrangs.Any(e => e.MaTt == id);
        }
    }
}
