using CoreResults;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositoryCore.Models;
using System.Linq;
using System.Security.Claims;

namespace AuthService.Attributes
{
    internal class CoreClaimsFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly Claim[] _claims;
        public CoreClaimsFilter(Claim[] claim)
        {
            _claims = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.IsAuthorize())
            {
                return;
            }

        }
    }
    internal class ActionFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly Claim[] _claims;
        bool isAction = false;
        string action = "", controller = "";

        //readonly string 
        public ActionFilter(Claim claim)
        {
            _claim = claim;
            isAction = bool.Parse(claim.Type);
            if (!isAction)
            {
                isAction = AuthOptions.CheckDefaulAction;
            }
            action = claim.Value.ToLower();
            controller = claim.ValueType.ToLower();

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (context.IsAuthorize())
            {
                return;
            }
            var item = context.HttpContext.User.FindAll("actions");
            if (!isAction)
            {
                return;
            }
            if (item.Any(m => m.Value.ToLower() == action))
            {
                return;

            }
            context.SetUnthorize();
        }

    }

    internal class SimpleAuthFilter : IAuthorizationFilter
    {

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
