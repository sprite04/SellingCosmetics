using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DOAN.Models
{
    [MetadataTypeAttribute(typeof(NguoiDungMetadata))]
    public partial class NGUOIDUNG
    {
        internal sealed class NguoiDungMetadata
        {
            [StringLength(50)]
            public string Username { get; set; }

            [StringLength(12)]
            [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Your phone number is unvalid.")]
            public string SDT { get; set; }

            [StringLength(50)]
            [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Your email address is unvalid.")]
            public string Mail { get; set; }

            [StringLength(50)]
            [Compare("Password", ErrorMessage = "Password miss match.")]
            public string Password1 { get; set; }
        }
    }
}