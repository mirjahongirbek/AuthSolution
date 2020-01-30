using System.Collections.Generic;

namespace AuthService.ModelView
{
    public class LoginResult
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public int SentOtp { get; set; }
        public int AttemptCount { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public List<string> Roles { get; set; }

    }

}
