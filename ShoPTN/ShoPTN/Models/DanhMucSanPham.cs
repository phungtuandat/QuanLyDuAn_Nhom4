using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("DanhMucSanPham")]
    public partial class DanhMucSanPham
    {
        public DanhMucSanPham()
        {
            DanhMucCons = new HashSet<DanhMucCon>();
        }

        [Key]
        [Column("Id_DanhMuc")]
        public int IdDanhMuc { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        public string TenDanhMuc { get; set; }

        [Display(Name = "Tình trạng")]
        public short TinhTrang { get; set; }

        [Display(Name = "Danh mục")]
        [InverseProperty(nameof(DanhMucCon.IdDanhMucSanPhamNavigation))]
        public virtual ICollection<DanhMucCon> DanhMucCons { get; set; }
    }
}
