using AuthModel;
using AuthModel.Interfaces;
using AuthModel.Models.Entitys;
using AuthModel.ModelView;

using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.CoreState;
using RepositoryCore.Enums;
using RepositoryCore.Exceptions;
using RepositoryCore.Models;
using System;
using System.Threading.Tasks;

namespace AuthService.Controller
{

    public abstract class AuthController<TUser, TUserRole, TKey> : ControllerBase
         where TUser : IdentityUser<TKey>
         where TUserRole : IdentityUserRole<TKey>
    {
        IAuthRepository<TUser, TUserRole, TKey> _user;
        public AuthController(IAuthRepository<TUser, TUserRole, TKey> auth)
        {
            _user = auth;
        }
        [HttpGet]
        public virtual async Task<NetResult<LoginResult>> RefreshToken(string refreshToken)
        {
            try
            {
                var loginResult = _user.LoginByRefresh(refreshToken);
                return loginResult;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }

        [HttpPost]
        public virtual async Task<NetResult<LoginResult>> Login([FromBody] LoginViewModal model)
        {
            try
            {
                var result = await _user.Login(model);
                if (result.Item1 != null)
                {
                    return result.Item1;
                }
                if (result.Item2 != null || result.Item2.ShouldSendOtp)
                {
                    var otp = RepositoryState.RandomInt();
                    _user.SetOtp(result.Item2, otp);
                    SenNotify(result.Item2, otp);
                    return new LoginResult()
                    {
                        UserName = result.Item2.UserName,
                        UserId = result.Item2.Id.ToString(),
                        IsSentOtp = true,
                    };
                }
                return null;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }

        [HttpPost]
        public virtual async Task<NetResult<RegisterResult>> Register([FromBody] RegisterUser model)
        {
            try
            {
                RegisterResult result = await _user.RegisterAsync(model);
                return result;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        protected abstract void SenNotify(TUser phoneNumber, string otpCode);
        [HttpGet]
        public virtual async Task<NetResult<ResponseData>> RestorePassword(string userName)
        {
            try
            {
                var otp = RepositoryState.RandomInt();
                var user = await _user.GetByUserName(userName);
                if (user == null) throw new CoreException("User not found");
                user.Token = RepositoryState.GenerateRandomString(24);
                _user.SetOtp(user, otp);
                SenNotify(user, otp);
                return new ResponseData() { Result = new { Token = user.Token, UserName = user.UserName, IsSendSms = true } };
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [HttpPost]
        public virtual async Task<NetResult<ResponseData>> RestorePassword([FromBody]RestorePasswordModel model)
        {
            try
            {
                SuccessResult result = new SuccessResult();
                if (string.IsNullOrEmpty(model.Token))
                {
                    throw new CoreException("Token not found", 5);

                }
                var loginResult = await _user.RestorePassword(model);
                if (loginResult.IsSentOtp)
                {
                    SendSms(loginResult.UserName, model.Otp);
                }
                return new ResponseData()
                {
                    Result = loginResult
                };

            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        protected abstract void SendSms(string userName, string otp);
        [HttpGet]
        public virtual async Task<NetResult<SuccessResult>> IsRegister(string userName)
        {
            SuccessResult result = new SuccessResult();
            try
            {
                if (AuthModalOption.SetNameAsPhone && !userName.Contains("@"))
                {

                    userName = RepositoryState.ParsePhone(userName);
                }
                var user = _user.GetFirst(m => m.UserName == userName || m.Email == userName);
                if (user == null)
                    result.Success = false;
                else
                    result.Success = true;
                return result;
            }
            catch (Exception ext)
            {
                result.Success = false;
                //return ext;
            }
            return result;
        }
        [HttpPost]
        public virtual async Task<NetResult<SuccessResult>> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                SuccessResult result = new SuccessResult();
                var user = _user.Get(this.UserId<TKey>());
                if (RepositoryState.GetHashString(user.Password) == RepositoryState.GetHashString(model.OldPassword))
                    result.Success = await _user.ChangePassword(user, model);
                return result;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [HttpPost]
        public virtual async Task<NetResult<LoginResult>> ActivateUser([FromBody] ActivateUserModel model)
        {
            try
            {
                return await _user.ActivateUser(model);

            }
            catch (Exception ext)
            {
                return ext;
            }
        }


    }

}
