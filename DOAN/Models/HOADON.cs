namespace DOAN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOADON")]
    public partial class HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADON()
        {
            CHITIETHDs = new HashSet<CHITIETHD>();
            TRAHANGs = new HashSet<TRAHANG>();
        }

        [Key]
        public int IdHD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDH { get; set; }

        public int? TongTien { get; set; }

        public int? IdKH { get; set; }

        [StringLength(30)]
        public string MaKM { get; set; }

        [Required]
        [StringLength(12)]
        public string SDT { get; set; }

        [Required]
        [StringLength(50)]
        public string DiaChi { get; set; }

        public int? TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHD> CHITIETHDs { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual TINHTRANG TINHTRANG1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRAHANG> TRAHANGs { get; set; }

        public virtual TINHTRANGDH TINHTRANGDH { get; set; }
    }
}
