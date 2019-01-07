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
    //public class HotelBookingPlan
    //{
    //    public int locId { get; set; }
    //    public string fromDate { get; set; }
    //    public string toDate { get; set; }
    //    public int roomtype { get; set; }
    //    public int roomCount { get; set; }
    //}
    public class AllBookingPlan
    {
        public TrainBusBookingPlan TrainBusBookingPlan { get; set; }
        public HotelBookingPlan HotelBookingPlan { get; set; }
    }

    public class TrainSeatAvaliabity
    {
        public string Tid { get; set; }
        public string TrainName { get; set; }
        public string TrainNum { get; set; }
        public string StartTime { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string fromId { get; set; }
        public string toId { get; set; }
        public string JourDate { get; set; }
        public List<TrainSeatAvaliabityCount> TrainSeatAvaliabityCounts { get; set; }
    }
    public class TrainSeatAvaliabityCount
    {

        public string Type { get; set; }
        public string Count { get; set; }
        public string Avialiable { get; set; }
        public string SeatId { get; set; }

    }

    public class HotelBookingPlan
    {
        public string CITY { get; set; }
        public string RETURN_DATE { get; set; }
        public string CHECK_OUT_DATE { get; set; }
        public string ROOM_TYPE { get; set; }
        public string ADULT_COUNT { get; set; }
        public string CHILD_COUNT { get; set; }
    }
    public class HotelAvalability
    {
        public int HOTEL_ID { get; set; }
        public string HOTEL_NAME { get; set; }
        public string LOCATION { get; set; }
        public int HOTEL_ROOM_COUNT { get; set; }
        public List<HotelAvalabilityList> HOTEL_ROOMS { get; set; }
    }
    public class HotelAvalabilityList
    {
        public int HOTEL_ROOM_ID { get; set; }
        public int ROOM_TYPE_ID { get; set; }
        public string ROOM_CLASS { get; set; }
        public string BED_COUNT { get; set; }
    }
}