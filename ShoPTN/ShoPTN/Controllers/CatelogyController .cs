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
    public class CatelogyController : Controller
    {
        private readonly ILogger<CatelogyController> _logger;
        private readonly BanLapTopContext _context;

        public CatelogyController(ILogger<CatelogyController> logger, BanLapTopContext db)
        {
            _logger = logger;
            _context = db;
        }

        public PartialViewResult ListProduct()
        {
            ViewBag.Catelogy = _context.DanhMucSanPhams.ToList();
            ViewBag.CatelogyChild = _context.DanhMucCons.ToList();
            return PartialView(_context.DanhMucSanPhams.Include(m => m.DanhMucCons).ToList());
        }
    }
}
