namespace DOAN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHAPHANG")]
    public partial class NHAPHANG
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime NgayNhap { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSP { get; set; }

        public int? SoLuong { get; set; }

        public int? GiaNhap { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
