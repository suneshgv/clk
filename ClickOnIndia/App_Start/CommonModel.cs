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

    public class TrainBusBookingPlan
    {
        public string bookType { get; set; }
        public int fromLocId { get; set; }
        public int toLocId { get; set; }
        public int type { get; set; }
        public int adultC { get; set; }
        public int childC { get; set; }
        public string fromDate { get; set; }

    }
    public class HotelBookingPlan
    {

        public int locId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int roomtype { get; set; }
        public int roomCount { get; set; }


    }
    public class AllBookingPlan
    {
        public TrainBusBookingPlan TrainBusBookingPlan { get; set; }
        public HotelBookingPlan HotelBookingPlan { get; set; }
    }
}