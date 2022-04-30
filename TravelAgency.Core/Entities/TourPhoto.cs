namespace TravelAgency.Core.Entities
{
    public class TourPhoto
    {
        public int Id { get; set; }
        public bool IsMain { get; set; }
        public string Url { get; set; }
        public Tour Tour { get; set; }
    }
}
