using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("NhanVien")]
    [Index(nameof(TenDangNhap), Name = "UQ__NhanVien__55F68FC04AEBFDC5", IsUnique = true)]
    public partial class NhanVien
    {
        public NhanVien()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [Column("ID_Staff")]
        public int IdStaff { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập họ và tên")]
        [StringLength(100)]
        [Display(Name ="Họ và tên")]
        public string HoVaTen { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        [Display(Name = "Điện thoại")]
        public string DienThoai { get; set; }

        [StringLength(225)]
        [Display(Name ="Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        [Display(Name ="Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [StringLength(255)]
        [Display(Name = "Ảnh đại diện")]
        public string Avartar { get; set; }

        [Display(Name = "Quyền")]
        public bool? Quyen { get; set; }

        [InverseProperty(nameof(SanPham.IdNhanVienNavigation))]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }

    public class NhanVienLogin
    {
        [StringLength(50)]
        public string TenDangNhap { get; set; }
        [StringLength(255)]
        public string MatKhau { get; set; }
    }
}
