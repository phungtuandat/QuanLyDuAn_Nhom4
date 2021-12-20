using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("DatHang")]
    public partial class DatHang
    {
        public DatHang()
        {
            DatHangChiTiets = new HashSet<DatHangChiTiet>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("KhachHang_ID")]
        [Display(Name = "Khách hàng")]
        public int KhachHangId { get; set; }

        [StringLength(20)]
        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string DienThoaiGiaoHang { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
        public string DiaChiGiaoHang { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Ngày đặt")]
        public DateTime? NgayDatHang { get; set; }

        [Display(Name = "Tình trạng")]
        public short? TinhTrang { get; set; }

        [Display(Name = "Khách hàng")]
        [ForeignKey(nameof(KhachHangId))]
        [InverseProperty("DatHangs")]
        public virtual KhachHang KhachHang { get; set; }
        [InverseProperty(nameof(DatHangChiTiet.DatHang))]
        public virtual ICollection<DatHangChiTiet> DatHangChiTiets { get; set; }
    }
}
