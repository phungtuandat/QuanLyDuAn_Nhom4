using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("HangSX")]
    public partial class HangSx
    {
        public HangSx()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }


        [StringLength(100)]
        [Display(Name = "Tên hãng")]
        [Required(ErrorMessage = "Tên hãng không được bỏ trống!")]
        public string TenHang { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "Chú thích")]
        public string ChuThich { get; set; }

        [Display(Name = "Tình trạng")]
        public short TinhTrang { get; set; }

        [InverseProperty(nameof(SanPham.HangSx))]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
