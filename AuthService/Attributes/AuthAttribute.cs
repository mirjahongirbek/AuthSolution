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
        public AuthAttribute(string claimsName, string roles) : base(typeof(CoreClaimFilter))
        {
            Arguments = new object[] { new Claim(claimsName,roles)};
        }
     
        public AuthAttribute(int Position) : base(typeof(PositonClaimFilter))
        {
            Arguments = new object[] { new Claim(Position.ToString(), "item") };
        }
        
        public AuthAttribute(bool byAction = false, [CallerMemberName] string action = "", [CallerFilePath] string path = "") : base(typeof(ActionFilter))
        {
            if (string.IsNullOrEmpty(action))
            {
                if (AuthOptions.CheckDefaulAction)
                {
                    throw new Exception("In path " + path + "not set in Controller plaese set Action");
                }
            }
            path = path.Split('\\').Last();
            path = path.Split("Controller")[0];
            Arguments = new object[] { new Claim(byAction.ToString(), action, path) };
        }

    }

}
