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
    
    public partial class tbl_RoomTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_RoomTypes()
        {
            this.tbl_HotelRoom = new HashSet<tbl_HotelRoom>();
        }
    
        public int RoomTypeId { get; set; }
        public string Type { get; set; }
        public Nullable<int> Uid { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> Bed_Count { get; set; }
        public string Room_Class { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HotelRoom> tbl_HotelRoom { get; set; }
        public virtual tbl_UserDetail tbl_UserDetail { get; set; }
    }
}
