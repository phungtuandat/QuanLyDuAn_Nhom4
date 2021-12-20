using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("comment")]
    public partial class Comment
    {
        [Key]
        [Column("Id_Commnet")]
        public int IdCommnet { get; set; }

        [Column("Customer_ID")]
        [Display(Name ="Người dùng")]
        public int CustomerId { get; set; }

        [Column("Product_Id")]
        [Display(Name ="Sản phẩm")]
        public int ProductId { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Ngày đăng")]
        public DateTime NgayDang { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage ="Vui lòng nhập nội dung")]
        public string NoiDung { get; set; }

        [Display(Name = "Tình trạng")]
        public short TinhTrang { get; set; }

        [Column("Images_Post")]
        [Display(Name = "Hình ảnh")]
        public string ImagesPost { get; set; }

        [Display(Name = "Khách hàng")]
        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(KhachHang.Comments))]
        public virtual KhachHang Customer { get; set; }

        [Display(Name = "Sản phẩm")]
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(SanPham.Comments))]
        public virtual SanPham Product { get; set; }
    }
}
