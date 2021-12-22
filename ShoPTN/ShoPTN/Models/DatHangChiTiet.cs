using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("DatHang_ChiTiet")]
    public partial class DatHangChiTiet
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("DatHang_ID")]
        [Display(Name ="Code Đặt Hàng")]
        public int DatHangId { get; set; }

        [Column("LapTop_ID")]
        [Display(Name = "Sản phẩm")]
        public int LapTopId { get; set; }

        [Display(Name = "Số lượng")]
        public short? SoLuong { get; set; }

        [Display(Name = "Đơn giá")]
        public int? DonGia { get; set; }

        [Display(Name = "Thành tiền")]
        public int? ThanhTien { get; set; }


        [ForeignKey(nameof(DatHangId))]
        [InverseProperty("DatHangChiTiets")]
        public virtual DatHang DatHang { get; set; }
        [ForeignKey(nameof(LapTopId))]
        [InverseProperty(nameof(SanPham.DatHangChiTiets))]
        public virtual SanPham LapTop { get; set; }
    }
}
