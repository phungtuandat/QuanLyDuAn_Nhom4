using System;
using System.Collections.Generic;
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
    public class DatHangController : Controller
    {
        private readonly BanLapTopContext _context;

        public DatHangController(BanLapTopContext context)
        {
            _context = context;
        }

        // GET: DatHang
        public async Task<IActionResult> Index()
        {
            var banLapTopContext = _context.DatHangs.Include(d => d.KhachHang);
            return View(await banLapTopContext.ToListAsync());
        }

        // GET: DatHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHangs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datHang == null)
            {
                return NotFound();
            }

            return View(datHang);
        }

        // GET: DatHang/Create
        public IActionResult Create()
        {
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "IdCustomer", "HoVaTen");
            return View();
        }

        // POST: DatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KhachHangId,DienThoaiGiaoHang,DiaChiGiaoHang,NgayDatHang,TinhTrang,TongTien")] DatHang datHang)
        {
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "IdCustomer", "HoVaTen", datHang.KhachHangId);
            if (ModelState.IsValid)
            {
                datHang.NgayDatHang = DateTime.Now;
                _context.Add(datHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(datHang);
        }

        // GET: DatHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHangs.FindAsync(id);
            if (datHang == null)
            {
                return NotFound();
            }
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "IdCustomer", "HoVaTen", datHang.KhachHangId);
            return View(datHang);
        }

        // POST: DatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KhachHangId,DienThoaiGiaoHang,DiaChiGiaoHang,NgayDatHang,TinhTrang,TongTien")] DatHang datHang)
        {
            if (id != datHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatHangExists(datHang.Id))
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
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "IdCustomer", "HoVaTen", datHang.KhachHangId);
            return View(datHang);
        }

        // GET: DatHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHangs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datHang == null)
            {
                return NotFound();
            }

            return View(datHang);
        }

        // POST: DatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datHang = await _context.DatHangs.FindAsync(id);
            _context.DatHangs.Remove(datHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatHangExists(int id)
        {
            return _context.DatHangs.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ChiTietDonHang(int id)
        {
            ViewBag.NameCustomer = _context.DatHangs.Where(m => m.Id == id).Include(m=>m.KhachHang).FirstOrDefault();
            var banLapTopContext = _context.DatHangChiTiets.Where(m=>m.DatHangId == id).Include(d => d.DatHang).Include(d => d.LapTop);
            return View(await banLapTopContext.ToListAsync());
        }
    }
}
