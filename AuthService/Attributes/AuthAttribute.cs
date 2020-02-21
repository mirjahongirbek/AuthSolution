using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace AuthService.Attributes
{

    public class AuthAttribute : TypeFilterAttribute
    {
        public AuthAttribute(string claimType, string claimValue) : base(typeof(CoreClaimFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
        public AuthAttribute(int Position) : base(typeof(CoreClaimFilter))
        {
        }
        public AuthAttribute(string controllerName, string actionName, params string[] roles) : base(typeof(CoreClaimsFilter))
        {
            Arguments = new object[] { new Claim(controllerName, actionName) };
        }
        public AuthAttribute(bool byAction = false, [CallerMemberName] string action = "", [CallerFilePath] string path = "") : base(typeof(ActionFilter))
        {
            if(string.IsNullOrEmpty(action))
            path = path.Split('\\').Last();
            path = path.Split("Controller")[0];
            Arguments = new object[] { new Claim(byAction.ToString(), action, path) };
        }

    }

}
