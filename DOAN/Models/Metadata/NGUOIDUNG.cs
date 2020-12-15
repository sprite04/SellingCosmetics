using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN.Models
{
    [MetadataTypeAttribute(typeof(NguoiDungMetadata))]
    public partial class NGUOIDUNG
    {
        internal sealed class NguoiDungMetadata
        {
            [StringLength(50)]
            public string Username { get; set; }


            [DisplayName("Name")]
            [StringLength(50)]
            public string HoTen { get; set; }

            [DisplayName("Phone Number")]
            [StringLength(12)]
            [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Phone number is unvalid.")]
            public string SDT { get; set; }

            [StringLength(50)]
            [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email address is unvalid.")]
            public string Mail { get; set; }

            [StringLength(50)]
            [Compare("Password", ErrorMessage = "Password miss match.")]
            public string Password1 { get; set; }

            [DisplayName("Female")]
            public bool GioiTinh { get; set; }


            [Column(TypeName = "date")]
            [DisplayName("Date Of Birth")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? NgaySinh { get; set; }

            [StringLength(50)]
            [DisplayName("Address")]
            public string DiaChi { get; set; }


            [DisplayName("User Type")]
            public int? IdLoaiUser { get; set; }
        }
    }
}