using ClickOnIndia.App_Start;
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

        #region Add, View and Edit Train Route
        [HttpGet]
        public ActionResult ManageTrainRoute(int? roID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    TrainRouteModel objTrainRouteModel = new TrainRouteModel();

                    if (roID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objRoute = (from route in dbEntities.tbl_Route
                                            where route.Roid == roID
                                            select route).FirstOrDefault();

                            if (objRoute != null)
                            {
                                objTrainRouteModel.RO_ID = objRoute.Roid;
                                objTrainRouteModel.LOCATION_NAME = objRoute.LocationName;
                                objTrainRouteModel.LOCATION_XY = objRoute.LocationXY;
                                objTrainRouteModel.TYPE = objRoute.Type.ToString();
                            }
                        }
                    }

                    return View(objTrainRouteModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
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
                            if (obj.RO_ID > 0)
                            {
                                var objRoute = (from route in dbEntities.tbl_Route
                                                where route.Roid == route.Roid
                                                select route).FirstOrDefault();
                                if (objRoute != null)
                                {
                                    objRoute.LocationName = obj.LOCATION_NAME;
                                    objRoute.Status = true;
                                    objRoute.Type = 1;
                                    objRoute.Date = DateTime.Now;
                                    objRoute.Uid = Convert.ToInt32(Session["UserID"]);
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Location is updated successfully.";
                                }
                            }
                            else
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
                                    objRouteAdd.Date = DateTime.Now;
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

        [HttpGet]
        public ActionResult ViewTrainRoute()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    TrainRouteModelView objTrainRouteModelView = new TrainRouteModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        objTrainRouteModelView.TrainRouteModelList = (from route in dbEntities.tbl_Route
                                                                      orderby route.Roid
                                                                      select new TrainRouteModel
                                                                      {
                                                                          RO_ID = route.Roid,
                                                                          LOCATION_NAME = route.LocationName,
                                                                          STATUS = route.Status.ToString(),
                                                                          TYPE = route.Type.ToString(),
                                                                          DATE = route.Date.ToString(),
                                                                          LOCATION_XY = route.LocationXY ?? ""
                                                                      }).ToList();

                    }
                    return View(objTrainRouteModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion

        #region Add, View and Edit Train
        public ActionResult ManageTrain(int? tID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    TrainModel objTrainModel = new TrainModel();

                    if (tID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objTrain = (from train in dbEntities.tbl_Train
                                            where train.Tid == tID
                                            select train).FirstOrDefault();
                            if (objTrain != null)
                            {
                                objTrainModel.TRAIN_NAME = objTrain.TrainName;
                                objTrainModel.START_TIME = objTrain.StartTime.ToString();
                                objTrainModel.TRAIN_NUMBER = objTrain.TrainNum;
                                objTrainModel.TYPE = objTrain.Type.ToString();
                                objTrainModel.TID = objTrain.Tid;
                            }

                            var objTrainRoute = (from trainRoute in dbEntities.tbl_TrainRoute
                                                 where trainRoute.Tid == tID
                                                 && trainRoute.Status == true
                                                 orderby trainRoute.SortOrder
                                                 select trainRoute).ToList();
                            if (objTrainRoute != null && objTrainRoute.Count() > 0)
                            {
                                objTrainModel.CHECK_ROUTES = objTrainRoute.Select(x => x.Roid.ToString()).Distinct().ToArray();
                            }
                        }
                    }

                    return View(objTrainModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
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
                            //TimeSpan time = TimeSpan.Parse(obj.START_TIME);
                            TimeSpan time = DateTime.Parse(obj.START_TIME).TimeOfDay;

                            using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                            {
                                if (obj.TID > 0)
                                {
                                    var objTrain = (from train in dbEntities.tbl_Train
                                                    where train.Tid == obj.TID
                                                    select train).FirstOrDefault();
                                    if (objTrain != null)
                                    {
                                        objTrain.TrainName = obj.TRAIN_NAME;
                                        objTrain.StartTime = time;
                                        objTrain.TrainNum = obj.TRAIN_NUMBER;
                                        objTrain.Status = true;
                                        objTrain.Type = Convert.ToInt32(obj.TYPE);
                                        objTrain.Uid = Convert.ToInt32(Session["UserID"]);
                                        objTrain.Date = DateTime.Now;
                                        dbEntities.SaveChanges();
                                    }

                                    var objTrainRouteDelete = (from trainRoute in dbEntities.tbl_TrainRoute
                                                               where trainRoute.Tid == obj.TID
                                                               select trainRoute).ToList();
                                    if (objTrainRouteDelete != null && objTrainRouteDelete.Count() > 0)
                                    {
                                        dbEntities.tbl_TrainRoute.RemoveRange(objTrainRouteDelete);
                                        dbEntities.SaveChanges();
                                    }

                                    int sort_order = 1;
                                    foreach (var rid in obj.CHECK_ROUTES)
                                    {
                                        tbl_TrainRoute objTrainRouteAdd = new tbl_TrainRoute();
                                        objTrainRouteAdd.Roid = Convert.ToInt32(rid);
                                        objTrainRouteAdd.Tid = obj.TID;
                                        objTrainRouteAdd.SortOrder = sort_order;
                                        objTrainRouteAdd.Status = true;
                                        objTrainRouteAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                        objTrainRouteAdd.Date = DateTime.Now;
                                        dbEntities.tbl_TrainRoute.Add(objTrainRouteAdd);
                                        dbEntities.SaveChanges();
                                        sort_order++;
                                    }

                                    obj.RESULT = "Entered train is updated successfully.";
                                }
                                else
                                {
                                    var objTrian = (from train in dbEntities.tbl_Train
                                                    where train.TrainName.ToLower().Equals(obj.TRAIN_NAME.ToLower())
                                                    && train.StartTime.ToString().Equals(obj.START_TIME)
                                                    && train.TrainNum.ToString().Equals(obj.TRAIN_NUMBER)
                                                    select train).FirstOrDefault();
                                    if (objTrian == null)
                                    {
                                        tbl_Train objTrainAdd = new tbl_Train();
                                        objTrainAdd.TrainName = obj.TRAIN_NAME;
                                        objTrainAdd.StartTime = time;
                                        objTrainAdd.TrainNum = obj.TRAIN_NUMBER;
                                        objTrainAdd.Status = true;
                                        objTrainAdd.Type = Convert.ToInt32(obj.TYPE);
                                        objTrainAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                        objTrainAdd.Date = DateTime.Now;
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
                                                objTrainRouteAdd.Date = DateTime.Now;
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

        [HttpGet]
        public ActionResult ViewTrain()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    TrainModelView objTrainModelView = new TrainModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        var objTrainModelList = (from trains in dbEntities.tbl_Train
                                                 select new
                                                 {
                                                     trains.Tid,
                                                     trains.TrainName,
                                                     trains.Status,
                                                     trains.Type,
                                                     trains.Date,
                                                     trains.StartTime,
                                                     trains.TrainNum,
                                                     TrainRoutes = (from trainRoutes in dbEntities.tbl_TrainRoute
                                                                    join routes in dbEntities.tbl_Route
                                                                    on trainRoutes.Roid equals routes.Roid
                                                                    where trainRoutes.Tid == trains.Tid
                                                                    && trainRoutes.Status == true
                                                                    && routes.Status == true
                                                                    orderby trainRoutes.SortOrder
                                                                    select new
                                                                    {
                                                                        routes.Roid,
                                                                        routes.LocationName
                                                                    }).Distinct().ToList()
                                                 }).ToList();

                        objTrainModelView.TrainModelList = (from x in objTrainModelList
                                                            select new TrainModel
                                                            {
                                                                TID = x.Tid,
                                                                TRAIN_NAME = x.TrainName,
                                                                STATUS = x.Status.ToString(),
                                                                TYPE = x.Type.ToString(),
                                                                TYPE_VALUE = Enum.GetName(typeof(Common.TrainType), x.Type).Replace("_", " "),
                                                                DATE = x.Date.ToString(),
                                                                START_TIME = x.StartTime.ToString(),
                                                                TRAIN_NUMBER = x.TrainNum.ToString(),
                                                                CHECK_ROUTES_VALUES = string.Join(",", x.TrainRoutes.Select(y => y.Roid)),
                                                                CHECK_ROUTES = x.TrainRoutes.Select(y => y.LocationName).ToArray()
                                                            }).ToList();
                    }
                    return View(objTrainModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion

        #region Add, View and Edit Bus Route
        [HttpGet]
        public ActionResult ManageBusRoute(int? blID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    BusRouteModel objBusRouteModel = new BusRouteModel();
                    if (blID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objRoute = (from route in dbEntities.tbl_BusLoc
                                            where route.Blid == blID
                                            select route).FirstOrDefault();
                            if (objRoute != null)
                            {
                                objBusRouteModel.LOCATION_NAME = objRoute.LocationName;
                                objBusRouteModel.LOCATION_XY = objRoute.LocationXY;
                                objBusRouteModel.TYPE = objRoute.Type.ToString();                                
                            }
                        }
                    }
                    return View(objBusRouteModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
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
                            if (obj.BL_ID > 0)
                            {
                                var objRoute = (from route in dbEntities.tbl_BusLoc
                                                where route.Blid == obj.BL_ID
                                                select route).FirstOrDefault();
                                if (objRoute != null)
                                {
                                    objRoute.LocationName = obj.LOCATION_NAME;
                                    objRoute.Status = true;
                                    objRoute.Type = 1;
                                    objRoute.Uid = Convert.ToInt32(Session["UserID"]);
                                    objRoute.Date = DateTime.Now;
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Bus Route is updated successfully.";
                                }
                            }
                            else
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
                                    objRouteAdd.Date = DateTime.Now;
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

        [HttpGet]
        public ActionResult ViewBusRoute()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    BusRouteModelView objBusRouteModelView = new BusRouteModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        objBusRouteModelView.BusRouteModelList = (from route in dbEntities.tbl_BusLoc
                                                                  orderby route.Blid
                                                                  select new BusRouteModel
                                                                  {
                                                                      BL_ID = route.Blid,
                                                                      LOCATION_NAME = route.LocationName,
                                                                      STATUS = route.Status.ToString(),
                                                                      TYPE = route.Type.ToString(),
                                                                      DATE = route.Date.ToString(),
                                                                      LOCATION_XY = route.LocationXY ?? ""
                                                                  }).ToList();
                    }
                    return View(objBusRouteModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion

        #region Add, View and Edit Bus
        [HttpGet]
        public ActionResult ManageBus(int? busID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    BusModel objBusModel = new BusModel();
                    if (busID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objBus = (from bus in dbEntities.tbl_Bus
                                          where bus.Busid == busID
                                          select bus).FirstOrDefault();
                            if (objBus != null)
                            {
                                objBusModel.BUS_ID = busID ?? 0;
                                objBusModel.BUS_NAME = objBus.BusName;
                                objBusModel.BUS_NUMBER = objBus.BusNum;
                                objBusModel.TYPE = objBus.Type.ToString();
                                objBusModel.SEAT_COUNT = objBus.Seatcount.ToString();
                                objBusModel.ADULT_COST = objBus.CostsAdult.ToString();
                                objBusModel.CHILD_COST = objBus.CostsChild.ToString();
                                objBusModel.START_TIME = objBus.StartTime.ToString();
                            }

                            var objBusRoute = (from busRoute in dbEntities.tbl_BusRoute
                                               where busRoute.BusId == busID
                                               && busRoute.Status == true
                                               orderby busRoute.Sortorder
                                               select busRoute).ToList();
                            if (objBusRoute != null && objBusRoute.Count() > 0)
                            {
                                objBusModel.CHECK_ROUTES = objBusRoute.Select(x => x.BrId.ToString()).Distinct().ToArray();
                            }
                        }
                    }
                    return View(objBusModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
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
                            //TimeSpan time = TimeSpan.Parse(obj.START_TIME);
                            TimeSpan time = DateTime.Parse(obj.START_TIME).TimeOfDay;

                            using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                            {
                                if (obj.BUS_ID > 0)
                                {
                                    var objBus = (from bus in dbEntities.tbl_Bus
                                                  where bus.Busid == obj.BUS_ID
                                                  select bus).FirstOrDefault();
                                    if (objBus != null)
                                    {
                                        objBus.BusName = obj.BUS_NAME;
                                        objBus.BusNum = obj.BUS_NUMBER;
                                        objBus.Type = Convert.ToInt32(obj.TYPE);
                                        objBus.Seatcount = Convert.ToInt32(obj.SEAT_COUNT);
                                        objBus.CostsAdult = Convert.ToInt32(obj.ADULT_COST);
                                        objBus.CostsChild = Convert.ToInt32(obj.CHILD_COST);
                                        objBus.StartTime = time;
                                        objBus.Date = DateTime.Now;
                                        objBus.Status = true;
                                        dbEntities.SaveChanges();
                                    }

                                    var objBusRouteDelete = (from busRoute in dbEntities.tbl_BusRoute
                                                             where busRoute.BusId == obj.BUS_ID
                                                             select busRoute).ToList();
                                    if (objBusRouteDelete != null && objBusRouteDelete.Count() > 0)
                                    {
                                        dbEntities.tbl_BusRoute.RemoveRange(objBusRouteDelete);
                                        dbEntities.SaveChanges();
                                    }

                                    int sort_order = 1;
                                    foreach (var rid in obj.CHECK_ROUTES)
                                    {
                                        tbl_BusRoute objBusRouteAdd = new tbl_BusRoute();
                                        objBusRouteAdd.Blid = Convert.ToInt32(rid);
                                        objBusRouteAdd.BusId = obj.BUS_ID;
                                        objBusRouteAdd.Sortorder = sort_order;
                                        objBusRouteAdd.Status = true;
                                        objBusRouteAdd.Date = DateTime.Now;
                                        dbEntities.tbl_BusRoute.Add(objBusRouteAdd);
                                        dbEntities.SaveChanges();
                                        sort_order++;
                                    }

                                    obj.RESULT = "Entered Bus is updated successfully.";
                                }
                                else
                                {
                                    var objBus = (from bus in dbEntities.tbl_Bus
                                                  where bus.BusName.ToLower().Equals(obj.BUS_NAME.ToLower())
                                                  && bus.StartTime.ToString().Equals(obj.START_TIME)
                                                  && bus.BusNum.ToString().Equals(obj.BUS_NUMBER)
                                                  select bus).FirstOrDefault();
                                    if (objBus == null)
                                    {
                                        tbl_Bus objBusAdd = new tbl_Bus();
                                        objBusAdd.BusName = obj.BUS_NAME;
                                        objBusAdd.BusNum = obj.BUS_NUMBER;
                                        objBusAdd.Type = Convert.ToInt32(obj.TYPE);
                                        objBusAdd.Seatcount = Convert.ToInt32(obj.SEAT_COUNT);
                                        objBusAdd.CostsAdult = Convert.ToInt32(obj.ADULT_COST);
                                        objBusAdd.CostsChild = Convert.ToInt32(obj.CHILD_COST);
                                        objBusAdd.StartTime = time;
                                        objBusAdd.Status = true;
                                        objBusAdd.Date = DateTime.Now;
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
                                                objBusRouteAdd.Date = DateTime.Now;
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

        [HttpGet]
        public ActionResult ViewBus()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    BusModelView objBusModelView = new BusModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        var objBusList = (from buses in dbEntities.tbl_Bus
                                          select new
                                          {
                                              buses.Busid,
                                              buses.BusName,
                                              buses.Status,
                                              buses.Type,
                                              buses.Date,
                                              buses.StartTime,
                                              buses.BusNum,
                                              BusRoutes = (from busRoutes in dbEntities.tbl_BusRoute
                                                           join routes in dbEntities.tbl_BusLoc
                                                           on busRoutes.Blid equals routes.Blid
                                                           where busRoutes.BusId == buses.Busid
                                                           && busRoutes.Status == true
                                                           && routes.Status == true
                                                           orderby busRoutes.Sortorder
                                                           select new
                                                           {
                                                               routes.Blid,
                                                               routes.LocationName
                                                           }).Distinct().ToList()
                                          }).ToList();

                        objBusModelView.BusModelList = (from x in objBusList
                                                        select new BusModel
                                                        {
                                                            BUS_ID = x.Busid,
                                                            BUS_NAME = x.BusName,
                                                            STATUS = x.Status.ToString(),
                                                            TYPE = x.Type.ToString(),
                                                            TYPE_VALUE = Enum.GetName(typeof(Common.BusType), x.Type).Replace("_", " "),
                                                            DATE = x.Date.ToString(),
                                                            START_TIME = x.StartTime.ToString(),
                                                            BUS_NUMBER = x.BusNum.ToString(),
                                                            CHECK_ROUTES_VALUES = string.Join(",\n", x.BusRoutes.Select(y => y.Blid)),
                                                            CHECK_ROUTES = x.BusRoutes.Select(y => y.LocationName).ToArray()
                                                        }).ToList();
                    }
                    return View(objBusModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion

        #region Add, View and Edit Hotels
        [HttpGet]
        public ActionResult ManageHotel(int? hID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelModel objHotelModel = new HotelModel();
                    if (hID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objHotels = (from hotels in dbEntities.tbl_Hotels
                                             where hotels.Hid == hID
                                             select hotels).FirstOrDefault();
                            if (objHotels != null)
                            {
                                objHotelModel.HOTEL_NAME = objHotels.HotelName;
                                objHotelModel.TYPE = objHotels.Type.ToString();
                                objHotelModel.LOCATION = objHotels.Location;
                            }
                        }
                    }
                    return View(objHotelModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult ManageHotel(HotelModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            if (obj.HID > 0)
                            {
                                var objHotels = (from hotels in dbEntities.tbl_Hotels
                                                 where hotels.Hid == obj.HID
                                                 select hotels).FirstOrDefault();
                                if (objHotels != null)
                                {
                                    objHotels.HotelName = obj.HOTEL_NAME;
                                    objHotels.Type = Convert.ToInt32(obj.TYPE);
                                    objHotels.Location = obj.LOCATION;
                                    objHotels.Status = true;
                                    objHotels.Date = DateTime.Now;
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Hotel is updated successfully.";
                                }
                            }
                            else
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
                                    objHotelsAdd.Date = DateTime.Now;
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

        [HttpGet]
        public ActionResult ViewHotel()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelModelView objHotelModelView = new HotelModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        var objHotelModelList = (from hotels in dbEntities.tbl_Hotels
                                                 select new
                                                 {
                                                     hotels.Hid,
                                                     hotels.HotelName,
                                                     hotels.Type,
                                                     hotels.Location,
                                                     hotels.Status,
                                                     hotels.RoomCount
                                                 }).ToList();

                        objHotelModelView.HotelModelList = (from hotels in objHotelModelList
                                                            select new HotelModel
                                                            {
                                                                HID = hotels.Hid,
                                                                HOTEL_NAME = hotels.HotelName,
                                                                TYPE = hotels.Type.ToString(),
                                                                TYPE_VALUE = Enum.GetName(typeof(Common.HotelType), hotels.Type).Replace("_", " "),
                                                                LOCATION = hotels.Location,
                                                                STATUS = hotels.Status.ToString(),
                                                                ROOM_COUNT = hotels.RoomCount.ToString()
                                                            }).ToList();
                    }
                    return View(objHotelModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion

        #region Add, View and Edit Hotel Room
        [HttpGet]
        public ActionResult ManageHotelRoom(int? hotelRoomID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelRoomModel objHotelRoomModel = new HotelRoomModel();
                    if (hotelRoomID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objRooms = (from rooms in dbEntities.tbl_HotelRoom
                                            where rooms.HotelRoomId == hotelRoomID
                                            select rooms).FirstOrDefault();
                            if (objRooms != null)
                            {
                                objHotelRoomModel.HOTEL_ROOM_ID = hotelRoomID ?? 0;
                                objHotelRoomModel.HID = objRooms.Hid.ToString();
                                objHotelRoomModel.ROOM_TYPE_ID = objRooms.RoomTypeId.ToString();
                            }
                        }
                    }
                    return View(objHotelRoomModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
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
                            if (obj.HOTEL_ROOM_ID > 0)
                            {
                                var objHotelRoom = (from rooms in dbEntities.tbl_HotelRoom
                                                    where rooms.HotelRoomId == obj.HOTEL_ROOM_ID
                                                    select rooms).FirstOrDefault();
                                if (objHotelRoom != null)
                                {
                                    objHotelRoom.Hid = Convert.ToInt32(obj.HID);
                                    objHotelRoom.RoomTypeId = Convert.ToInt32(obj.ROOM_TYPE_ID);
                                    objHotelRoom.Status = true;
                                    objHotelRoom.Date = DateTime.Now;
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Hotel Room is updated successfully.";
                                }
                            }
                            else
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
                                    objHotelRoomAdd.Date = DateTime.Now;
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

        [HttpGet]
        public ActionResult ViewHotelRoom()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelRoomModelView objHotelRoomModelView = new HotelRoomModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        var objHotelRoomModelList = (from hotelRoom in dbEntities.tbl_HotelRoom
                                                     select new
                                                     {
                                                         hotelRoom.HotelRoomId,
                                                         hotelRoom.Hid,
                                                         HotelName = hotelRoom.tbl_Hotels.HotelName,
                                                         hotelRoom.RoomTypeId,
                                                         Room_Class = hotelRoom.tbl_RoomTypes.Room_Class,
                                                         Bed_Count = hotelRoom.tbl_RoomTypes.Bed_Count,
                                                         hotelRoom.Status
                                                     }).ToList();

                        objHotelRoomModelView.HotelRoomModelList = (from hotelRoom in objHotelRoomModelList
                                                                    select new HotelRoomModel
                                                                    {
                                                                        HOTEL_ROOM_ID = hotelRoom.HotelRoomId,
                                                                        HID = hotelRoom.Hid.ToString(),
                                                                        HOTEL_NAME = hotelRoom.HotelName,
                                                                        ROOM_TYPE_ID = hotelRoom.RoomTypeId.ToString(),
                                                                        ROOM_TYPE_VALUE = hotelRoom.Room_Class + " " + hotelRoom.Bed_Count,
                                                                        STATUS = hotelRoom.Status.ToString()
                                                                    }).ToList();
                    }
                    return View(objHotelRoomModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion

        #region Add, View and Edit Hotel Room types
        [HttpGet]
        public ActionResult ManageHotelRoomType(int? roomTypeID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelRoomTypeModel objHotelRoomTypeModel = new HotelRoomTypeModel();

                    if (roomTypeID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objRoomType = (from roomtype in dbEntities.tbl_RoomTypes
                                               where roomtype.RoomTypeId == roomTypeID
                                               select roomtype).FirstOrDefault();
                            if (objRoomType == null)
                            {
                                objHotelRoomTypeModel.ROOM_TYPE_ID = roomTypeID ?? 0;
                                objHotelRoomTypeModel.ROOM_CLASS = objRoomType.Room_Class;
                                objHotelRoomTypeModel.BED_COUNT = objRoomType.Bed_Count.ToString();
                            }
                        }
                    }

                    return View(objHotelRoomTypeModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
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
                            if (obj.ROOM_TYPE_ID > 0)
                            {
                                var objRoomType = (from roomtype in dbEntities.tbl_RoomTypes
                                                   where roomtype.RoomTypeId == obj.ROOM_TYPE_ID
                                                   select roomtype).FirstOrDefault();
                                if (objRoomType != null)
                                {
                                    objRoomType.Room_Class = obj.ROOM_CLASS;
                                    objRoomType.Bed_Count = Convert.ToInt32(obj.BED_COUNT);
                                    objRoomType.Status = true;
                                    objRoomType.Date = DateTime.Now;
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Hotel Room Type is updated successfully.";
                                }
                            }
                            else
                            {
                                var objRoomType = (from roomtype in dbEntities.tbl_RoomTypes
                                                   where roomtype.Room_Class.ToLower().Equals(obj.ROOM_CLASS.ToLower())
                                                   && roomtype.Bed_Count.ToString().Equals(obj.BED_COUNT)
                                                   select roomtype).FirstOrDefault();
                                if (objRoomType == null)
                                {
                                    tbl_RoomTypes objRoomTypeAdd = new tbl_RoomTypes();
                                    objRoomTypeAdd.Room_Class = obj.ROOM_CLASS;
                                    objRoomTypeAdd.Bed_Count = Convert.ToInt32(obj.BED_COUNT);
                                    objRoomTypeAdd.Status = true;
                                    objRoomTypeAdd.Date = DateTime.Now;
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

        [HttpGet]
        public ActionResult ViewHotelRoomType()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    HotelRoomTypeModelView objHotelRoomTypeModelView = new HotelRoomTypeModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        objHotelRoomTypeModelView.HotelRoomTypeModelList = (from hotelRoomType in dbEntities.tbl_RoomTypes
                                                                            select new HotelRoomTypeModel
                                                                            {
                                                                                ROOM_TYPE_ID = hotelRoomType.RoomTypeId,
                                                                                ROOM_CLASS = hotelRoomType.Room_Class,
                                                                                BED_COUNT = hotelRoomType.Bed_Count.ToString()
                                                                            }).ToList();
                    }
                    return View(objHotelRoomTypeModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion

        #region Comapartment
        [HttpGet]
        public ActionResult ManageCompartment(int? compID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    CompartmentModel objCompartmentModel = new CompartmentModel();
                    if (compID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objcomp = (from compartment in dbEntities.tbl_Compartment
                                           where compartment.Compid == compID
                                           select compartment).FirstOrDefault();
                            if (objcomp != null)
                            {
                                objCompartmentModel.COMP_ID = compID ?? 0;
                                objCompartmentModel.COMP_NAME = objcomp.CompName;
                                objCompartmentModel.TYPE = objcomp.Type.ToString();
                                objCompartmentModel.TRAIN_ID = objcomp.Tid.ToString();
                            }
                        }
                    }
                    return View(objCompartmentModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult ManageCompartment(CompartmentModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            if (obj.COMP_ID > 0)
                            {
                                var objcompartment = (from comp in dbEntities.tbl_Compartment
                                                      where comp.Compid == obj.COMP_ID
                                                      select comp).FirstOrDefault();
                                if (objcompartment != null)
                                {
                                    objcompartment.CompName = obj.COMP_NAME;
                                    objcompartment.Type = Convert.ToInt32(obj.TYPE);
                                    objcompartment.Status = true;
                                    objcompartment.Tid = Convert.ToInt32(obj.TRAIN_ID);
                                    objcompartment.Uid = Convert.ToInt32(Session["UserID"]);
                                    objcompartment.Date = DateTime.Now;
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Compartment is updated successfully.";
                                }
                            }
                            else
                            {
                                var objcompartment = (from comp in dbEntities.tbl_Compartment
                                                      where comp.CompName.ToLower().Equals(obj.COMP_NAME.ToLower())
                                                      && comp.Type.ToString().Equals(obj.TYPE)
                                                      select comp).FirstOrDefault();
                                if (objcompartment == null)
                                {
                                    tbl_Compartment objcompartmentAdd = new tbl_Compartment();
                                    objcompartmentAdd.CompName = obj.COMP_NAME;
                                    objcompartmentAdd.Type = Convert.ToInt32(obj.TYPE);
                                    objcompartmentAdd.Status = true;
                                    objcompartmentAdd.Tid = Convert.ToInt32(obj.TRAIN_ID);
                                    objcompartmentAdd.Uid = Convert.ToInt32(Session["UserID"]);
                                    objcompartmentAdd.Date = DateTime.Now;
                                    dbEntities.tbl_Compartment.Add(objcompartmentAdd);
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Compartment is inserted successfully.";
                                }
                                else
                                {
                                    obj.RESULT = "Entered Compartment is already available.";
                                }
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

        public ActionResult ViewCompartment()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    CompartmentModelView objCompartmentModelView = new CompartmentModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        var objCompartmentModelList = (from compartment in dbEntities.tbl_Compartment
                                                       select new
                                                       {
                                                           compartment.Compid,
                                                           compartment.CompName,
                                                           compartment.Type,
                                                           compartment.Status,
                                                           compartment.Tid,
                                                           TrainName = compartment.tbl_Train.TrainName,
                                                           compartment.Date
                                                       }).ToList();

                        objCompartmentModelView.CompartmentModelList = (from compartment in objCompartmentModelList
                                                                        select new CompartmentModel
                                                                        {
                                                                            COMP_ID = compartment.Compid,
                                                                            COMP_NAME = compartment.CompName,
                                                                            TYPE = compartment.Type.ToString(),
                                                                            TYPE_VALUE = Enum.GetName(typeof(Common.ComaprtmentType), compartment.Type).Replace("_", " "),
                                                                            STATUS = compartment.Status.ToString(),
                                                                            TRAIN_ID = compartment.Tid.ToString(),
                                                                            TRAIN_NAME = compartment.TrainName,
                                                                            DATE = compartment.Date.ToString()
                                                                        }).ToList();
                    }
                    return View(objCompartmentModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion

        #region Seat Class
        [HttpGet]
        public ActionResult ManageSeatClass(int? seatID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    SeatModel objSeatModel = new SeatModel();
                    if (seatID > 0)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objseat = (from seat in dbEntities.tbl_SeatClass
                                           where seat.Sid == seatID
                                           select seat).FirstOrDefault();
                            if (objseat != null)
                            {
                                objSeatModel.SEAT_ID = seatID ?? 0;
                                objSeatModel.SEAT_NAME = objseat.SName;
                                objSeatModel.TYPE = objseat.Type.ToString();
                                objSeatModel.COUNT = objseat.Count.ToString();
                                objSeatModel.COMP_ID = objseat.Compid.ToString();
                                objSeatModel.ADULT_COST = objseat.CostsAdult.ToString();
                                objSeatModel.CHILD_COST = objseat.CostsChild.ToString();
                            }
                        }
                    }
                    return View(objSeatModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult ManageSeatClass(SeatModel obj)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            if (obj.SEAT_ID > 0)
                            {
                                var objseat = (from seat in dbEntities.tbl_SeatClass
                                               where seat.Sid == obj.SEAT_ID
                                               select seat).FirstOrDefault();
                                if (objseat != null)
                                {
                                    objseat.SName = obj.SEAT_NAME;
                                    objseat.Type = Convert.ToInt32(obj.TYPE);
                                    objseat.Count = Convert.ToInt32(obj.COUNT);
                                    objseat.Compid = Convert.ToInt32(obj.COMP_ID);
                                    objseat.CostsAdult = Convert.ToInt32(obj.ADULT_COST);
                                    objseat.CostsChild = Convert.ToInt32(obj.CHILD_COST);
                                    objseat.Status = true;
                                    objseat.Date = DateTime.Now;
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Seat Class is updated successfully.";
                                }
                            }
                            else
                            {
                                var objseat = (from seat in dbEntities.tbl_SeatClass
                                               where seat.SName.ToLower().Equals(obj.SEAT_NAME.ToLower())
                                               select seat).FirstOrDefault();
                                if (objseat == null)
                                {
                                    tbl_SeatClass objSeatAdd = new tbl_SeatClass();
                                    objSeatAdd.SName = obj.SEAT_NAME;
                                    objSeatAdd.Type = Convert.ToInt32(obj.TYPE);
                                    objSeatAdd.Count = Convert.ToInt32(obj.COUNT);
                                    objSeatAdd.Compid = Convert.ToInt32(obj.COMP_ID);
                                    objSeatAdd.CostsAdult = Convert.ToDecimal(obj.ADULT_COST);
                                    objSeatAdd.CostsChild = Convert.ToDecimal(obj.CHILD_COST);
                                    objSeatAdd.Status = true;
                                    objSeatAdd.Date = DateTime.Now;
                                    dbEntities.tbl_SeatClass.Add(objSeatAdd);
                                    dbEntities.SaveChanges();

                                    obj.RESULT = "Entered Seat class is inserted successfully.";
                                }
                                else
                                {
                                    obj.RESULT = "Entered Seat class is already available.";
                                }
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

        public ActionResult ViewSeatClass()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    SeatModelView objSeatModelView = new SeatModelView();
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        var objSeatModelList = (from seat in dbEntities.tbl_SeatClass
                                                select new
                                                {
                                                    seat.Sid,
                                                    seat.SName,
                                                    seat.Type,
                                                    seat.Count,
                                                    seat.Compid,
                                                    CompName = seat.tbl_Compartment.CompName,
                                                    seat.CostsAdult,
                                                    seat.CostsChild
                                                }).ToList();

                        objSeatModelView.SeatModelList = (from seat in objSeatModelList
                                                          select new SeatModel
                                                          {
                                                              SEAT_ID = seat.Sid,
                                                              SEAT_NAME = seat.SName,
                                                              TYPE = seat.Type.ToString(),
                                                              TYPE_VALUE = Enum.GetName(typeof(Common.SeatClassType), seat.Type).Replace("_", " "),
                                                              COUNT = seat.Count.ToString(),
                                                              COMP_ID = seat.Compid.ToString(),
                                                              COMPARTMENT_NAME = seat.CompName,
                                                              ADULT_COST = seat.CostsAdult.ToString(),
                                                              CHILD_COST = seat.CostsChild.ToString()
                                                          }).ToList();
                    }
                    return View(objSeatModelView);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home", new { area = "" });
            }
        }
        #endregion
    }
}