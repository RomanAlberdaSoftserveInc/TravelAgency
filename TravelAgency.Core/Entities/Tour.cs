using System;
using System.Collections.Generic;

namespace TravelAgency.Core.Entities
{
    public class Tour
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool IncludedInsurance { get; set; }
        public Hotel Hotel { get; set; }
        public TourType TourType { get; set; }
        public Manager CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public ICollection<TourPhoto> TourPhotos { get; set; } = new List<TourPhoto>();
        public ICollection<Transportation> Transportations { get; set; } = new List<Transportation>();
    }
}
