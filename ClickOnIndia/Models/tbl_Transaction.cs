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
    
    public partial class tbl_Transaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Transaction()
        {
            this.tbl_HotelBooking = new HashSet<tbl_HotelBooking>();
            this.tbl_TrainBooking = new HashSet<tbl_TrainBooking>();
        }
    
        public int Transid { get; set; }
        public string Ran_Transid { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Status { get; set; }
        public string StatusMsg { get; set; }
        public Nullable<int> Type { get; set; }
        public System.DateTime Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HotelBooking> tbl_HotelBooking { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_TrainBooking> tbl_TrainBooking { get; set; }
    }
}
