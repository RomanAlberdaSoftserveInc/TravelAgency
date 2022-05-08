using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;
using TravelAgency.Services;

namespace TravelAgency.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IList<TravelAgency.Core.Enums.Role> _roles;
 
        public AuthorizeAttribute(params TravelAgency.Core.Enums.Role[] roles)
        {
            _roles = roles ?? new TravelAgency.Core.Enums.Role[] { };
        }

        private bool IsInAnyRole(Manager user, IList<TravelAgency.Core.Enums.Role> roles)
        {
            var userRoles = user.Roles;
            List<string> rolesList = roles.ToList().ConvertAll(f => f.ToString());
            return userRoles.Any(u => rolesList.ToList().Contains(u.Name));
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var managerRepository = context.HttpContext.RequestServices.GetService(typeof(IManagerRepository)) as IManagerRepository;
            var roleRepository = context.HttpContext.RequestServices.GetService(typeof(IRoleRepository)) as IRoleRepository;
            var roles = await roleRepository.GetAllAsync();
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var user = (Manager)context.HttpContext.Items["User"];
            var userWithRoles = await managerRepository.GetUserWithRolesAsync(user.Id);

            if (user == null || (roles.Any() && !IsInAnyRole(userWithRoles, _roles)))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
