using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClickOnIndia.App_Start
{
    public class CommonModel
    {
    }

    public class LoginModel
    {
        [Required]
        public string USER_NAME { get; set; }
        [Required]
        public string PASSWORD { get; set; }
        public string RESULT { get; set; }
    }

    public class ListModel
    {
        public string ID { get; set; }
        public string VALUE { get; set; }
    }
}