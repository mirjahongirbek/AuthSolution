using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthModel.Interfaces;
using AuthModel.ModelView.UserRole;
using MongoAuthService.Models;
using RepositoryCore.Exceptions;

namespace MongoAuthService.Services
{
    public class MongoUserRoleService<TUser, TRole, TUserRole> : IUserRoleRepository<TUser, TRole, TUserRole, string>
    where TUser : MongoUser
        where TUserRole : MongoUserRole
        where TRole : MongoRole
    {
        IAuthRepository<TUser, TUserRole, string> _user;
        IRoleRepository<TRole, string> _role;
        public MongoUserRoleService(IAuthRepository<TUser, TUserRole, string> user,
        IRoleRepository<TRole, string> role)
        {
            _user = user;
            _role = role;
        }
        //todo
        public async Task<bool> AddUserRole(AddUserRoleModel<string> model, string UserId)
        {
            var role = _role.Get(model.RoleId);// ?? throw new CoreException();
            if (role == null) throw new CoreException("Role not found", 20);
            var userRole = (TUserRole)Activator.CreateInstance(typeof(TUserRole));
            userRole.RoleId = role.Id;
            userRole.MongoRole = role;
            userRole.AddUserId = UserId;
            var user = _user.Get(model.UserId);//?? throw new CoreException();
            if (user == null) throw new CoreException("User not found", 0);
            user.UserRoles.Add(userRole);
            await _user.Update(user);
            return true;
        }

        public async Task<bool> DeleteUserRole(AddUserRoleModel<string> model, string userId)
        {
            var user = _user.Get(model.UserId);
            var userRole = user.UserRoles.FirstOrDefault(m => m.RoleId == model.RoleId);
            user.UserRoles.Remove(userRole);
            await _user.Update(user);
            return true;
        }

        public List<TRole> GetUserRoles(string userId)
        {
            var user = _user.Get(userId);
            if (user == null)
            {
                throw new CoreException("User not found", 0);
            }
            return user.UserRoles.Select(m => (TRole)m.MongoRole).ToList();

        }
    }
}
