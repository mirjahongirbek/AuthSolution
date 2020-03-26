using AuthService.Enum;
using AuthService.Models;

namespace AuthService.ModelView
{
    public class LoginViewModal
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DeviceName { get; set; }
        public string PartnerName { get; set; }
        public LoginType LoginType { get; set; } = LoginType.InLogin;
        public string DeviceId { get; set; }
        public bool SaveMe{ get; set; }
        
    }
    public class RegisterUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ComparePassword { get; set; }
        public bool CheckValidate()
        {
            return true;
        }        
        
    }
    public class RegisterResult
    {
        public bool IsRegister { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; } 
        public int ErrorId { get; set; }
        public static RegisterResult Create(IdentityUser<string> user)
        {
            RegisterResult result = new RegisterResult()
            {
                IsRegister = true,
                ErrorId = 0,
               UserName= user.UserName
            };
            return result;
        }
    }
    public class RestorePasswordModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ComparePassword { get; set; }
        public string Otp { get; set; }
        public string Token { get; set; }
        public bool IsCompare()
        {
            if(Password== ComparePassword)
            {
                return true;
            }
            return false;
        }

    }
    public class ChangePasswordModel
    {
        public string Password { get; set; }
        public string ComparePassword { get; set; }
        public string OldPassword { get; set; }
        public bool IsCompare()
        {
            if(Password== ComparePassword)
            {
                return true;
            }
            return false;
        }

    }
    public class ActivateUserModel
    {
        public string Otp { get; set; }
        public string UserName { get; set; }
        public int Count { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
      
        public string GetUserName()
        {

            return "";
        }
    }

}
