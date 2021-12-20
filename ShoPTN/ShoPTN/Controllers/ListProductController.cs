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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace ShoPTN.Controllers
{
    public class ListProductController : Controller
    {
        private readonly ILogger<ListProductController> _logger;
        private readonly BanLapTopContext _context;

        public ListProductController(ILogger<ListProductController> logger, BanLapTopContext db)
        {
            _logger = logger;
            _context = db;
        }

        public IActionResult ProductHightLights()
        {
            // return về trang Có Action Index
            // TRẢ VỀ DANH SÁCH GIỎ HÀNG
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            return View(_context.SanPhams.Where(m => m.ProductHighlights == 1).ToList());
        }

        

        public IActionResult Index()
        {
            return View(_context.SanPhams.ToList());
        }

        [HttpPost]
        public IActionResult FitterProduct(int IdHangsx, int LoaiSanPham,int TinhTrang)
        {
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            if (IdHangsx == 0 && LoaiSanPham ==0 && TinhTrang == 0)
            {
                return View();
            }
            else
            {
                if(IdHangsx != 0 && LoaiSanPham != 0 && TinhTrang != 0)
                {
                    // trả về trang VỚI  ACTION
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.CateChild == LoaiSanPham && m.HangSxId == IdHangsx && m.ProductHighlights == 1).ToList();
                    return View(t);
                }
                else if(IdHangsx != 0 && LoaiSanPham == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.ProductHighlights == 1).ToList();
                    return View(t);
                }
                else if(LoaiSanPham != 0 && IdHangsx == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.CateChild == LoaiSanPham && m.ProductHighlights == 1).ToList();
                    return View(t);
                }
                else if(LoaiSanPham == 0 && IdHangsx == 0 && TinhTrang !=0)
                {
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.ProductHighlights == 1).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && LoaiSanPham == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.IdTinhTrang == TinhTrang && m.ProductHighlights == 1).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && LoaiSanPham != 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == LoaiSanPham && m.ProductHighlights == 1).ToList();
                    return View(t);
                }
                else if (IdHangsx == 0 && LoaiSanPham != 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == LoaiSanPham && m.ProductHighlights == 1).ToList();
                    return View(t);
                }
                else
                {
                    var t = _context.SanPhams.Where(m => m.CateChild == LoaiSanPham && m.IdTinhTrang == TinhTrang && m.ProductHighlights == 1).ToList();
                    return View(t);
                }
            }
        }

        public IActionResult ProductNew()
        {
            // return về trang Có Action Index
            // TRẢ VỀ DANH SÁCH GIỎ HÀNG
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            return View(_context.SanPhams.Where(m => m.ProductNew == 1).ToList());
        }

        [HttpPost]
        public IActionResult FitterProductNew(int IdHangsx, int LoaiSanPham, int TinhTrang)
        {
            ViewBag.HangSx = _context.HangSxes.ToList();
            ViewBag.CateChild = _context.DanhMucCons.ToList();
            ViewBag.TinhTrang = _context.TinhTrangs.ToList();
            if (IdHangsx == 0 && LoaiSanPham == 0 && TinhTrang == 0)
            {
                return View();
            }
            else
            {
                if (IdHangsx != 0 && LoaiSanPham != 0 && TinhTrang != 0)
                {
                    // trả về trang VỚI  ACTION
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang && m.CateChild == LoaiSanPham && m.HangSxId == IdHangsx ).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && LoaiSanPham == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx ).ToList();
                    return View(t);
                }
                else if (LoaiSanPham != 0 && IdHangsx == 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.CateChild == LoaiSanPham ).ToList();
                    return View(t);
                }
                else if (LoaiSanPham == 0 && IdHangsx == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.IdTinhTrang == TinhTrang ).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && LoaiSanPham == 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.IdTinhTrang == TinhTrang ).ToList();
                    return View(t);
                }
                else if (IdHangsx != 0 && LoaiSanPham != 0 && TinhTrang == 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == LoaiSanPham ).ToList();
                    return View(t);
                }
                else if (IdHangsx == 0 && LoaiSanPham != 0 && TinhTrang != 0)
                {
                    var t = _context.SanPhams.Where(m => m.HangSxId == IdHangsx && m.CateChild == LoaiSanPham ).ToList();
                    return View(t);
                }
                else
                {
                    var t = _context.SanPhams.Where(m => m.CateChild == LoaiSanPham && m.IdTinhTrang == TinhTrang ).ToList();
                    return View(t);
                }
            }
        }
    }
}
