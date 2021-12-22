using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("TinhTrang")]
    public partial class TinhTrang
    {
        public TinhTrang()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [Column("MaTT")]
        public int MaTt { get; set; }

        [Display(Name ="Tên tình trạng")]
        [Required(ErrorMessage ="Vui lòng nhập tên tình trạng")]
        [Column("TenTT")]
        [StringLength(255)]
        public string TenTt { get; set; }

        [Display(Name = "Chú thích")]
        [StringLength(255)]
        public string ChuTich { get; set; }

        [InverseProperty(nameof(SanPham.IdTinhTrangNavigation))]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
