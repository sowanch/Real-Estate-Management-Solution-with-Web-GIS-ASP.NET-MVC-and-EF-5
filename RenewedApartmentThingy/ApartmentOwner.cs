//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RenewedApartmentThingy
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApartmentOwner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApartmentOwner()
        {
            this.Apartments = new HashSet<Apartment>();
        }
    
        public long ApartmentOwnerID { get; set; }
        public string ApartmentOwnerName { get; set; }
        public string ApartmentOwnerNumber { get; set; }
        public System.DateTime OwnerStartDate { get; set; }
        public Nullable<System.DateTime> OwnerEndDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}