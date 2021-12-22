using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("thongtinkythuatlaptop")]
    public partial class Thongtinkythuatlaptop
    {
        [Key]
        [Column("Id_ThongTin")]
        public int IdThongTin { get; set; }

        [Display(Name ="Sản phẩm")]
        [Column("ID_Product")]
        public int IdProduct { get; set; }

        [Display(Name = "CPU")]
        [Column("CPU")]
        [StringLength(255)]
        [Required(ErrorMessage ="Vui lòng nhập đầy đủ thông tin")]
        public string Cpu { get; set; }

        [Display(Name = "Ram")]
        [Column("Ram")]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string Ram { get; set; }

        [Display(Name = "Màn hình")]
        [Column("ManHinh")]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string ManHinh { get; set; }

        [Display(Name = "GPU")]
        [Column("GPU")]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string Gpu { get; set; }

        [Display(Name = "Rom")]
        [Column("Rom")]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string Rom { get; set; }

        [Display(Name = "Kích thước")]
        [StringLength(255)]
        [Column("KichThuoc")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string KichThuoc { get; set; }

        [Display(Name = "Xuất xứ")]
        [Column("XuatXu")]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string XuatXu { get; set; }

        [Display(Name = "Năm ra mắt")]
        [Column("NamRaMat")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public int? NamRaMat { get; set; }

        [Display(Name = "Sản phẩm")]
        [ForeignKey(nameof(IdProduct))]
        [InverseProperty(nameof(SanPham.Thongtinkythuatlaptops))]
        public virtual SanPham IdProductNavigation { get; set; }
    }
}
