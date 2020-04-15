using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthModel;
using AuthModel.Interfaces;
using AuthModel.ModelView;
using MongoAuthService.Models;
using RepositoryCore.CoreState;
using RepositoryCore.Exceptions;
using RepositoryCore.Interfaces;

namespace MongoAuthService.Services
{
    public partial class MongoUserService<TUser, TRole, TUserRole>
    {
        public LoginResult Login(TUser user)
        {
            if (user == null) { return null; }
            var roles = user.UserRoles.Select(m => m.MongoRole).ToList();
            var jwt = AuthModalOption.GenerateClaims<TUser, MongoRole, string>(user, roles);
            var accessToken = AuthModalOption.SetToken<TUser, string>(jwt, user);
            var refreshToken = RepositoryState.GenerateRandomString(24);
            user.RefreshToken = refreshToken;
            _repo.Update(user);
            LoginResult loginResult = new LoginResult()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserName = user.UserName,
                UserId = user.Id,
                IsSentOtp = false

            };
            return loginResult;

        }
        public async Task<(LoginResult, TUser)> Login(LoginViewModal model)
        {
            var user = CheckUserByUserName(model.UserName, model.Password);
            if (user == null)
            {
                throw new CoreException("User not found", 0);

            }
            if (AuthModalOption.CheckDeviceId && !user.CheckDevice(model.DeviceId))
            {
                return (null, user);
            }
            return (Login(user), user);
        }

        public LoginResult LoginByRefresh(string refreshToken)
        {
            var user = _repo.GetFirst(m => m.RefreshToken == refreshToken);
            if (user == null)
            {

            }
            return Login(user);
        }

        public async Task<bool> RegisterAsync(TUser model)
        {
            _repo.Add(model);
            return true;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterUser model)
        {
            if (model == null) { }
            if (!model.CheckValidate())
            {
            }
            var user = CheckUser(model.UserName);
            if (user != null) { throw new CoreException("User not found", 0); }
            user = (TUser)Activator.CreateInstance(typeof(TUser));
            user.Create<string>(model);
            await RegisterAsync(user);
            return RegisterResult.Create(user);

        }
        public async Task<LoginResult> ActivateUser(ActivateUserModel model)
        {
            TUser user = null;
            if (AuthModalOption.SetNameAsPhone)
            {
                user = _repo.GetFirst(m => m.UserName == model.UserName.ParsePhone());
            }
            else user = _repo.GetFirst(m => m.UserName == model.UserName);
            if (user == null) { throw new CoreException("User not found", 0); }
            if (CheckUserOtp(user, model.Otp))
            {
                user.AddDeviceId(model.DeviceId, model.DeviceName);
                await Update(user);
                return Login(user);
            }
            else
            {
                throw new CoreException("Confirm code is incorect", 3);
            }

        }
        public async Task<LoginResult> LoginResult(LoginViewModal model)
        {

            var user = CheckUserByUserName(model.UserName, model.Password);
            if (user == null)
            {
            }
            return Login(user);

        }
    }
    public partial class MongoUserService<TUser, TRole, TUserRole> : IAuthRepository<TUser, TUserRole, string>
        where TUser : MongoUser
        where TUserRole : MongoUserRole
        where TRole : MongoRole
    {
        IRepositoryCore<TUser, string> _repo;
        public MongoUserService(IRepositoryCore<TUser, string> repo)
        {
            _repo = repo;
        }


        public bool AddUser(TUser user)
        {
            _repo.Add(user);
            return true;
        }

        public void AddUserRole(string userId, TUserRole userRole)
        {
            var user = Get(userId);
            user.UserRoles.Add(userRole);
            Update(user).Wait();
        }

        public async Task<bool> ChangePassword(TUser user, ChangePasswordModel model)
        {
            if (user.Password != model.OldPassword)
            {

            }
            user.Password = RepositoryState.GetHashString(model.Password);
            await Update(user);
            return true;
        }

        public TUser CheckUser(string userName)
        {
            if (AuthModalOption.SetNameAsPhone)
            {
                userName = RepositoryState.ParsePhone(userName);
            }
            return _repo.GetFirst(m => m.UserName == userName);//?? throw new CoreException("User not found",0);
        }

        public TUser CheckUserByPhone(string userName, string phoneNumber)
        {
            var user = GetByUserName(userName).Result;
            if (user.PhoneNumber == RepositoryState.ParsePhone(phoneNumber))
            {
                return user;
            }
            throw new CoreException("Phone Number is incorrect", 2);
        }

        public TUser CheckUserByUserName(string userName, string password)
        {
            var user = CheckUser(userName);
            if (user == null) { throw new CoreException("User not found", 0); }
            if (user.Password == RepositoryState.GetHashString(password))
            {
                return user;
            }
            throw new CoreException("UserName or Password is incorect", 1);
        }

        public bool CheckUserOtp(TUser user, string otp)
        {
            if (user.LastOtp == otp)
            {
                user.PhoneNumberConfirmed = true;
                return true;
            }
            return false;
        }

        public long Count()
        {
            return _repo.Count();
        }

        public long Count(Expression<Func<TUser, bool>> expression)
        {
            return _repo.Count(expression);
        }

        public async Task<bool> Delete(string id)
        {
            _repo.Delete(id);
            return true;
        }


        public async Task<bool> Delete(TUser user)
        {
            _repo.Delete(user);
            return true;
        }

        public IEnumerable<TUser> Find(Expression<Func<TUser, bool>> expression)
        {
            return _repo.FindAll();
        }
        public IEnumerable<TUser> FindAll()
        {
            return _repo.FindAll();
        }

        public TUser Get(string id)
        {
            return _repo.Get(id) ?? throw new CoreException("User not found", 0);
        }

        public async Task<TUser> GetByEmail(string model)
        {
            if (!RepositoryState.IsValidEmail(model)) { }
            return _repo.GetFirst(m => m.Email == model);
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public async Task<TUser> GetByUserName(string userName)
        {
            if (AuthModalOption.SetNameAsPhone)
            {
                userName = RepositoryState.ParsePhone(userName);
            }
            return _repo.GetFirst(m => m.UserName == userName) ?? throw new CoreException("User not found", 0);

        }

        public TUser GetFirst(Expression<Func<TUser, bool>> expression)
        {
            return _repo.GetFirst(expression);
        }
        public async Task<LoginResult> RestorePassword(RestorePasswordModel model)
        {
            var user = await GetByUserName(model.UserName);
            if (user == null)
            {
                throw new CoreException("User not found", 0);
            }
            if (string.IsNullOrEmpty(model.Token))
            {
                model.Token = RepositoryState.GenerateRandomString(24);
                SetOtp(user, RepositoryState.RandomInt(6));
                model.Otp = user.LastOtp;
                return new LoginResult()
                {
                    UserName = user.UserName,
                    IsSentOtp = true,
                    
                };
            }
            if (user.Token == model.Token)
            {
                if (user.CheckOtp(model.Otp))
                {
                    user.Password = RepositoryState.GetHashString(model.Password);
                    await Update(user);
                    return Login(user);
                }
                throw new CoreException("Confirm Code is incorrct", 3);
            }
            throw new CoreException("");
        }
        #region SetOtp
        public void SetOtp(ClaimsPrincipal user, string Otp)
        {
            var userId = user.FindFirst(m => m.Type.ToLower() == "id")?.Value;
            if (string.IsNullOrEmpty(userId)) return;
            var getUser = Get(userId);
            SetOtp(getUser, Otp);
        }

        public TUser SetOtp(string UserId, string otp)
        {
            var user = _repo.Get(UserId);
            SetOtp(user, otp);
            return user;
        }
        public bool SetOtp(TUser user, string otp)
        {
            user.LastOtp = otp;
            user.LastOtpDate = DateTime.Now;
            Update(user).Wait();
            return true;
        }
        #endregion

        public void SetRefresh(TUser user)
        {
            user.RefreshToken = RepositoryState.GenerateRandomString(24);
            Update(user).Wait();
        }
        public async Task Update(TUser user)
        {
            _repo.Update(user);
        }

        public async Task<bool> ChangePassword(string userId, ChangePasswordModel model)
        {
            var user = Get(userId);
            return await ChangePassword(user, model);

        }
    }


}
