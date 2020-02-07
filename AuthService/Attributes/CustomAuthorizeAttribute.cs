using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.Attributes
{
   
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
        public ClaimRequirementAttribute(int Position) : base(typeof(ClaimRequirementFilter))
        {
        }
        public ClaimRequirementAttribute(string controllerName, string actionName, params string[] roles) : base(typeof(ClaimsRequirementFilter))
        {
            Arguments = new object[] { new Claim(controllerName, actionName) };
        }
    }

}
