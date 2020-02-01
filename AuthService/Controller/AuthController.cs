using AuthService.Interfaces.Service;
using AuthService.Models;
using AuthService.ModelView;
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
    public abstract class AuthController<TUser, TUserRole> : ControllerBase
         where TUser : IdentityUser
         where TUserRole : IdentityUserRole
    {
        IAuthRepository<TUser, TUserRole> _user;
        public AuthController(IAuthRepository<TUser, TUserRole> auth)
        {
            _user = auth;
        }
        [HttpGet]
        public virtual async Task<NetResult<LoginResult>> RefreshToken(string refreshToken)
        {
            try
            {
               var loginResult= _user.LoginByRefresh(refreshToken);
               return loginResult;
                
            }catch(Exception ext)
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

                if (result.Item2!= null|| result.Item2.ShouldSendOtp)
                {
                   var otp= RepositoryState.RandomInt();
                    _user.SetOtp(result.Item2, otp);
                    SendSms(result.Item2.PhoneNumber, otp);
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
        protected abstract void SendSms(string phoneNumber , string otpCode);
        [HttpGet]
        public virtual async Task<NetResult<ResponseData>> RestorePassword(string userName)
        {
            try
            {
                var otp = RepositoryState.RandomInt();
                _user.SetOtp(userName, otp);
                return StatusCore.Success;
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
                bool isRestore = await _user.RestorePasswor(model);

                return StatusCore.Success;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [HttpGet]
        public virtual async Task<NetResult<SuccessResult>> IsRegister(string userName)
        {
            try
            {
                SuccessResult result = new SuccessResult();
                var user = _user.Find(m => m.UserName == userName || m.Email == userName);
                if (user == null)
                    result.Success = false;
                else
                    result.Success = true;
                return result;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [HttpPost]
        public virtual async Task<NetResult<SuccessResult>> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                SuccessResult result = new SuccessResult();
               var user= _user.Get(UserId);
               if(RepositoryState.GetHashString( user.Password)== RepositoryState.GetHashString(model.Password))
              result.Success= await _user.ChangePassword(user, model);
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
              return await  _user.ActivateUser(model);

            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        
        private int UserId { get {
               var userId= User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    Response.StatusCode = 401;
                    throw new CoreException("Anuthorize",401);

                }
                return int.Parse(userId);            
            } }
    }
}
