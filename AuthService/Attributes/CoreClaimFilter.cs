using CoreResults;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositoryCore.Models;
using System.Linq;
using System.Security.Claims;

namespace AuthService.Attributes
{
    internal class CoreClaimFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly Claim[] _claims;
        public CoreClaimFilter(Claim claim)
        {
            _claim = claim;
        }
        [Auth]
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.IsAuthorize())
            {
                return;
            }        
        }
    }





}
