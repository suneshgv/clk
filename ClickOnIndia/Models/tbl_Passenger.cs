//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClickOnIndia.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Passenger
    {
        public int Pid { get; set; }
        public string PassName { get; set; }
        public Nullable<int> PassAge { get; set; }
        public string PassGender { get; set; }
        public Nullable<int> BusBookId { get; set; }
        public Nullable<int> TrainBookId { get; set; }
        public Nullable<int> BusSeatNo { get; set; }
        public Nullable<int> TrainSeatNo { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual tbl_BusBoking tbl_BusBoking { get; set; }
        public virtual tbl_TrainBooking tbl_TrainBooking { get; set; }
    }
}
