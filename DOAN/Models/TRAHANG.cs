namespace DOAN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRAHANG")]
    public partial class TRAHANG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTra { get; set; }

        public bool? TinhTrang { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(200)]
        public string LyDo { get; set; }

        public virtual HOADON HOADON { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
