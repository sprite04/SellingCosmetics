namespace DOAN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TINHTRANGDH")]
    public partial class TINHTRANGDH
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdHD { get; set; }

        public int? IdDTGH { get; set; }

        public int? TinhTrang { get; set; }

        public virtual DTGIAOHANG DTGIAOHANG { get; set; }

        public virtual HOADON HOADON { get; set; }

        public virtual TINHTRANG TINHTRANG1 { get; set; }
    }
}
