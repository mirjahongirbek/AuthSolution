using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService;
using AuthService.Interfaces.Service;
using AuthService.ModelView;
using MongoAuthService.Models;
using RepositoryCore.CoreState;
using RepositoryCore.Interfaces;

namespace MongoAuthService.Services
{
    public partial class MongoUserService<TUser, TRole, TUserRole>
    {
        public LoginResult Login(TUser user)
        {
            if (user == null) { return null; }
            var roles = user.UserRoles.Select(m => m.MongoRole).ToList();
            var jwt = AuthOptions.GenerateClaims<TUser, MongoRole, string>(user, roles);

        }
        public async Task<TUser> GetByUserName(string userName, string password)
        {
            var user = CheckUser(userName);
            if (user.Password == RepositoryState.GetHashString(password))
            {
                return user;
            }
            return null;
        }
        public async Task<(LoginResult, TUser)> Login(LoginViewModal model)
        {
            var user = await GetByUserName(model.UserName, model.Password);
            if (user == null)
            {

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

        public Task<bool> RegisterAsync(TUser model)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterResult> RegisterAsync(RegisterUser model)
        {
            throw new NotImplementedException();
        }
        public Task<LoginResult> ActivateUser(ActivateUserModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<LoginResult> LoginResult(LoginViewModal model)
        {
            var user = await GetByUserName(model.UserName, model.Password);
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

        public void AddUserRole(TUserRole userRole)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(TUser user, ChangePasswordModel model)
        {
            throw new NotImplementedException();
        }

        public TUser CheckUser(string userName)
        {
            if (AuthOptions.SetNameAsPhone)
            {
                userName = RepositoryState.ParsePhone(userName);
            }
            return _repo.GetFirst(m => m.UserName == userName);
        }

        public TUser CheckUserByPhone(string userName, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public TUser CheckUserByUserName(string userName, string Password)
        {
            throw new NotImplementedException();
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
         

        public Task<bool> Delete(TUser user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TUser> Find(Expression<Func<TUser, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TUser> FindAll()
        {
            throw new NotImplementedException();
        }

       
        public TUser Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> GetByEmail(string model)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public TUser GetFirst(Expression<Func<TUser, bool>> expression)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RestorePasswor(RestorePasswordModel model)
        {
            throw new NotImplementedException();
        }

        public void SetOtp(ClaimsPrincipal user, string Otp)
        {
            throw new NotImplementedException();
        }

        public TUser SetOtp(int UserId, string otp)
        {
            throw new NotImplementedException();
        }

        public TUser SetOtp(string username, string otp)
        {
            throw new NotImplementedException();
        }

        public bool SetOtp(TUser user, string otp)
        {
            throw new NotImplementedException();
        }

        public void SetRefresh(TUser user)
        {
            throw new NotImplementedException();
        }

        public string SetToken(List<Claim> claims, TUser user)
        {
            throw new NotImplementedException();
        }

        public Task Update(TUser user)
        {
            throw new NotImplementedException();
        }
    }

}
