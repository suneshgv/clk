using ClickOnIndia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClickOnIndia.App_Start
{
    public class Common
    {
        public List<ListModel> GetRoutes()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                {
                    objListModel = (from route in dbEntities.tbl_Route
                                    where route.Status == true
                                    orderby route.LocationName
                                    select new ListModel
                                    {
                                        ID = route.Roid.ToString(),
                                        VALUE = route.LocationName
                                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetTrainTypes()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                objListModel = ((TrainType[])Enum.GetValues(typeof(TrainType))).Select(c => new ListModel() { ID = ((int)c).ToString(), VALUE = c.ToString().Replace("_"," ") }).ToList();
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public enum TrainType
        {
            Passenger,
            Express,
            Super_Fast
        }
       
    }
}