using AuthService;
using AuthService.Enum;
using AuthService.ModelView;
using Microsoft.EntityFrameworkCore;
using RepositoryCore.CoreState;
using RepositoryCore.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EntityRepository.Services
{
    //Register Partial
    public partial class EntityUserService<TUser, TRole, TUserRole>
    {
        public async Task<RegisterResult> RegisterAsync(RegisterUser model)
        {
            RegisterResult result = new RegisterResult();
            
            if (AuthOptions.SetNameAsPhone)
            {
                model.UserName = RepositoryState.ParsePhone(model.UserName);
            }
            var user = _dbSet.FirstOrDefault(m => m.UserName == model.UserName || m.Email == model.UserName);
            if (user != null)
            {
                throw new CoreException("User Name or Password not Found", 7);
                return result;
            }
            user = AddRegister(model);
            if (AuthOptions.SetNameAsPhone)
            {
                model.UserName = RepositoryState.ParsePhone(model.UserName);
                if (string.IsNullOrEmpty(model.UserName))
                {
                    throw new CoreException("UserName is Not Valid", 6);
                }
                user.PhoneNumber = model.UserName;
            }
            //_dbSet.Add(user);
            Add(user);
            result.IsRegister = true;
            result.UserName = user.UserName;
            return result;

        }
        public virtual async Task<bool> RegisterAsync(TUser model)
        {
            var user = await
                _dbSet.FirstOrDefaultAsync(m => m.UserName == model.UserName
                && m.Password == RepositoryState.GetHashString(model.Password));
            if (user != null) { return false; }
            model.Password = RepositoryState.GetHashString(model.Password);
            _dbSet.Add(model);
            _context.SaveChanges();
            return true;
        }
        private TUser AddRegister(RegisterUser model)
        {
           
            var user = (TUser)Activator.CreateInstance(typeof(TUser));
            user.UserName = model.UserName;
            user.UserStatus = UserStatus.NewRegistered;
            user.Password = model.Password;
            user.Email = model.Email;
            return user;
        }
    }
}
