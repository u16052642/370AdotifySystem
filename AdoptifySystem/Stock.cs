//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdoptifySystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stock()
        {
            this.Donation_Line = new HashSet<Donation_Line>();
        }
    
        public int Stock_ID { get; set; }
        public string Stock_Name { get; set; }
        public Nullable<int> Stock_Quantity { get; set; }
        public string Stock_Description { get; set; }
        public Nullable<int> Stock_Type_ID { get; set; }
        public Nullable<int> Packaging_Type_ID { get; set; }
        public Nullable<int> Unit_Type_ID { get; set; }
        public Nullable<int> Unit_number { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation_Line> Donation_Line { get; set; }
        public virtual Packaging_Type Packaging_Type { get; set; }
        public virtual Stock_Type Stock_Type { get; set; }
        public virtual Unit_Type Unit_Type { get; set; }
    }
}