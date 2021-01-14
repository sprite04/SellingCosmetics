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
        }

        [Key]
        public int IdHD { get; set; }

        public DateTime? NgayDH { get; set; }

        public int? TongTien { get; set; }

        public int? IdKH { get; set; }

        public int? IdKM { get; set; }

        [Required]
        [StringLength(12)]
        public string SDT { get; set; }

        [Required]
        [StringLength(1000)]
        public string DiaChi { get; set; }

        public int? TinhTrang { get; set; }

        public int? IdDTGH { get; set; }

        public DateTime? NgayGH { get; set; }

        public bool? DaThanhToan { get; set; }

        public int? TienVanChuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHD> CHITIETHDs { get; set; }

        public virtual DTGIAOHANG DTGIAOHANG { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }

        public virtual TINHTRANG TINHTRANG1 { get; set; }
    }
}
