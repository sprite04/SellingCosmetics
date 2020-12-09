namespace DOAN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIOHANG")]
    public partial class GIOHANG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdKH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSP { get; set; }

        public int? SoLuong { get; set; }

        public bool? TinhTrang { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
