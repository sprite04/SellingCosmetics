namespace DOAN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            CHITIETHDs = new HashSet<CHITIETHD>();
            GIOHANGs = new HashSet<GIOHANG>();
            NHAPHANGs = new HashSet<NHAPHANG>();
            SANPHAMLOIs = new HashSet<SANPHAMLOI>();
            TRAHANGs = new HashSet<TRAHANG>();
        }

        [Key]
        public int IdSP { get; set; }

        [StringLength(50)]
        public string TenSP { get; set; }

        [StringLength(50)]
        public string AnhSP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [StringLength(2000)]
        public string MoTa { get; set; }

        public int? TinhTrang { get; set; }

        public int? GiaGoc { get; set; }

        public float? LoiNhuan { get; set; }

        [StringLength(20)]
        public string DonVi { get; set; }

        public int? SoLuong { get; set; }

        public int? SoLanMua { get; set; }

        public int? MaKM { get; set; }

        public int? IdTH { get; set; }

        public int? IdLoaiSP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHD> CHITIETHDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIOHANG> GIOHANGs { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }

        public virtual LOAISANPHAM LOAISANPHAM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAPHANG> NHAPHANGs { get; set; }

        public virtual THUONGHIEU THUONGHIEU { get; set; }

        public virtual TINHTRANG TINHTRANG1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAMLOI> SANPHAMLOIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRAHANG> TRAHANGs { get; set; }
    }
}
