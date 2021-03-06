﻿using System.Collections.Generic;

namespace AuthModel.ModelView
{
    public class LoginResult
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public bool IsSentOtp { get; set; }
        public int AttemptCount { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public List<string> Roles { get; set; }

    }

}
