using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("SanPham")]
    public partial class SanPham
    {
        public SanPham()
        {
            Comments = new HashSet<Comment>();
            DatHangChiTiets = new HashSet<DatHangChiTiet>();
            ImagesProducts = new HashSet<ImagesProduct>();
            Thongtinkythuatlaptops = new HashSet<Thongtinkythuatlaptop>();
        }

        [Key]
        [Column("ID_Product")]
        [Display(Name ="Sản phẩm")]
        public int IdProduct { get; set; }

        [Column("HangSX_ID")]
        [Display(Name = "Hãng sản xuất")]
        public int HangSxId { get; set; }

        [Column("CateChild")]
        [Display(Name = "Danh mục")]
        public int CateChild { get; set; }

        [Column("Id_NhanVien")]
        [Display(Name = "Nhân viên")]
        public int IdNhanVien { get; set; }

        [Column("Id_TinhTrang")]
        [Display(Name = "Tình trạng")]
        public int IdTinhTrang { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Giá bán không bỏ trống")]
        public int GiaBan { get; set; }

        [Display(Name = "Giá gốc")]
        public int? GiaKhuyenMai { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Column("out_of_sock")]
        [Display(Name = "Tình trạng")]
        public short OutOfSock { get; set; }

        [Display(Name = "Trạng thái")]
        public short TrangThai { get; set; }

        [Display(Name = "Mới")]
        public short? ProductNew { get; set; }

        [Display(Name = "Nổi bật")]
        public short? ProductHighlights { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Số lượng không bỏ trống")]
        public int? SoLuong { get; set; }

        [Display(Name = "Thư mục")]
        [Column("Folder_Name")]
        [StringLength(255)]
        public string FolderName { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không bỏ trống")]
        public string TenSanPham { get; set; }


        [Display(Name = "Danh mục")]
        [ForeignKey(nameof(CateChild))]
        [InverseProperty(nameof(DanhMucCon.SanPhams))]
        public virtual DanhMucCon CateChildNavigation { get; set; }

        [Display(Name = "Hãng")]
        [ForeignKey(nameof(HangSxId))]
        [InverseProperty("SanPhams")]
        public virtual HangSx HangSx { get; set; }

        [Display(Name = "Nhân viên")]
        [ForeignKey(nameof(IdNhanVien))]
        [InverseProperty(nameof(NhanVien.SanPhams))]
        public virtual NhanVien IdNhanVienNavigation { get; set; }

        [Display(Name = "Tình trạng")]
        [ForeignKey(nameof(IdTinhTrang))]
        [InverseProperty(nameof(TinhTrang.SanPhams))]
        public virtual TinhTrang IdTinhTrangNavigation { get; set; }

        [InverseProperty(nameof(Comment.Product))]
        public virtual ICollection<Comment> Comments { get; set; }

        [InverseProperty(nameof(DatHangChiTiet.LapTop))]
        public virtual ICollection<DatHangChiTiet> DatHangChiTiets { get; set; }

        [InverseProperty(nameof(ImagesProduct.IdProductNavigation))]
        public virtual ICollection<ImagesProduct> ImagesProducts { get; set; }

        [InverseProperty(nameof(Thongtinkythuatlaptop.IdProductNavigation))]
        public virtual ICollection<Thongtinkythuatlaptop> Thongtinkythuatlaptops { get; set; }
    }
}
