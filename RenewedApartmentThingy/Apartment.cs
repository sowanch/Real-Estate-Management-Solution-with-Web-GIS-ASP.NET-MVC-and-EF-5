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
    
    public partial class Apartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apartment()
        {
            this.ApartmentStatusTrails = new HashSet<ApartmentStatusTrail>();
            this.Photos = new HashSet<Photo>();
        }
    
        public long ApartmentID { get; set; }
        public long ApartmentOwnerID { get; set; }
        public System.DateTime ApartmentBuildDate { get; set; }
        public bool ApartmentStatus { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public string ApartmentName { get; set; }
        public int ParkingType { get; set; }
        public string LocationName { get; set; }
        public decimal CurrentPrice { get; set; }
        public int ApartmentType { get; set; }
        public int ApartmentSize { get; set; }
        public bool WaterAvailability { get; set; }
        public bool ElectricityAvailability { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<int> Floor { get; set; }
        public int BuildingFloors { get; set; }
        public string BuildingComplexName { get; set; }
    
        public virtual ApartmentOwner ApartmentOwner { get; set; }
        public virtual ApartmentType ApartmentType1 { get; set; }
        public virtual ParkingType ParkingType1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmentStatusTrail> ApartmentStatusTrails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
