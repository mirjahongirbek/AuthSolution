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
    
    public class UserRoleController<TUser, TUserRole, TRole, TDeleteData> : ControllerBase
         where TUser : IdentityUser
         where TUserRole : IdentityUserRole
          where TRole : IdentityRole
        where TDeleteData:DeleteData
    {
      
        IUserRoleRepository<TUser, TRole, TUserRole, TDeleteData> _userRole;
        public UserRoleController(IUserRoleRepository<TUser, TRole, TUserRole, TDeleteData> userRole
            
            )
        {
         
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
        public async Task<NetResult<List<RoleResult<TRole>>>> GetUserRoles()
        {
            try
            {
                var roles = _userRole.GetUserRoles(this.UserId()).Select(m => new RoleResult<TRole>(m)).ToList();
                return roles;
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
      
        [HttpPost]
        public async Task<object> DeleteUserRole([FromBody]AddUserRoleModel model)
        {
            SuccessResult result = new SuccessResult();
            try
            {
               result.Success=await _userRole.DeleteUserRole(model, this.UserId());

            }catch(Exception ext)
            {
                result.Success = false;    
            }
            return result;
        }

        


    }

}
