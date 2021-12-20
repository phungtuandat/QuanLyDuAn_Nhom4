using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("DanhMucCon")]
    public partial class DanhMucCon
    {
        public DanhMucCon()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public int CatelogyChild { get; set; }
        [Column("Id_DanhMucSanPham")]
        [Display(Name = "Danh mục sản phẩm")]
        public int IdDanhMucSanPham { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(100)]
        public string TenDanhMuc { get; set; }

        [Display(Name = "Tình trạng")]
        public short TinhTrang { get; set; }

        [StringLength(255)]
        [Display(Name = "Chú thích")]
        public string ChuThich { get; set; }

        [Display(Name = "Danh mục")]
        [ForeignKey(nameof(IdDanhMucSanPham))]
        [InverseProperty(nameof(DanhMucSanPham.DanhMucCons))]
        public virtual DanhMucSanPham IdDanhMucSanPhamNavigation { get; set; }
        [InverseProperty(nameof(SanPham.CateChildNavigation))]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
