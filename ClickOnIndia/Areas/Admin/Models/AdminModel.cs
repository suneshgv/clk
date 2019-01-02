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

    #region Add, View and Edit Train Route
    public class TrainRouteModelView
    {
        public List<TrainRouteModel> TrainRouteModelList { get; set; }
    }

    public class TrainRouteModel
    {
        public int RO_ID { get; set; }
        [Display(Name = "Location Name"), Required]
        public string LOCATION_NAME { get; set; }
        public string STATUS { get; set; }
        public string TYPE { get; set; }
        public string TYPE_VALUE { get; set; }
        public string DATE { get; set; }
        public string RESULT { get; set; }
        public string LOCATION_XY { get; set; }
    }
    #endregion

    #region Add, View and Edit Train
    public class TrainModelView
    {
        public List<TrainModel> TrainModelList { get; set; }
    }

    public class TrainModel
    {
        public int TID { get; set; }
        [Display(Name = "Train Name"), Required]
        public string TRAIN_NAME { get; set; }
        public string STATUS { get; set; }
        [Display(Name = "Train Type"), Required]
        public string TYPE { get; set; }
        public string TYPE_VALUE { get; set; }
        public string DATE { get; set; }
        [Display(Name = "Start Time"), Required]
        public string START_TIME { get; set; }
        [Display(Name = "Train Number"), Required]
        public string TRAIN_NUMBER { get; set; }
        public string RESULT { get; set; }
        public string CHECK_ROUTES_VALUES { get; set; }
        public string[] CHECK_ROUTES { get; set; }
    }
    #endregion

    #region Add, View and Edit Bus Route
    public class BusRouteModelView
    {
        public List<BusRouteModel> BusRouteModelList { get; set; }
    }

    public class BusRouteModel
    {
        public int BL_ID { get; set; }
        [Display(Name = "Location Name"), Required]
        public string LOCATION_NAME { get; set; }
        public string STATUS { get; set; }
        public string TYPE { get; set; }
        public string TYPE_VALUE { get; set; }
        public string DATE { get; set; }
        public string RESULT { get; set; }
        public string LOCATION_XY { get; set; }
    }
    #endregion

    #region Add, View and Edit Bus
    public class BusModelView
    {
        public List<BusModel> BusModelList { get; set; }
    }

    public class BusModel
    {
        public int BUS_ID { get; set; }
        [Display(Name = "Bus Name"), Required]
        public string BUS_NAME { get; set; }
        [Display(Name = "Bus Number"), Required]
        public string BUS_NUMBER { get; set; }
        [Display(Name = "Bus Type"), Required]
        public string TYPE { get; set; }
        public string TYPE_VALUE { get; set; }
        public string STATUS { get; set; }
        public string DATE { get; set; }
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
    #endregion

    #region Add, View and Edit Hotel
    public class HotelModelView
    {
        public List<HotelModel> HotelModelList { get; set; }
    }

    public class HotelModel
    {
        public int HID { get; set; }
        [Display(Name = "Hotel Name"), Required]
        public string HOTEL_NAME { get; set; }
        [Display(Name = "Hotel Type"), Required]
        public string TYPE { get; set; }
        public string TYPE_VALUE { get; set; }
        [Display(Name = "Location Name"), Required]
        public string LOCATION { get; set; }
        public string STATUS { get; set; }
        public string ROOM_COUNT { get; set; }
        public string RESULT { get; set; }
    }
    #endregion

    #region Add, View and Edit Hotel Room
    public class HotelRoomModelView
    {
        public List<HotelRoomModel> HotelRoomModelList { get; set; }
    }

    public class HotelRoomModel
    {
        public int HOTEL_ROOM_ID { get; set; }
        [Display(Name = "Hotel"), Required]
        public string HID { get; set; }
        public string HOTEL_NAME { get; set; }
        [Display(Name = "Type"), Required]
        public string ROOM_TYPE_ID { get; set; }
        public string ROOM_TYPE_VALUE { get; set; }
        public string STATUS { get; set; }
        public string RESULT { get; set; }
    }
    #endregion

    #region Add, View and Edit Hotel Room Type
    public class HotelRoomTypeModelView
    {
        public List<HotelRoomTypeModel> HotelRoomTypeModelList { get; set; }
    }

    public class HotelRoomTypeModel
    {
        public int ROOM_TYPE_ID { get; set; }
        [Display(Name = "Room Class"), Required]
        public string ROOM_CLASS { get; set; }
        [Display(Name = "No Of Beds"), Required]
        public string BED_COUNT { get; set; }
        public string RESULT { get; set; }
    }
    #endregion

    #region Comapartment
    public class CompartmentModelView
    {
        public List<CompartmentModel> CompartmentModelList { get; set; }
    }

    public class CompartmentModel
    {
        public int COMP_ID { get; set; }
        [Display(Name = "Compartment Name"), Required]
        public string COMP_NAME { get; set; }
        [Display(Name = "Type"), Required]
        public string TYPE { get; set; }
        public string TYPE_VALUE { get; set; }
        public string STATUS { get; set; }
        [Display(Name = "Train"), Required]
        public string TRAIN_ID { get; set; }
        public string TRAIN_NAME { get; set; }
        public string DATE { get; set; }
        public string UID { get; set; }
        public string RESULT { get; set; }
    }
    #endregion

    #region Seats
    public class SeatModelView
    {
        public List<SeatModel> SeatModelList { get; set; }
    }

    public class SeatModel
    {
        public int SEAT_ID { get; set; }
        [Display(Name = "Seat Name"), Required]
        public string SEAT_NAME { get; set; }
        [Display(Name = "Type"), Required]
        public string TYPE { get; set; }
        public string TYPE_VALUE { get; set; }
        [Display(Name = "Count"), Required]
        public string COUNT { get; set; }
        [Display(Name = "Compartment"), Required]
        public string COMP_ID { get; set; }
        public string COMPARTMENT_NAME { get; set; }
        [Display(Name = "Adult Cost"), Required]
        public string ADULT_COST { get; set; }
        [Display(Name = "Child Cost"), Required]
        public string CHILD_COST { get; set; }
        public string DATE { get; set; }
        public string RESULT { get; set; }
    }
    #endregion
}