using CoreResults;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositoryCore.Models;
using System.Linq;
using System.Security.Claims;

namespace AuthService.Attributes
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly Claim[] _claims;
        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasClaim = false;
            if (context.HttpContext.User == null && context.HttpContext.User.Claims.Count() == 0)
            {
                hasClaim = true;
            }
            else
            {
                hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            }
            
            if (!hasClaim)
            {
                NetResult<ResponseData> result = new NetResult<ResponseData>()
                {
                    HttpStatus = 401,
                    Error = new RepositoryCore.Result.ErrorResult() { Code=401, Message="Unuthorize" }
                };
                context.Result = new CoreJsonResult(result);
            }
        }
    }





}
