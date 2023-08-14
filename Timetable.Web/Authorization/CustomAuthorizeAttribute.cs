using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Timetable.Domain.Enums;

namespace Timetable.RazorWeb.Authorization
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly RoleEnum[] _allowedRoles;

        public CustomAuthorizeAttribute(RoleEnum[] requiredRole)
        {
            _allowedRoles = requiredRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new ChallengeResult();
                return;
            }

            // You can implement your custom role checking logic here
            if (!IsUserInRole(user, _allowedRoles))
            {
                context.Result = new ForbidResult();
            }
        }

        private bool IsUserInRole(ClaimsPrincipal user, IEnumerable<RoleEnum> roles)
        {
            foreach (var role in roles)
            {
                if (user.IsInRole(role.ToString()))
                {
                    return true;
                } 
            }
            return false;
        }
    }
}
