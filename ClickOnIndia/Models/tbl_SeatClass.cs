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
    
    public partial class tbl_SeatClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SeatClass()
        {
            this.tbl_TrainBooking = new HashSet<tbl_TrainBooking>();
        }
    
        public int Sid { get; set; }
        public string SName { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> Compid { get; set; }
        public Nullable<decimal> CostsAdult { get; set; }
        public Nullable<decimal> CostsChild { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual tbl_Compartment tbl_Compartment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_TrainBooking> tbl_TrainBooking { get; set; }
    }
}
