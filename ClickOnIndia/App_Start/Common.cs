﻿using ClickOnIndia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClickOnIndia.App_Start
{
    public class Common
    {
        public List<ListModel> GetTimes()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today.AddDays(1);//.AddHours(-1);

                for (DateTime date = start; date < end; date = date.AddHours(1))
                {
                    objListModel.Add(new ListModel
                    {
                        ID = date.ToShortTimeString(),
                        VALUE = date.ToShortTimeString()
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetTrainRoutes()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                {
                    objListModel = (from data in dbEntities.tbl_Route
                                    where data.Status == true
                                    orderby data.LocationName
                                    select new ListModel
                                    {
                                        ID = data.Roid.ToString(),
                                        VALUE = data.LocationName
                                    }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetBusRoutes()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                {
                    objListModel = (from data in dbEntities.tbl_BusLoc
                                    where data.Status == true
                                    orderby data.LocationName
                                    select new ListModel
                                    {
                                        ID = data.Blid.ToString(),
                                        VALUE = data.LocationName
                                    }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetHotels()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                {
                    objListModel = (from data in dbEntities.tbl_Hotels
                                    where data.Status == true
                                    orderby data.HotelName
                                    select new ListModel
                                    {
                                        ID = data.Hid.ToString(),
                                        VALUE = data.HotelName
                                    }).Distinct().ToList();
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
                objListModel = ((TrainType[])Enum.GetValues(typeof(TrainType))).Select(c => new ListModel() { ID = ((int)c).ToString(), VALUE = c.ToString().Replace("_", " ") }).ToList();
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetBusTypes()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                objListModel = ((BusType[])Enum.GetValues(typeof(BusType))).Select(c => new ListModel() { ID = ((int)c).ToString(), VALUE = c.ToString().Replace("_", " ") }).ToList();
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetHotelType()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                objListModel = ((HotelType[])Enum.GetValues(typeof(HotelType))).Select(c => new ListModel()
                {
                    ID = ((int)c).ToString(),
                    VALUE = c.ToString().Replace("_", " ")
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetHotelRoomType()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                {
                    objListModel = (from data in dbEntities.tbl_RoomTypes
                                    where data.Status == true
                                    orderby data.RoomTypeId
                                    select new ListModel
                                    {
                                        ID = data.RoomTypeId.ToString(),
                                        VALUE = data.Room_Class + " with " + data.Bed_Count + " beds"
                                    }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

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

        public List<ListModel> GetTrains()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                {
                    objListModel = (from data in dbEntities.tbl_Train
                                    where data.Status == true
                                    orderby data.TrainName
                                    select new ListModel
                                    {
                                        ID = data.Tid.ToString(),
                                        VALUE = data.TrainName
                                    }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetCompartments()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                using (Db_ClickOnIndiaEntities dbEntities = new Db_ClickOnIndiaEntities())
                {
                    objListModel = (from data in dbEntities.tbl_Compartment
                                    where data.Status == true
                                    orderby data.CompName
                                    select new ListModel
                                    {
                                        ID = data.Compid.ToString(),
                                        VALUE = data.CompName
                                    }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetCompartmentType()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                objListModel = ((ComaprtmentType[])Enum.GetValues(typeof(ComaprtmentType))).Select(c => new ListModel() { ID = ((int)c).ToString(), VALUE = c.ToString().Replace("_", " ") }).ToList();
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public List<ListModel> GetSeatClassType()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                objListModel = ((SeatClassType[])Enum.GetValues(typeof(SeatClassType))).Select(c => new ListModel() { ID = ((int)c).ToString(), VALUE = c.ToString().Replace("_", " ") }).ToList();
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }



        //public List<ListModel> GetTrainTypes()
        //{
        //    List<ListModel> objListModel = new List<ListModel>();
        //    try
        //    {
        //        objListModel = ((TrainType[])Enum.GetValues(typeof(TrainType))).Select(c => new ListModel() { ID = ((int)c).ToString(), VALUE = c.ToString().Replace("_"," ") }).ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return objListModel;
        //}


        public enum TrainType
        {
            Passenger,
            Express,
            Super_Fast
        }

        public enum BusType
        {
            Express,
            Super_Luxury,
            Garuda
        }
        public List<ListModel> GetTrainClassType()
        {
            List<ListModel> objListModel = new List<ListModel>();
            try
            {
                objListModel = ((TrainType[])Enum.GetValues(typeof(TrainClassType))).Select(c => new ListModel() { ID = ((int)c).ToString(), VALUE = c.ToString().Replace("_", " ") }).ToList();
            }
            catch (Exception ex)
            {

            }
            return objListModel;
        }

        public enum HotelType
        {
            Five_Star,
            Three_Star,
            Normal
        }
        public enum TrainClassType
        {
            Sleeper,
            Ac,
            Chair_Car
        }

        public enum SeatClassType
        {
            First,
            Second
        }

        public enum ComaprtmentType
        {
            Ac,
            NonAc,
            General
        }
    }
}