using System;

namespace TravelAgency.Core.Entities
{
    public class Check
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int PersonCount { get; set; }
        public Tour Tour { get; set; }
        public User User { get; set; }
        public Manager Manager { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
