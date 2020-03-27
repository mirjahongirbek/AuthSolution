using AuthService.Attributes;
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

    public class UserRoleController<TUser, TUserRole, TRole, TKey> : ControllerBase
         where TUser : IdentityUser<TKey>
         where TUserRole : IdentityUserRole<TKey>
          where TRole : IdentityRole<TKey>
    {

        IUserRoleRepository<TUser, TRole, TUserRole, TKey> _userRole;
        public UserRoleController(IUserRoleRepository<TUser, TRole, TUserRole, TKey> userRole)
        {
            _userRole = userRole;

        }
        [HttpPost]
        public async Task<NetResult<SuccessResult>> AddUserToRole([FromBody]AddUserRoleModel<TKey> model)
        {
            try
            {
                SuccessResult result = new SuccessResult();
                result.Success = await _userRole.AddUserRole(model, this.UserId<TKey>());
                return result;
            }
            catch (Exception ext) { return ext; }
        }
        [HttpGet]
        public async Task<NetResult<List<RoleResult<TRole, TKey>>>> GetMyRoles()
        {
            try
            {
                var roles = _userRole.GetUserRoles(this.UserId<TKey>()).Select(m => new RoleResult<TRole, TKey>(m)).ToList();
                return roles;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [HttpGet]
        public async Task<NetResult<List<RoleResult<TRole, TKey>>>> GetUserRoles(TKey id)
        {
            try
            {
                var roles = _userRole.GetUserRoles(id).Select(m => new RoleResult<TRole, TKey>(m)).ToList();
                return roles;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }

        [HttpPost]
        public async Task<object> DeleteUserRole([FromBody]AddUserRoleModel<TKey> model)
        {
            SuccessResult result = new SuccessResult();
            try
            {
                result.Success = await _userRole.DeleteUserRole(model, this.UserId<TKey>());

            }
            catch (Exception ext)
            {
                result.Success = false;
            }
            return result;
        }




    }

}
