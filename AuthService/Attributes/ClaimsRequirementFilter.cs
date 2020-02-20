using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthService.Attributes
{
    internal class ClaimsRequirementFilter : IAuthorizationFilter
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
    internal class ActionFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly Claim[] _claims;
        public ActionFilter(Claim[] claim)
        {
            _claims = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

        }
    }
    internal class SimpleAuthFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly Claim[] _claims;
        public SimpleAuthFilter(Claim[] claim)
        {
            _claims = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

        }
    }




}
