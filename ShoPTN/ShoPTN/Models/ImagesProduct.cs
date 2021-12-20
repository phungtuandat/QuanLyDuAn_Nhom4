using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("Images_Product")]
    public partial class ImagesProduct
    {
        [Key]
        [Column("Id_Images")]
        public int IdImages { get; set; }

        [Column("Id_Product")]
        public int IdProduct { get; set; }
        [StringLength(255)]

        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }

        [Column("FOLDER")]
        [StringLength(255)]
        [Display(Name = "Thư mục")]
        public string Folder { get; set; }

        [Display(Name = "Sản phẩm")]
        [ForeignKey(nameof(IdProduct))]
        [InverseProperty(nameof(SanPham.ImagesProducts))]
        public virtual SanPham IdProductNavigation { get; set; }
    }
}
