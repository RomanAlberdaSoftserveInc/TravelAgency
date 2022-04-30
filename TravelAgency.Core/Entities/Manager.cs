namespace TravelAgency.Core.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Agency Agency { get; set; }
    }
}
