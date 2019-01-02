using ClickOnIndia.App_Start;
using ClickOnIndia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClickOnIndia.Controllers
{
    public class HomeController : Controller
    {

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            LoginModel objLoginModel = new LoginModel();
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View(objLoginModel);
        }

        [HttpPost]
        public ActionResult Login(LoginModel obj  )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                    {
                        var objUserUN = (from user in dbEntities.tbl_UserDetail
                                         where user.UserName.ToLower().Equals(obj.USER_NAME.ToLower())
                                         select new { user.Uid, user.Status }).FirstOrDefault();
                        if (objUserUN != null)
                        {
                            if (objUserUN.Status == true)
                            {
                                var objUserPWD = (from user in dbEntities.tbl_UserDetail
                                                  where user.UserName.ToLower().Equals(obj.USER_NAME.ToLower())
                                                  && user.Password.ToLower().Equals(obj.PASSWORD.ToLower())
                                                  select new { user.Uid }).FirstOrDefault();
                                if (objUserPWD != null)
                                {
                                    var objUser = (from user in dbEntities.tbl_UserDetail
                                                   join role in dbEntities.tbl_Role
                                                   on user.Rid equals role.Rid
                                                   where user.UserName.ToLower().Equals(obj.USER_NAME.ToLower())
                                                   && user.Password.ToLower().Equals(obj.PASSWORD.ToLower())
                                                   select new
                                                   {
                                                       user.UserName,
                                                       user.Uid,
                                                       role.Rid,
                                                       role.RoleName
                                                   }).FirstOrDefault();
                                    if (objUser != null)
                                    {
                                        Session["UserID"] = objUser.Uid;
                                        Session["RoleID"] = objUser.Rid;
                                        Session["UserName"] = objUser.Rid;

                                        if (objUser.RoleName.ToLower() == "admin")
                                        {
                                            return RedirectToAction("Index", "Admin", new { area = "Admin" });
                                        }
                                        else if (objUser.RoleName.ToLower() == "passenger")
                                        {
                                            return RedirectToAction("Index", "Home");
                                        }
                                    }
                                }
                                else
                                {
                                    obj.RESULT = "Entered Password is not correct";
                                }
                            }
                            else
                            {
                                obj.RESULT = "User Name is exist, please activate your profile.";
                            }
                        }
                        else
                        {
                            obj.RESULT = "User Name not exist, please signup into our site.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj.RESULT = "Error occured, please contact adminstrator.";
            }
            return View(obj);
        }


        public ActionResult UserLogin(LoginModel obj)
        {
            int val = 0;
            using (Db_ClickOnIndiaEntities db = new Db_ClickOnIndiaEntities())
            {
                var ud = (from r in db.tbl_Role
                          join u in db.tbl_UserDetail on r.Rid equals u.Rid
                          where u.UserName.Equals(obj.USER_NAME) && u.Password.Equals(obj.PASSWORD) && r.Rid == 2
                          select u
                      ).FirstOrDefault();
                if (ud != null)
                {
                    Session["UserID"] = ud.Uid;
                    Session["RoleID"] = ud.Rid;
                    val = 1;
                }
                else
                {
                    val = 0;
                }
                //Session["UserName"] = objUser.Rid;


            }

            return Json(val, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                Session.Abandon();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Login", "Home");
        }
        #endregion

        #region Passenger
        public ActionResult Index()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        #endregion


        #region UserRegister

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tbl_UserDetail obj)
        {
            try
            {
                using (Db_ClickOnIndiaEntities db = new Models.Db_ClickOnIndiaEntities())
                {
                    if (ModelState.IsValid)
                    {
                        obj.Rid = 2;
                        obj.Status = true;

                        db.tbl_UserDetail.Add(obj);
                        db.SaveChanges();
                        ViewBag.Msg = "User Created Successfully";
                    }


                }
                return View(new tbl_UserDetail());
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("unique"))
                    ViewBag.Msg = "UserName must be Unique";

                return View();
            }
        }
        #endregion

        #region TrainLocation

        public ActionResult GetTrainLoc(string q)
        {
            using (Db_ClickOnIndiaEntities db = new Db_ClickOnIndiaEntities())
            {
                var list = db.tbl_Route.Where(x => x.LocationName.Contains(q)).Select(x => new { Name = x.LocationName, Roid = x.Roid }).ToList();
                //var js = JsonConvert.SerializeObject(list);
                return Json(list, JsonRequestBehavior.AllowGet);

            }
        }



        [HttpPost]
        public ActionResult SearchBookingPost(AllBookingPlan obj)
        {

            using (Db_ClickOnIndiaEntities db = new Db_ClickOnIndiaEntities())
            {
                var routlist = (from t in db.tbl_Train
                                    //join tl in TrainIds on t.Tid equals tl.Tid
                                join tr in db.tbl_TrainRoute on t.Tid equals tr.Tid
                                join r in db.tbl_Route on tr.Roid equals r.Roid
                                join c in db.tbl_Compartment on t.Tid equals c.Tid
                                join s in db.tbl_SeatClass on c.Compid equals s.Sid
                                where tr.Roid == obj.TrainBusBookingPlan.fromLocId || tr.Roid == obj.TrainBusBookingPlan.toLocId && c.Status == true
                                select new
                                {
                                    TrainId = t.Tid,
                                    TrianName = t.TrainName,
                                    TrainNumber = t.TrainNum,
                                    StartTime = t.StartTime,
                                    Sort = tr.SortOrder,
                                    RouteName = r.LocationName,
                                    RouteId = r.Roid,
                                    AdultCost = s.CostsAdult,
                                    ChildCost = s.CostsChild,
                                    CompartmentId = c.Compid,
                                    CompartmentName = c.CompName,
                                    SeatId = s.Sid,
                                    ClassName = s.SName,
                                    SeatCount = s.Count,

                                }
                              ).OrderByDescending(x => x.Sort).ToList();


                var results = (from r in routlist
                               group r by new
                               {
                                   r.TrainId
                               }
                               into g
                               select new
                               {
                                   TrainId = g.Key.TrainId,
                                   TrainName = g.Select(x => x.TrianName).FirstOrDefault(),
                                   TrainNumber = g.Select(x => x.TrainNumber).FirstOrDefault(),
                                   StartTime = g.Select(x => x.StartTime).FirstOrDefault(),
                                   Routes = g.Select(x => new { x.RouteName, x.RouteId, x.Sort }).OrderBy(x => x.Sort).ToList(),
                                   Seats = g.Select(x => new { x.ClassName, x.SeatCount, x.SeatId }).Distinct().ToList()

                               }).ToList();

                List<TrainSeatAvaliabity> trainlists = new List<TrainSeatAvaliabity>();
                foreach (var item in results)
                {
                    if (item.Routes.Count > 1)
                    {
                        // var foo = item.Routes.Where(x => x.RouteId == obj.TrainBusBookingPlan.fromLocId).FirstOrDefault();
                        if (item.Routes[0].RouteId == obj.TrainBusBookingPlan.fromLocId)
                        {
                            TrainSeatAvaliabity TrainSeatAvaliabity = new TrainSeatAvaliabity();

                            TrainSeatAvaliabity.fromId = obj.TrainBusBookingPlan.fromLocId.ToString();
                            TrainSeatAvaliabity.toId = obj.TrainBusBookingPlan.toLocId.ToString();

                            TrainSeatAvaliabity.StartTime = item.StartTime.Value.ToString();
                            TrainSeatAvaliabity.Tid = item.TrainId.ToString();
                            TrainSeatAvaliabity.TrainNum = item.TrainNumber;
                            TrainSeatAvaliabity.TrainName = item.TrainName;
                            TrainSeatAvaliabity.StartTime = item.StartTime.Value.ToString();
                            TrainSeatAvaliabity.JourDate = obj.TrainBusBookingPlan.fromDate;
                            DateTime jd = new DateTime();
                            jd = Convert.ToDateTime(obj.TrainBusBookingPlan.fromDate);
                            var routes = db.tbl_TrainRoute.Where(x => x.Tid == item.TrainId && x.Roid >= obj.TrainBusBookingPlan.fromLocId && x.Roid <= obj.TrainBusBookingPlan.toLocId).OrderBy(x => x.SortOrder).ToList();
                            TimeSpan t = item.StartTime.Value;
                            DateTime sdt = new DateTime(jd.Year, jd.Month, jd.Day, t.Hours, t.Minutes, 0);
                            TrainSeatAvaliabity.Departure = item.StartTime.Value.ToString();
                            for (int r = 0; r < routes.Count; r++)
                            {

                                if (r != 0)
                                {
                                    // if ( (r + 1) <= routes.Count)
                                    sdt = sdt.AddMinutes(routes[r].TimeBet ?? 0);
                                }
                            }

                            TrainSeatAvaliabity.Arrival = sdt.ToString();


                            List<TrainSeatAvaliabityCount> TrainSeatAvaliabityCounts = new List<TrainSeatAvaliabityCount>();

                            foreach (var s in item.Seats)
                            {
                                TrainSeatAvaliabityCounts.Add(new TrainSeatAvaliabityCount { Count = s.SeatCount.ToString(), Type = s.ClassName, Avialiable = "", SeatId = s.SeatId.ToString() });

                            }

                            TrainSeatAvaliabity.TrainSeatAvaliabityCounts = TrainSeatAvaliabityCounts;

                            trainlists.Add(TrainSeatAvaliabity);


                        }
                    }
                }
                ViewBag.Avali = trainlists;
                return View("SearchBooking");

            }


        }


        #endregion

    }
}