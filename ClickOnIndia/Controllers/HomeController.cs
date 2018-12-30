using ClickOnIndia.App_Start;
using ClickOnIndia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Login(LoginModel obj)
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




        #endregion

    }
}