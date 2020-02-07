using System;

namespace AuthService.Models
{
    public class DeleteData
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string TableName { get; set; }
        public string SchemeName { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
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
