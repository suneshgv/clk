using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClickOnIndia.Models
{
    [MetadataType(typeof(tbl_UserDetailMetaData))]
    public partial class tbl_UserDetail
    {
        public string ConfirmPassword { get; set; }
    }
    public class tbl_UserDetailMetaData
    {
        public int Uid { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }


        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public Nullable<int> Rid { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime Date { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"\+?\d[\d -]{8,12}\d")]
        public string Phone { get; set; }
    }
}