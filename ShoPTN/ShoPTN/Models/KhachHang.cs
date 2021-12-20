using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("KhachHang")]
    [Index(nameof(TenDangNhap), Name = "UQ__KhachHan__55F68FC0850B620F", IsUnique = true)]
    public partial class KhachHang
    {
        public KhachHang()
        {
            Comments = new HashSet<Comment>();
            DatHangs = new HashSet<DatHang>();
        }

        [Key]
        [Column("ID_Customer")]
        [Display(Name = "Khách hàng")]
        public int IdCustomer { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập họ và tên")]
        [StringLength(100)]
        public string HoVaTen { get; set; }

        [StringLength(20)]
        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string DienThoai { get; set; }

        [StringLength(225)]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [StringLength(255)]
        [Display(Name = "Ảnh đại diện")]
        public string Avartar { get; set; }


        [InverseProperty(nameof(Comment.Customer))]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty(nameof(DatHang.KhachHang))]
        public virtual ICollection<DatHang> DatHangs { get; set; }
    }

    public class Customer_Login
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
