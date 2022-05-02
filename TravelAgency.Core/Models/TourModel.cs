namespace TravelAgency.Core.Models
{
    public class TourModel
    {
        public int TourId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool IncludedInsurance { get; set; }
    }
}
