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
    
    public partial class tbl_BusBoking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_BusBoking()
        {
            this.tbl_Passenger = new HashSet<tbl_Passenger>();
        }
    
        public int Bbid { get; set; }
        public Nullable<int> Uid { get; set; }
        public Nullable<int> From_BrId { get; set; }
        public Nullable<int> To_BrId { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> Transid { get; set; }
        public System.DateTime JourneyDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
    
        public virtual tbl_BusRoute tbl_BusRoute { get; set; }
        public virtual tbl_BusRoute tbl_BusRoute1 { get; set; }
        public virtual tbl_BusRoute tbl_BusRoute2 { get; set; }
        public virtual tbl_UserDetail tbl_UserDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Passenger> tbl_Passenger { get; set; }
    }
}
