namespace DOAN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHUYENMAI")]
    public partial class KHUYENMAI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHUYENMAI()
        {
            HOADONs = new HashSet<HOADON>();
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        public int IdMa { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(30)]
        [DisplayName("Mã khuyến mãi")]
        public string MaKM { get; set; }

        [DisplayName("Loại khuyến mãi")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public int? LoaiKM { get; set; }

        [DisplayName("Ngày bắt đầu")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public DateTime? NgayBD { get; set; }

        [DisplayName("Ngày kết thúc")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public DateTime? NgayKT { get; set; }

        [DisplayName("Giá trị")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public int? GiaTri { get; set; }

        [StringLength(200)]
        public string ChiTiet { get; set; }

        public bool? TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
