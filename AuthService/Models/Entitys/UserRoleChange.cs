using System;

namespace AuthService.Models
{
    public class UserRoleChange
    {
        public int ChangeUserId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Description { get; set; }
    }
    /* public class LoginResult
     {
         public LoginResult()
         {
             Roles = new List<string>();

         }
         public string AccessToken { get; set; }
         public string UserName { get; set; }
         public string RefreshToken { get; set; }
         public List<string> Roles { get; set; }
         public int MaxPosition { get; set; }
         public List<string> Actions { get; set; }
         public int MyId { get; set; }
     }*/


}
