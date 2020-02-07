using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthService.Attributes
{
    public class ClaimsRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly Claim[] _claims;
        public ClaimsRequirementFilter(Claim[] claim)
        {
            _claims = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

        }

    }





}
