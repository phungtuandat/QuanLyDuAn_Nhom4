using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShoPTN.Models;

#nullable disable

namespace ShoPTN.Data
{
    public partial class BanLapTopContext : DbContext
    {
        public BanLapTopContext()
        {
        }

        public BanLapTopContext(DbContextOptions<BanLapTopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DanhMucCon> DanhMucCons { get; set; }
        public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }
        public virtual DbSet<DatHang> DatHangs { get; set; }
        public virtual DbSet<DatHangChiTiet> DatHangChiTiets { get; set; }
        public virtual DbSet<HangSx> HangSxes { get; set; }
        public virtual DbSet<ImagesProduct> ImagesProducts { get; set; }
        public virtual DbSet<InfomationTechology> InfomationTechologies { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Thongtinkythuatlaptop> Thongtinkythuatlaptops { get; set; }
        public virtual DbSet<TinhTrang> TinhTrangs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=BanLapTop;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdCommnet)
                    .HasName("PK__comment__525962FAFDC0CF92");

                entity.Property(e => e.ImagesPost).IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__Custome__30F848ED");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__Product__31EC6D26");
            });

            modelBuilder.Entity<DanhMucCon>(entity =>
            {
                entity.HasKey(e => e.CatelogyChild)
                    .HasName("PK__DanhMucC__3FA25A48EEAD874B");

                entity.HasOne(d => d.IdDanhMucSanPhamNavigation)
                    .WithMany(p => p.DanhMucCons)
                    .HasForeignKey(d => d.IdDanhMucSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DanhMucCo__Id_Da__1A14E395");
            });

            modelBuilder.Entity<DanhMucSanPham>(entity =>
            {
                entity.HasKey(e => e.IdDanhMuc)
                    .HasName("PK__DanhMucS__9008DCCF6FF95E74");
            });

            modelBuilder.Entity<DatHang>(entity =>
            {
                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.DatHangs)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DatHang__KhachHa__2A4B4B5E");
            });

            modelBuilder.Entity<DatHangChiTiet>(entity =>
            {
                entity.HasOne(d => d.DatHang)
                    .WithMany(p => p.DatHangChiTiets)
                    .HasForeignKey(d => d.DatHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DatHang_C__DatHa__2D27B809");

                entity.HasOne(d => d.LapTop)
                    .WithMany(p => p.DatHangChiTiets)
                    .HasForeignKey(d => d.LapTopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DatHang_C__LapTo__2E1BDC42");
            });

            modelBuilder.Entity<ImagesProduct>(entity =>
            {
                entity.HasKey(e => e.IdImages)
                    .HasName("PK__Images_P__B87925FDD55114A2");

                entity.Property(e => e.Folder).IsUnicode(false);

                entity.Property(e => e.HinhAnh).IsUnicode(false);

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ImagesProducts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Images_Pr__Id_Pr__24927208");
            });

            modelBuilder.Entity<InfomationTechology>(entity =>
            {
                entity.HasKey(e => e.IdBaiViet)
                    .HasName("PK__Infomati__6992298A48823813");

                entity.Property(e => e.Avatar).IsUnicode(false);

                entity.Property(e => e.LuotXem).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PK__KhachHan__2D8FDE5F4729E0E5");

                entity.Property(e => e.Avartar).IsUnicode(false);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.IdStaff)
                    .HasName("PK__NhanVien__922B8FE6D7D8BF4C");

                entity.Property(e => e.Avartar).IsUnicode(false);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__SanPham__522DE49637B0C931");

                entity.Property(e => e.FolderName).IsUnicode(false);

                entity.HasOne(d => d.CateChildNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.CateChild)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__CateChi__1FCDBCEB");

                entity.HasOne(d => d.HangSx)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.HangSxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__HangSX___1ED998B2");

                entity.HasOne(d => d.IdNhanVienNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__Id_Nhan__20C1E124");

                entity.HasOne(d => d.IdTinhTrangNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdTinhTrang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__Id_Tinh__21B6055D");
            });

            modelBuilder.Entity<Thongtinkythuatlaptop>(entity =>
            {
                entity.HasKey(e => e.IdThongTin)
                    .HasName("PK__thongtin__7F80B85F1CBDDB38");

                entity.Property(e => e.Cpu).IsUnicode(true);

                entity.Property(e => e.Gpu).IsUnicode(true);

                entity.Property(e => e.KichThuoc).IsUnicode(true);

                entity.Property(e => e.ManHinh).IsUnicode(true);

                entity.Property(e => e.Ram).IsUnicode(true);

                entity.Property(e => e.Rom).IsUnicode(true);

                entity.Property(e => e.XuatXu).IsUnicode(true);

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Thongtinkythuatlaptops)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__thongtink__ID_Pr__276EDEB3");
            });

            modelBuilder.Entity<TinhTrang>(entity =>
            {
                entity.HasKey(e => e.MaTt)
                    .HasName("PK__TinhTran__272500797B3874D3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
