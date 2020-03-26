using AuthService;
using AuthService.Interfaces.Service;
using AuthService.ModelView;
using EntityRepository.Context;
using EntityRepository.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryCore.CoreState;
using RepositoryCore.Exceptions;
using RepositoryCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityRepository.Services
{

    public partial class EntityUserService<TUser, TRole, TUserRole>
        : IAuthRepository<TUser, TUserRole, int>
       where TUser : EntityUser
       where TUserRole :EntityUserRole
       where TRole : EntityRole
    {
        #region Default Constructor
        DbSet<TUser> _dbSet;
        DbSet<TUserRole> _userRole;
        DbContext _context;
        IRepositoryCore<TUser, int> _repo;
        IRoleRepository<TRole, int> _roleService;
        public EntityUserService(IDbContext context,
            IRepositoryCore<TUser, int> repo,
            IRoleRepository<TRole, int> roleService
            
            )
        {
            _repo = repo;
            _dbSet = context.DataContext.Set<TUser>();
            _userRole = context.DataContext.Set<TUserRole>();
            _context = context.DataContext;
            _roleService = roleService;
        }
        #endregion
        #region CRUD Metdhos
        public virtual async Task<bool> Delete(int id)
        {
            var user = Get(id);
            if (user == null)
            {
                return false;
            }
            _dbSet.Remove(user);
            return true;
        }
        public virtual TUser Get(int id)
        {
            return _dbSet.FirstOrDefault(m => m.Id == id);
        }
        public virtual async Task<TUser> GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(m => m.Email == email || m.UserName == email);
        }
        public virtual async Task<TUser> GetByUserName(string userName)
        {
            TUser user = null;
            if (AuthOptions.SetNameAsPhone)
            {
                user = _dbSet.FirstOrDefault(m => m.UserName == RepositoryState.ParsePhone(userName));
            }
            else
            {
                user = _dbSet.FirstOrDefault(m => m.UserName == userName);
            }
            return user;
            //return _dbSet.FirstOrDefault(m => m.UserName == userName);
        }
        public async Task<bool> Delete(TUser user)
        {
            _dbSet.Remove(user);
            Save();
            return true;
        }
        protected void Save()
        {
            _context.SaveChanges();
        }
        public async Task<TUser> GetMe(string userName)
        {
            return _dbSet.FirstOrDefault(m => m.UserName == userName);
        }
        public async Task Update(TUser user)
        {
            _dbSet.Update(user);
            Save();
        }
        public List<TRole> GetRoles(TUser user)
        {
            List<TUserRole> userRoles = _userRole.Where(m => m.UserId == user.Id).ToList();
            var IdRoles = userRoles.Select(m => m.RoleId).ToList();
            var roles = _roleService.GetList(IdRoles);
            return roles;
        }
        public long Count()
        {
            return _dbSet.Count();
        }
        #endregion
       

        public async Task<TUser> GetMe(int id)
        {
            return _dbSet.FirstOrDefault(m => m.Id == id);
        }
        private bool Add(TUser user)
        {
            _dbSet.Add(user);
            Save();
            return true;
        }
        public bool AddUser(TUser user)
        {
            var existUser = GetByUserName(user.UserName).Result;
            if (existUser != null)
            {
                return false;
            }
            Add(user);
            return true;
        }
        public IEnumerable<TUser> FindAll()
        {
            return _dbSet.Where(m => true);
        }
        #region
        public TUser CheckUser(string userName)
        {
            return _dbSet.FirstOrDefault(m => m.UserName == userName);

        }
        public TUser CheckUserByPhone(string userName, string phoneNumber)
        {
            var user = _dbSet.FirstOrDefault(m => m.UserName == userName && m.PhoneNumber == phoneNumber);
            return user;
        }
        public TUser CheckUserByUserName(string userName, string Password)
        {
            return _dbSet.FirstOrDefault(m => m.UserName == userName && m.Password == RepositoryState.GetHashString(Password));
        }
        #endregion
        public TUser GetFirst(Expression<Func<TUser, bool>> expression)
        {
            return _dbSet.Where(expression).OrderByDescending(m => m.Id).FirstOrDefault();
            //return _dbSet.FirstOrDefault(expression);
        }
        public IEnumerable<TUser> Find(Expression<Func<TUser, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

       

        public long Count(Expression<Func<TUser, bool>> expression)
        {
            return _dbSet.Count(expression);
        }
        public void AddUserRole(TUserRole userRole)
        {
            _userRole.Add(userRole);
            _context.SaveChanges();
        }
        
        #region Password
        public async Task<bool> RestorePasswor(RestorePasswordModel model)
        {
            if (!model.IsCompare()) return false;

            var user = await GetByUserName(model.UserName);//GetFirst(m => m.UserName == model.UserName);
            if (user == null)
            {
                throw new CoreException("User Not Valid",5);
            }
            if (!string.IsNullOrEmpty(model.Token))
            {

            }
            if (CheckUserOtp(user, model.Otp))
            {
                user.Password = RepositoryState.GetHashString(model.Password);
            }
            else
            {
                //4 Restore Password
                throw new CoreException("Error Otp",4);
            }
            await Update(user);
            return true;
        }
        public async Task<bool> ChangePassword(TUser user, ChangePasswordModel model)
        {
            if (!model.IsCompare())
            {
                throw new CoreException("Compare password is not valid",3);
            }
            if (user.Password != RepositoryState.GetHashString(model.OldPassword))
            {
                throw new CoreException(" Passwor is not valid",2);
            }
            user.Password = RepositoryState.GetHashString(model.Password);
            await Update(user);
            return true;
        }

        public void AddUserRole(int userId, TUserRole userRole)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResult> LoginResult(LoginViewModal model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(int userId, ChangePasswordModel model)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
   
}
