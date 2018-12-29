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
    
    public partial class tbl_TrainBooking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_TrainBooking()
        {
            this.tbl_Passenger = new HashSet<tbl_Passenger>();
        }
    
        public int Bid { get; set; }
        public Nullable<int> Uid { get; set; }
        public Nullable<int> Tid { get; set; }
        public Nullable<int> from_Trid { get; set; }
        public Nullable<int> to_Trid { get; set; }
        public Nullable<int> Sid { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> Transid { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> CancelStatus { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime JourneyDate { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Passenger> tbl_Passenger { get; set; }
        public virtual tbl_SeatClass tbl_SeatClass { get; set; }
        public virtual tbl_TrainRoute tbl_TrainRoute { get; set; }
        public virtual tbl_TrainRoute tbl_TrainRoute1 { get; set; }
        public virtual tbl_Transaction tbl_Transaction { get; set; }
        public virtual tbl_UserDetail tbl_UserDetail { get; set; }
    }
}
