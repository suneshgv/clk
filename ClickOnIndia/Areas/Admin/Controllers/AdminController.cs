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

        #region Route
        [HttpGet]
        public ActionResult ManageRoute()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                try
                {
                    RouteModel objRouteModel = new RouteModel();
                    return View(objRouteModel);
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
        public ActionResult ManageRoute(RouteModel obj)
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
                                tbl_Route objAdd = new tbl_Route();
                                objAdd.LocationName = obj.LOCATION_NAME;
                                objAdd.Status = true;
                                objAdd.Type = 1;
                                dbEntities.tbl_Route.Add(objAdd);
                                dbEntities.SaveChanges();

                                obj.RESULT = "Entered location is inserted successfully.";
                            }
                            else
                            {
                                obj.RESULT = "Entered location is already available.";
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
                        using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                        {
                            var objTrian = (from train in dbEntities.tbl_Train
                                            where train.TrainName.ToLower().Equals(obj.TRAIN_NAME.ToLower())
                                            && train.StartTime.ToString().Equals(obj.START_TIME)
                                            select train).FirstOrDefault();
                            if (objTrian == null)
                            {
                                TimeSpan time = TimeSpan.Parse(obj.START_TIME);

                                tbl_Train objTrainAdd = new tbl_Train();
                                objTrainAdd.TrainName = obj.TRAIN_NAME;
                                objTrainAdd.StartTime = time;
                                objTrainAdd.TrainName = obj.TRAIN_NUM;
                                objTrainAdd.Status = true;
                                objTrainAdd.Type = Convert.ToInt32(obj.TYPE);
                                dbEntities.tbl_Train.Add(objTrainAdd);
                                dbEntities.SaveChanges();
                                int Trid = objTrainAdd.Tid;

                                if (obj.CHECK_ROUTES!=null && obj.CHECK_ROUTES.Length > 0)
                                {
                                    int sort_order = 1;
                                    foreach(var rid in obj.CHECK_ROUTES)
                                    {
                                        tbl_TrainRoute objTrainRouteAdd = new tbl_TrainRoute();
                                        objTrainRouteAdd.Roid = Convert.ToInt32(rid);
                                        objTrainRouteAdd.Trid = Trid;
                                        objTrainRouteAdd.SortOrder = sort_order;
                                        objTrainRouteAdd.Status = true;
                                        dbEntities.tbl_TrainRoute.Add(objTrainRouteAdd);
                                        dbEntities.SaveChanges();
                                        sort_order++;
                                    }                                    
                                }

                                obj.RESULT = "Entered train is inserted successfully.";
                            }
                            else
                            {
                                obj.RESULT = "Entered train is already available.";
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
    }
}