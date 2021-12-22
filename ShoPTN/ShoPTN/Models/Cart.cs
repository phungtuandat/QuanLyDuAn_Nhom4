using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShoPTN.Models
{
    public class Cart
    {
        public SanPham Products { get; set; }
        public int Quantity { get; set; }
    }
}
