using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClickOnIndia.Areas.Admin.Models
{
    public class AdminModel
    {
    }

    public class RouteModel
    {
        [Required]
        public string LOCATION_NAME { get; set; }
        public string STATUS { get; set; }
        public string TYPE { get; set; }
        public string DATE { get; set; }
        public string RESULT { get; set; }
    }

    public class TrainModel
    {
        [Required]
        public string TRAIN_NAME { get; set; }
        public string STATUS { get; set; }
        [Required]
        public string TYPE { get; set; }
        public string DATE { get; set; }
        [Required]
        public string START_TIME { get; set; }
        [Required]
        public string TRAIN_NUM { get; set; }
        public string RESULT { get; set; }
        public string[] CHECK_ROUTES { get; set; }
    }

    public class BusModel
    {

    }
}