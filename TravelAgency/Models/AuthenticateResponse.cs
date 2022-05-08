using System.Collections.Generic;
using System.Linq;
using TravelAgency.Core.Entities;

namespace TravelAgency.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Role> Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Manager user, string token)
        {
            Id = user.Id;
            FirstName = user.Name;
            LastName = user.Surname;
            Email = user.Email;
            Role = user.Roles.ToList();
            Token = token;
        }
    }
}
