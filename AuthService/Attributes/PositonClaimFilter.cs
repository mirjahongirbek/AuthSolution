using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthService.Attributes
{
    internal class PositonClaimFilter : IAuthorizationFilter
    {
        int postion;
        public PositonClaimFilter(Claim claim)
        {
            postion = int.Parse(claim.Type);
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.IsAuthorize())
            {
                return;
            }
            var pst = context.HttpContext.User.FindFirst("position")?.Value?.ToString();
            if (string.IsNullOrEmpty(pst))
            {
                context.SetUnthorize();
                return;
            }
            var p = int.Parse(pst);
            if(p>= postion)
            {
                return;
            }
            context.SetUnthorize();
        }
    }


}
