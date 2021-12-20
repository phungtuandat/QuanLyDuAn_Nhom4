using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoPTN.Models
{
    [Table("Infomation_Techology")]
    public partial class InfomationTechology
    {
        [Key]
        [Column("Id_BaiViet")]
        public int IdBaiViet { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Ngày đăng")]
        public DateTime NgayDang { get; set; }

        [Display(Name = "Lượt xem")]
        public int? LuotXem { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập nội dung")]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        [StringLength(255)]
        [Display(Name = "Ảnh bìa")]
        public string Avatar { get; set; }

        [Display(Name = "Tình trạng")]
        public short? TinhTrang { get; set; }
    }
}
