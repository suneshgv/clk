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

    public class TrainRouteModel
    {
        [Display(Name = "Location Name"), Required]
        public string LOCATION_NAME { get; set; }
        public string STATUS { get; set; }
        public string TYPE { get; set; }
        public string DATE { get; set; }
        public string RESULT { get; set; }
        public string LOCATION_XY { get; set; }
    }

    public class TrainModel
    {
        [Display(Name = "Train Name"), Required]
        public string TRAIN_NAME { get; set; }
        public string STATUS { get; set; }
        [Display(Name = "Train Type"), Required]
        public string TYPE { get; set; }
        public string DATE { get; set; }
        [Display(Name = "Start Time"), Required]
        public string START_TIME { get; set; }
        [Display(Name = "Train Number"), Required]
        public string TRAIN_NUMBER { get; set; }
        public string RESULT { get; set; }
        public string CHECK_ROUTES_VALUES { get; set; }
        public string[] CHECK_ROUTES { get; set; }
    }

    public class BusRouteModel
    {
        [Display(Name = "Location Name"), Required]
        public string LOCATION_NAME { get; set; }
        public string STATUS { get; set; }
        public string TYPE { get; set; }
        public string DATE { get; set; }
        public string RESULT { get; set; }
        public string LOCATION_XY { get; set; }
    }

    public class BusModel
    {
        [Display(Name = "Bus Name"), Required]
        public string BUS_NAME { get; set; }
        [Display(Name = "Bus Number"), Required]
        public string BUS_NUMBER { get; set; }
        [Display(Name = "Bus Type"), Required]
        public string TYPE { get; set; }
        public bool STATUS { get; set; }
        [Display(Name = "Seat Count"), Required]
        public string SEAT_COUNT { get; set; }
        [Display(Name = "Adult Cost"), Required]
        public string ADULT_COST { get; set; }
        [Display(Name = "Child Cost"), Required]
        public string CHILD_COST { get; set; }
        [Display(Name = "Start Time"), Required]
        public string START_TIME { get; set; }
        public string RESULT { get; set; }
        public string CHECK_ROUTES_VALUES { get; set; }
        public string[] CHECK_ROUTES { get; set; }
    }

    public class HotelModel
    {
        [Display(Name = "Hotel Name"), Required]
        public string HOTEL_NAME { get; set; }
        [Display(Name = "Hotel Type"), Required]
        public string TYPE { get; set; }
        [Display(Name = "Location Name"), Required]
        public string LOCATION { get; set; }
        public string STATUS { get; set; }
        public string ROOM_COUNT { get; set; }
        public string RESULT { get; set; }
    }

    public class HotelRoomModel
    {
        [Display(Name = "Hotel"), Required]
        public string HID { get; set; }
        [Display(Name = "Type"), Required]
        public string ROOM_TYPE_ID { get; set; }
        public string STATUS { get; set; }
        public string RESULT { get; set; }
    }

    public class HotelRoomTypeModel
    {
        [Display(Name = "Room Class"), Required]
        public string Room_Class { get; set; }
        [Display(Name = "No Of Beds"), Required]
        public string Bed_Count { get; set; }
        public string RESULT { get; set; }
    }
}