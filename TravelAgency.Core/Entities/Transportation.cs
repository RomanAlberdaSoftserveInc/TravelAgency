using System;

namespace TravelAgency.Core.Entities
{
    public class Transportation
    {
        public int Id { get; set; }
        public string DepatureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime DepatureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double PricePerPerson { get; set; }
        public Transport Transport { get; set; }
    }
}
