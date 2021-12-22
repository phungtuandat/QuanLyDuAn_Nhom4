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
    public class HangSxController : Controller
    {
        private readonly BanLapTopContext _context;

        public HangSxController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: HangSx
        public async Task<IActionResult> Index()
        {
            return View(await _context.HangSxes.ToListAsync());
        }

        // GET: HangSx/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangSx = await _context.HangSxes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hangSx == null)
            {
                return NotFound();
            }

            return View(hangSx);
        }

        // GET: HangSx/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HangSx/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenHang,ChuThich,TinhTrang")] HangSx hangSx)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hangSx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hangSx);
        }

        // GET: HangSx/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangSx = await _context.HangSxes.FindAsync(id);
            if (hangSx == null)
            {
                return NotFound();
            }
            return View(hangSx);
        }

        // POST: HangSx/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenHang,ChuThich,TinhTrang")] HangSx hangSx)
        {
            if (id != hangSx.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangSx);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangSxExists(hangSx.Id))
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
            return View(hangSx);
        }

        // GET: HangSx/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangSx = await _context.HangSxes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hangSx == null)
            {
                return NotFound();
            }

            return View(hangSx);
        }

        // POST: HangSx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangSx = await _context.HangSxes.FindAsync(id);
            _context.HangSxes.Remove(hangSx);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangSxExists(int id)
        {
            return _context.HangSxes.Any(e => e.Id == id);
        }
    }
}
