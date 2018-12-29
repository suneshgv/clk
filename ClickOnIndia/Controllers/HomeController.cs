using ClickOnIndia.App_Start;
using ClickOnIndia.Models;
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

        public  ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tbl_UserDetail obj)
        {
            using (Db_ClickOnIndiaEntities db= new Models.Db_ClickOnIndiaEntities())
            {
                obj.Rid = (int)Common.UserTypes.User;

              

            }
                return View();
        }
        #endregion

    }
}