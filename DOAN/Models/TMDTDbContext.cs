namespace DOAN.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TMDTDbContext : DbContext
    {
        public TMDTDbContext()
            : base("name=TMDT")
        {
        }

        public virtual DbSet<CHITIETHD> CHITIETHDs { get; set; }
        public virtual DbSet<DTGIAOHANG> DTGIAOHANGs { get; set; }
        public virtual DbSet<GIOHANG> GIOHANGs { get; set; }
        public virtual DbSet<HOADON> HOADONs { get; set; }
        public virtual DbSet<KHUYENMAI> KHUYENMAIs { get; set; }
        public virtual DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
        public virtual DbSet<LOAIUSER> LOAIUSERs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NHAPHANG> NHAPHANGs { get; set; }
        public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<SANPHAMLOI> SANPHAMLOIs { get; set; }
        public virtual DbSet<THONGKETHANG> THONGKETHANGs { get; set; }
        public virtual DbSet<THUONGHIEU> THUONGHIEUx { get; set; }
        public virtual DbSet<TINHTRANG> TINHTRANGs { get; set; }
        public virtual DbSet<TINHTRANGDH> TINHTRANGDHs { get; set; }
        public virtual DbSet<TRAHANG> TRAHANGs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HOADON>()
                .Property(e => e.MaKM)
                .IsUnicode(false);

            modelBuilder.Entity<HOADON>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<HOADON>()
                .HasMany(e => e.CHITIETHDs)
                .WithRequired(e => e.HOADON)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOADON>()
                .HasMany(e => e.TRAHANGs)
                .WithRequired(e => e.HOADON)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOADON>()
                .HasOptional(e => e.TINHTRANGDH)
                .WithRequired(e => e.HOADON);

            modelBuilder.Entity<KHUYENMAI>()
                .Property(e => e.MaKM)
                .IsUnicode(false);

            modelBuilder.Entity<KHUYENMAI>()
                .HasMany(e => e.SANPHAMs)
                .WithOptional(e => e.KHUYENMAI)
                .HasForeignKey(e => e.MaKM);

            modelBuilder.Entity<LOAIUSER>()
                .HasMany(e => e.PHANQUYENs)
                .WithRequired(e => e.LOAIUSER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .HasMany(e => e.GIOHANGs)
                .WithRequired(e => e.NGUOIDUNG)
                .HasForeignKey(e => e.IdKH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .HasMany(e => e.HOADONs)
                .WithOptional(e => e.NGUOIDUNG)
                .HasForeignKey(e => e.IdKH);

            modelBuilder.Entity<PHANQUYEN>()
                .Property(e => e.TinhNang)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.AnhSP)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CHITIETHDs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.GIOHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.NHAPHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.SANPHAMLOIs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.TRAHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<THUONGHIEU>()
                .Property(e => e.AnhTH)
                .IsUnicode(false);

            modelBuilder.Entity<THUONGHIEU>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<TINHTRANG>()
                .HasMany(e => e.HOADONs)
                .WithOptional(e => e.TINHTRANG1)
                .HasForeignKey(e => e.TinhTrang);

            modelBuilder.Entity<TINHTRANG>()
                .HasMany(e => e.SANPHAMs)
                .WithOptional(e => e.TINHTRANG1)
                .HasForeignKey(e => e.TinhTrang);

            modelBuilder.Entity<TINHTRANG>()
                .HasMany(e => e.TINHTRANGDHs)
                .WithOptional(e => e.TINHTRANG1)
                .HasForeignKey(e => e.TinhTrang);
        }
    }
}
