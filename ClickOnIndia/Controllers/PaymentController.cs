using ClickOnIndia.App_Start;
using ClickOnIndia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClickOnIndia.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        [HttpPost]
        public ActionResult SearchBookingConfirm(FormCollection fc)
        {

            try
            {
                using (Db_ClickOnIndiaEntities db = new Db_ClickOnIndiaEntities())
                {
                    int fromId = Convert.ToInt32(fc["fromId"]);
                    int toId = Convert.ToInt32(fc["toId"]);
                    int tId = Convert.ToInt32(fc["tId"]);
                    int sId = Convert.ToInt32(fc["sId"]);
                    DateTime jourDate = Convert.ToDateTime(fc["jourDate"]);

                    string[] passN = fc["Pname"].Split(',');
                    string[] passG = fc["Pgen"].Split(',');
                    string[] passA = fc["Page"].Split(',');

                    int passCnt = passN.Length;

                    tbl_SeatClass seat = db.tbl_SeatClass.Where(x => x.Sid == sId).FirstOrDefault();

                    decimal totCost = seat.CostsAdult ?? 0 * passCnt;

                    PayU payu = new PayU();
                    string PayuKey = ConfigurationManager.AppSettings["PayuKey"];
                    string PayuSalt = ConfigurationManager.AppSettings["PayuSalt"];
                    string PayuUrl = ConfigurationManager.AppSettings["PayuUrl"];
                    string PayuSuccessUrl = ConfigurationManager.AppSettings["PayuSuccessUrl"];
                    string PayuFailUrl = ConfigurationManager.AppSettings["PayuFailUrl"];

                    tbl_Transaction tr = new tbl_Transaction();
                    tr.Price = totCost;
                    db.tbl_Transaction.Add(tr);
                    db.SaveChanges();

                    tbl_TrainBooking tb = new tbl_TrainBooking();
                    tb.from_Trid = fromId;
                    tb.to_Trid = toId;
                    tb.Tid = tId;
                    tb.Sid = sId;
                    tb.JourneyDate = jourDate;
                    tb.Status = true;

                    List<tbl_Passenger> passs = new List<tbl_Passenger>();
                    for (int i = 0; i < passN.Length; i++)
                    {

                        tbl_Passenger pa = new tbl_Passenger();
                        pa.PassName = passN[i];
                        pa.PassGender = passG[i];
                        pa.PassAge = Convert.ToInt32(passA[i]);
                        passs.Add(pa);
                    }

                    tb.tbl_Passenger = passs;

                    Session["passDet"] = tb;

                    int id = tr.Transid;
                    Session["trainId"] = id;
                    string strForm = payu.CheckOut(PayuKey, PayuSalt, PayuUrl, PayuSuccessUrl, PayuFailUrl, Convert.ToDouble(totCost.ToString()), id.ToString(), "username",
                     "Clickonindia", "suneshgv@gmail.com", "9809998204");
                    // Page.Controls.Add(new LiteralControl(strForm));
                    ViewBag.str = strForm;
                    return View("postpayu");
                }

            }
            catch (Exception ex)
            {
                return View();

            }
         

        }

        public ActionResult Success(FormCollection fr)
        {
            using (Db_ClickOnIndiaEntities db = new Db_ClickOnIndiaEntities())
            {
                int tranId = Convert.ToInt32(Session["trainId"]);
                tbl_Transaction tr = db.tbl_Transaction.Where(x => x.Transid == tranId).FirstOrDefault();
                tr.Price = Convert.ToDecimal(fr["amount"]);
                tr.Ran_Transid = fr["txnid"];
                tr.Status = true;
                tr.StatusMsg = fr["status"];
                db.SaveChanges();

                tbl_TrainBooking tb = new tbl_TrainBooking();
                tb = (tbl_TrainBooking)Session["passDet"];
                tb.Transid = tranId;

                //var tbl_Passengers = db.tbl_TrainBooking.Where(x => x.JourneyDate == tb.JourneyDate && x.Status == true && x.Tid==tb.Tid && x.Sid==tb.Sid).Select(x => x.tbl_Passenger).FirstOrDefault();
                var tbl_Passengers = db.tbl_Passenger.Where(x => x.tbl_TrainBooking.JourneyDate == tb.JourneyDate && x.tbl_TrainBooking.Status == true && x.tbl_TrainBooking.Sid == tb.Sid && x.tbl_TrainBooking.Tid == tb.Tid).ToList();
                tbl_Compartment comp = db.tbl_SeatClass.Where(x => x.Sid == tb.Sid).Select(x => x.tbl_Compartment).FirstOrDefault();

                string lastPassS = null;
                if (tbl_Passengers != null)
                {
                    lastPassS = tbl_Passengers.Where(x => x.Status == true).OrderByDescending(x => x.Pid).Select(x => x.TrainSeatNo).FirstOrDefault();
                }
                if (lastPassS != null)
                {
                    string[] s = lastPassS.Split('_');
                    string sNo = s[1];
                    string cName = s[0];
                    int sno = Convert.ToInt32(s[1]);
                    int x = sno;
                    foreach (var p in tb.tbl_Passenger)
                    {
                       
                        x += 1;
                        p.TrainSeatNo = cName + "_" + x;
                        p.Status = true;
                    }
                }
                else
                {
                    string sNo = "1";
                    string cName = comp.CompName;
                    int sno = Convert.ToInt32(sNo);
                    int x = sno;
                    foreach (var p in tb.tbl_Passenger)
                    {
                       
                        x += 1;
                        p.TrainSeatNo = cName + "_" + x;
                        p.Status = true;
                    }
                }


                db.tbl_TrainBooking.Add(tb);
                db.SaveChanges();

            }
            return View();
        }
        public ActionResult Failuer(FormCollection fr)
        {
            using (Db_ClickOnIndiaEntities db = new Db_ClickOnIndiaEntities())
            {
                int tranId = Convert.ToInt32(Session["trainId"]);
                tbl_Transaction tr = db.tbl_Transaction.Where(x => x.Transid == tranId).FirstOrDefault();
                tr.Price = Convert.ToDecimal(fr["amount"]);
                tr.Ran_Transid = fr["txnid"];
                tr.Status = false;
                tr.StatusMsg = fr["status"];
                db.SaveChanges();
            }
            return View();
        }
    }
}