using AuthModel;
using AuthModel.Enum;
using AuthService;
using RepositoryCore.Exceptions;
using System;
using System.Security.Claims;

namespace EntityRepository.Services
{
    //Check Otp
    public partial class EntityUserService<TUser, TRole, TUserRole>
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
        public TUser SetOtp(int id, string otp)
        {
           var user= GetMe(id).Result;
            SetOtp(user, otp);
            return user;
        }
        public TUser SetOtp(string username, string otp)
        {
           var user= GetByUserName(username).Result;
            SetOtp(user, otp);
            return user;

        }
        public bool CheckUserOtp(TUser user, string otp)
        {
            if (user.LastOtpDate.AddMinutes(AuthModalOption.OtpTime) < DateTime.Now)
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
