using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthService.Attributes
{
    internal class CoreClaimFilter : IAuthorizationFilter
    {
        List<string> roles;
        string claimsName;
        public CoreClaimFilter(Claim claim)
        {
            roles = new List<string>();
            claimsName = claim.Type;
            var list = claim.Value.Split(",");
            foreach (var i in list)
            {
                roles.Add(i.Trim().ToLower());
            }
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.IsAuthorize())
            {
                return;
            }
            if (roles.Count() == 0)
            {
                return;
            }
            var userRoles = context.HttpContext.User.FindAll(claimsName);
            foreach (var i in roles)
            {
                if (userRoles.Any(n => n.Value.Trim().ToLower() == i.ToLower()))
                {
                    return;
                }
            }
            AuthOptions.SetUnthorize(context);
        }
    }
}
