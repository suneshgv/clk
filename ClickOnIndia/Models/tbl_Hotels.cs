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
    
    public partial class tbl_Hotels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Hotels()
        {
            this.tbl_HotelBooking = new HashSet<tbl_HotelBooking>();
            this.tbl_HotelRoom = new HashSet<tbl_HotelRoom>();
        }
    
        public int Hid { get; set; }
        public string HotelName { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> RoomCount { get; set; }
        public Nullable<int> Uid { get; set; }
        public System.DateTime Date { get; set; }
        public string Location { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HotelBooking> tbl_HotelBooking { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HotelRoom> tbl_HotelRoom { get; set; }
        public virtual tbl_UserDetail tbl_UserDetail { get; set; }
    }
}
