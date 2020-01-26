
using AuthService.Enum;

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
}
