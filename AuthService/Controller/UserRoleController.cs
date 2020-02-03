using AuthService.Interfaces.Service;
using AuthService.Models;
using AuthService.ModelView;
using AuthService.ModelView.Roles;
using AuthService.ModelView.UserRole;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controller
{
    public class UserRoleController<TUser, TUserRole, TRole> : ControllerBase
         where TUser : IdentityUser
         where TUserRole : IdentityUserRole
          where TRole : IdentityRole
    {
        IAuthRepository<TUser, TUserRole> _user;
        IRoleRepository<TRole> _roles;
        IUserRoleRepository<TUser, TRole, TUserRole> _userRole;
        public UserRoleController(IAuthRepository<TUser, TUserRole> user, IRoleRepository<TRole> roles,
            IUserRoleRepository<TUser, TRole, TUserRole> userRole
            )
        {
            _user = user;
            _roles = roles;
            _userRole = userRole;
        }
        [HttpPost]
        public async Task<NetResult<SuccessResult>> AddUserToRole([FromBody]AddUserRoleModel model)
        {
            try
            {
                SuccessResult result = new SuccessResult();
                result.Success = await _userRole.AddUserRole(model, this.UserId());
                return result;
            }
            catch (Exception ext) { return ext; }
        }
        [HttpGet]
        public async Task<NetResult<List<RoleResult<TRole>>>> GetUserRoles(int UserId)
        {
            try
            {
                var roles = _userRole.GetUserRoles(UserId).Select(m => new RoleResult<TRole>(m)).ToList();
                return roles;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        


    }

}
