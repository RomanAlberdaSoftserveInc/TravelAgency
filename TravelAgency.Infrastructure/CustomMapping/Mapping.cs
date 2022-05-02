using TravelAgency.Core.Entities;
using TravelAgency.Core.Models;

namespace TravelAgency.Infrastructure.CustomMapping
{
   public class Mapping
    {
        public Manager MapManager(ManagerModel managerModel)
        {
            return new Manager()
            {
                Id = managerModel.ManagerId,
                Name = managerModel.ManagerName,
                Surname = managerModel.ManagerSurname,
                Email = managerModel.Email,
                PhoneNumber = managerModel.ManagerPhoneNumber,
            };
        }
        public User MapUser(UserModel userModel)
        {
            return new User()
            {
                Id = userModel.UserId,
                Name = userModel.UserName,
                Surname = userModel.UserSurname,
                Email = userModel.Email,
                PhoneNumber = userModel.UserPhoneNumber,
                Passport = userModel.Passport,
            };
        }

        public Tour MapTour(TourModel tourModel)
        {
            return new Tour()
            {
                Id = tourModel.TourId,
                IncludedInsurance = tourModel.IncludedInsurance,
                City = tourModel.City,
                Country = tourModel.Country,
            };
        }
    }
}
