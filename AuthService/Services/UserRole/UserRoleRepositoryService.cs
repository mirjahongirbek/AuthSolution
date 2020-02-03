
using AuthService.Interfaces.Service;
using AuthService.Models;
using AuthService.ModelView.UserRole;
using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Services.UserRole
{
    public class UserRoleRepositoryService<TUser, TRole, TUserRole> : IUserRoleRepository<TUser, TRole, TUserRole>
       where TUser : IdentityUser
       where TUserRole : IdentityUserRole
       where TRole : IdentityRole
    {
        public DbSet<TUserRole> _dbSet;
        IRoleRepository<TRole> _roles;
        public UserRoleRepositoryService(IDbContext context,
            IRoleRepository<TRole> roles)
        {
            _dbSet = context.DataContext.Set<TUserRole>();
            _roles = roles;
        }
        public async Task<bool> AddUserRole(AddUserRoleModel model, int UserId)
        {
            var userRole = _dbSet.FirstOrDefault(m => m.UserId == model.UserId && m.RoleId == model.RoleId);
            if (userRole != null)
            {

            }
            userRole = (TUserRole)Activator.CreateInstance(typeof(TUserRole));
            userRole.UserId = model.UserId;
            userRole.RoleId = model.RoleId;
            UserRoleChange userRoleChange = new UserRoleChange()
            {
                ChangeDate = DateTime.Now,
                ChangeUserId = UserId,
                Description = "User Role add"
            };
            userRole.AddUserRole(userRoleChange);
            _dbSet.Add(userRole);
            return true;
        }

        public List<TRole> GetUserRoles(int userId)
        {
            var userRoles = _dbSet.Where(m => m.UserId == userId).ToList();
            List<TRole> roles = new List<TRole>();

            foreach (var i in userRoles)
            {
                var role = _roles.Get(i.RoleId);
                roles.Add(role);
            }
            return roles;
        }
    }
}
