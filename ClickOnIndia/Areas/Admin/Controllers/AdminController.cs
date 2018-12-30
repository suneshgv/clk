using ClickOnIndia.Areas.Admin.Models;
using ClickOnIndia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClickOnIndia.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Train Route
        [HttpGet]
        public ActionResult ManageTrainRoute()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    TrainRouteModel objTrainRouteModel = new TrainRouteModel();
                    return View(objTrainRouteModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "Home" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "Home" });
            }
        }

        [HttpPost]
        public ActionResult ManageTrainRoute(TrainRouteModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objRoute = (from route in dbEntities.tbl_Route
                                            where route.LocationName.ToLower().Equals(obj.LOCATION_NAME.ToLower())
                                            select route).FirstOrDefault();
                            if (objRoute == null)
                            {
                                tbl_Route objRouteAdd = new tbl_Route();
                                objRouteAdd.LocationName = obj.LOCATION_NAME;
                                objRouteAdd.Status = true;
                                objRouteAdd.Type = 1;
                                objRouteAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                dbEntities.tbl_Route.Add(objRouteAdd);
                                dbEntities.SaveChanges();

                                obj.RESULT = "Entered Location is inserted successfully.";
                            }
                            else
                            {
                                obj.RESULT = "Entered Location is already available.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj.RESULT = "Error occured, please try again.";
                }
            }
            else
            {
                obj.RESULT = "Session Expired, please login again.";
            }
            return View(obj);
        }
        #endregion

        #region Train
        public ActionResult ManageTrain()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    TrainModel objTrainModel = new TrainModel();
                    return View(objTrainModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "Home" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "Home" });
            }
        }

        [HttpPost]
        public ActionResult ManageTrain(TrainModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (obj.CHECK_ROUTES != null && obj.CHECK_ROUTES.Length > 0)
                        {
                            using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                            {
                                var objTrian = (from train in dbEntities.tbl_Train
                                                where train.TrainName.ToLower().Equals(obj.TRAIN_NAME.ToLower())
                                                && train.StartTime.ToString().Equals(obj.START_TIME)
                                                && train.TrainNum.ToString().Equals(obj.TRAIN_NUMBER)
                                                select train).FirstOrDefault();
                                if (objTrian == null)
                                {
                                    TimeSpan time = TimeSpan.Parse(obj.START_TIME);

                                    tbl_Train objTrainAdd = new tbl_Train();
                                    objTrainAdd.TrainName = obj.TRAIN_NAME;
                                    objTrainAdd.StartTime = time;
                                    objTrainAdd.TrainNum = obj.TRAIN_NUMBER;
                                    objTrainAdd.Status = true;
                                    objTrainAdd.Type = Convert.ToInt32(obj.TYPE);
                                    objTrainAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                    dbEntities.tbl_Train.Add(objTrainAdd);
                                    dbEntities.SaveChanges();
                                    int Tid = objTrainAdd.Tid;
                                    if (Tid > 0)
                                    {
                                        int sort_order = 1;
                                        foreach (var rid in obj.CHECK_ROUTES)
                                        {
                                            tbl_TrainRoute objTrainRouteAdd = new tbl_TrainRoute();
                                            objTrainRouteAdd.Roid = Convert.ToInt32(rid);
                                            objTrainRouteAdd.Tid = Tid;
                                            objTrainRouteAdd.SortOrder = sort_order;
                                            objTrainRouteAdd.Status = true;
                                            objTrainRouteAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                            dbEntities.tbl_TrainRoute.Add(objTrainRouteAdd);
                                            dbEntities.SaveChanges();
                                            sort_order++;
                                        }

                                        obj.RESULT = "Entered train is inserted successfully.";
                                    }
                                    else
                                    {
                                        obj.RESULT = "Error occured, please try again.";
                                    }
                                }
                                else
                                {
                                    obj.RESULT = "Entered train is already available.";
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("CHECK_ROUTES_VALUES", "Please check atleast one Location");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CHECK_ROUTES_VALUES", "Please check atleast one Location");
                    }
                }
                catch (Exception ex)
                {
                    obj.RESULT = "Error occured, please try again.";
                }
            }
            else
            {
                obj.RESULT = "Session Expired, please login again.";
            }
            return View(obj);
        }
        #endregion

        #region Bus Route
        [HttpGet]
        public ActionResult ManageBusRoute()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    BusRouteModel objBusRouteModel = new BusRouteModel();
                    return View(objBusRouteModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "Home" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "Home" });
            }
        }

        [HttpPost]
        public ActionResult ManageBusRoute(BusRouteModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objRoute = (from route in dbEntities.tbl_BusLoc
                                            where route.LocationName.ToLower().Equals(obj.LOCATION_NAME.ToLower())
                                            select route).FirstOrDefault();
                            if (objRoute == null)
                            {
                                tbl_BusLoc objRouteAdd = new tbl_BusLoc();
                                objRouteAdd.LocationName = obj.LOCATION_NAME;
                                objRouteAdd.Status = true;
                                objRouteAdd.Type = 1;
                                objRouteAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                dbEntities.tbl_BusLoc.Add(objRouteAdd);
                                dbEntities.SaveChanges();

                                obj.RESULT = "Entered Bus Route is inserted successfully.";
                            }
                            else
                            {
                                obj.RESULT = "Entered Bus Route is already available.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj.RESULT = "Error occured, please try again.";
                }
            }
            else
            {
                obj.RESULT = "Session Expired, please login again.";
            }
            return View(obj);
        }
        #endregion

        #region Bus
        public ActionResult ManageBus()
        {
            BusModel objBusModel = new BusModel();
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View(objBusModel);
        }

        [HttpPost]
        public ActionResult ManageBus(BusModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (obj.CHECK_ROUTES != null && obj.CHECK_ROUTES.Length > 0)
                        {
                            using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                            {
                                var objBus = (from bus in dbEntities.tbl_Bus
                                              where bus.BusName.ToLower().Equals(obj.BUS_NAME.ToLower())
                                              && bus.StartTime.ToString().Equals(obj.START_TIME)
                                              && bus.BusNum.ToString().Equals(obj.BUS_NUMBER)
                                              select bus).FirstOrDefault();
                                if (objBus == null)
                                {
                                    TimeSpan time = TimeSpan.Parse(obj.START_TIME);

                                    tbl_Bus objBusAdd = new tbl_Bus();
                                    objBusAdd.BusName = obj.BUS_NAME;
                                    objBusAdd.BusNum = obj.BUS_NUMBER;
                                    objBusAdd.Type = Convert.ToInt32(obj.TYPE);
                                    objBusAdd.Seatcount = Convert.ToInt32(obj.SEAT_COUNT);
                                    objBusAdd.CostsAdult = Convert.ToInt32(obj.ADULT_COST);
                                    objBusAdd.CostsChild = Convert.ToInt32(obj.CHILD_COST);
                                    objBusAdd.StartTime = time;
                                    objBusAdd.Status = true;
                                    objBusAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                    dbEntities.tbl_Bus.Add(objBusAdd);
                                    dbEntities.SaveChanges();
                                    int Busid = objBusAdd.Busid;
                                    if (Busid > 0)
                                    {
                                        int sort_order = 1;
                                        foreach (var rid in obj.CHECK_ROUTES)
                                        {
                                            tbl_BusRoute objBusRouteAdd = new tbl_BusRoute();
                                            objBusRouteAdd.Blid = Convert.ToInt32(rid);
                                            objBusRouteAdd.BusId = Busid;
                                            objBusRouteAdd.Sortorder = sort_order;
                                            objBusRouteAdd.Status = true;
                                            dbEntities.tbl_BusRoute.Add(objBusRouteAdd);
                                            dbEntities.SaveChanges();
                                            sort_order++;
                                        }

                                        obj.RESULT = "Entered Bus is inserted successfully.";
                                    }
                                    else
                                    {
                                        obj.RESULT = "Error occured, please try again.";
                                    }
                                }
                                else
                                {
                                    obj.RESULT = "Entered Bus is already available.";
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("CHECK_ROUTES_VALUES", "Please check atleast one Location");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CHECK_ROUTES_VALUES", "Please check atleast one Location");
                    }
                }
                catch (Exception ex)
                {
                    obj.RESULT = "Error occured, please try again.";
                }
            }
            else
            {
                obj.RESULT = "Session Expired, please login again.";
            }
            return View(obj);
        }
        #endregion

        #region Hotels
        [HttpGet]
        public ActionResult ManageHotels()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelModel objManageHotel = new HotelModel();
                    return View(objManageHotel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "Home" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "Home" });
            }
        }

        [HttpPost]
        public ActionResult ManageHotels(HotelModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objHotels = (from hotels in dbEntities.tbl_Hotels
                                             where hotels.HotelName.ToLower().Equals(obj.HOTEL_NAME.ToLower())
                                             && hotels.Type.ToString().Equals(obj.TYPE)
                                             select hotels).FirstOrDefault();
                            if (objHotels == null)
                            {
                                tbl_Hotels objHotelsAdd = new tbl_Hotels();
                                objHotelsAdd.HotelName = obj.HOTEL_NAME;
                                objHotelsAdd.Type = Convert.ToInt32(obj.TYPE);
                                objHotelsAdd.Location = obj.LOCATION;
                                objHotelsAdd.Status = true;
                                objHotelsAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                dbEntities.tbl_Hotels.Add(objHotelsAdd);
                                dbEntities.SaveChanges();

                                obj.RESULT = "Entered Hotel is inserted successfully.";
                            }
                            else
                            {
                                obj.RESULT = "Entered Hotel is already available.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj.RESULT = "Error occured, please try again.";
                }
            }
            else
            {
                obj.RESULT = "Session Expired, please login again.";
            }
            return View(obj);
        }
        #endregion

        #region Hotel Room
        [HttpGet]
        public ActionResult ManageHotelRoom()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelRoomModel objHotelRoomModel = new HotelRoomModel();
                    return View(objHotelRoomModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "Home" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "Home" });
            }
        }

        [HttpPost]
        public ActionResult ManageHotelRoom(HotelRoomModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objRooms = (from rooms in dbEntities.tbl_HotelRoom
                                            where rooms.RoomTypeId.ToString().Equals(obj.ROOM_TYPE_ID)
                                            && rooms.Hid.ToString().Equals(obj.HID)
                                            select rooms).FirstOrDefault();
                            if (objRooms == null)
                            {
                                tbl_HotelRoom objHotelRoomAdd = new tbl_HotelRoom();
                                objHotelRoomAdd.Hid = Convert.ToInt32(obj.HID);
                                objHotelRoomAdd.RoomTypeId = Convert.ToInt32(obj.ROOM_TYPE_ID);
                                objHotelRoomAdd.Status = true;
                                objHotelRoomAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                dbEntities.tbl_HotelRoom.Add(objHotelRoomAdd);
                                dbEntities.SaveChanges();

                                obj.RESULT = "Entered Hotel Room is inserted successfully.";
                            }
                            else
                            {
                                obj.RESULT = "Entered Hotel Room is already available.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj.RESULT = "Error occured, please try again.";
                }
            }
            else
            {
                obj.RESULT = "Session Expired, please login again.";
            }
            return View(obj);
        }
        #endregion

        #region Hotel Room types
        [HttpGet]
        public ActionResult ManageHotelRoomType()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelRoomTypeModel objHotelRoomTypeModel = new HotelRoomTypeModel();
                    return View(objHotelRoomTypeModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "Home" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "Home" });
            }
        }

        [HttpPost]
        public ActionResult ManageHotelRoomType(HotelRoomTypeModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objRoomType = (from roomtype in dbEntities.tbl_RoomTypes
                                               where roomtype.Room_Class.ToLower().Equals(obj.Room_Class.ToLower())
                                               && roomtype.Bed_Count.ToString().Equals(obj.Bed_Count)
                                               select roomtype).FirstOrDefault();
                            if (objRoomType == null)
                            {
                                tbl_RoomTypes objRoomTypeAdd = new tbl_RoomTypes();
                                objRoomTypeAdd.Room_Class = obj.Room_Class;
                                objRoomTypeAdd.Bed_Count = Convert.ToInt32(obj.Bed_Count);
                                objRoomTypeAdd.Status = true;
                                objRoomTypeAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                dbEntities.tbl_RoomTypes.Add(objRoomTypeAdd);
                                dbEntities.SaveChanges();

                                obj.RESULT = "Entered Hotel Room Type is inserted successfully.";
                            }
                            else
                            {
                                obj.RESULT = "Entered Hotel Room Type is already available.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj.RESULT = "Error occured, please try again.";
                }
            }
            else
            {
                obj.RESULT = "Session Expired, please login again.";
            }
            return View(obj);
        }
        #endregion

    }
}