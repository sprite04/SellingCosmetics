namespace DOAN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("THONGKETHANG")]
    public partial class THONGKETHANG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Nam { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Thang { get; set; }

        public int? TongNhap { get; set; }

        public int? TongGiaHang { get; set; }

        public int? TongDoanhThu { get; set; }
    }
}
