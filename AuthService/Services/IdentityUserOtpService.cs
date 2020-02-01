using AuthService.Enum;
using RepositoryCore.Exceptions;
using System;
using System.Security.Claims;

namespace AuthService.Services
{
    //Check Otp
    public partial class IdentityUserService<TUser, TRole, TUserRole>
    {
        #region Otp

        public void SetOtp(ClaimsPrincipal claims, string otp)
        {
            var user = GetByUserName(claims.Identity.Name).Result;
            SetOtp(user, otp);
        }

        public bool SetOtp(TUser user, string otp)
        {
            user.LastOtpDate = DateTime.Now;
            user.ErrorOtpCount = 0;
            user.LastOtp = otp;
            Update(user).Wait();
            return true;
        }
        public void SetOtp(int id, string otp)
        {
            SetOtp(GetMe(id).Result, otp);
        }
        public void SetOtp(string username, string otp)
        {
            SetOtp(GetByUserName(username).Result, otp);

        }
        public bool CheckUserOtp(TUser user, string otp)
        {
            if (user.LastOtpDate.AddMinutes(AuthOptions.OtpTime) < DateTime.Now)
            {
                throw new CoreException("");
            }
            if (user.LastOtp == otp)
            {
                user.IsSendOtp = false;
                user.LastOtp = "";
                user.UserStatus = UserStatus.Active;
                Update(user).Wait();
                return true;
            }
            else
            {
                user.ErrorOtpCount++;
                throw new CoreException("Error Otp", 4);
            }

        }
        #endregion
    }
}
