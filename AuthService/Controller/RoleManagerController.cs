using AuthService.Attributes;
using AuthService.Interfaces.Service;
using AuthService.Models;
using AuthService.ModelView;
using AuthService.ModelView.Roles;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Controller
{
    public class RoleManagerController<TRole> : ControllerBase
        where TRole : IdentityRole
    {
        IRoleRepository<TRole> _roles;
        public RoleManagerController(IRoleRepository<TRole> role)
        {
            _roles = role;
        }
        [HttpPost]
        //[Auth(ClaimTypes.Name, "CanReadResource")]
        public virtual async Task<NetResult<SuccessResult>> AddRole([FromBody] TRole model)
        {
            try
            {
                SuccessResult result = new SuccessResult();
                bool success = await _roles.AddRole(model, this.UserId());
                result.Id = model.Id;
                return result;
            }
            catch(Exception ext)
            {
                return ext;
            }
            
        }
        [HttpGet]
        [Auth(ClaimTypes.Role, "CanReadResource")]
        public virtual NetResult<List<RoleResult<TRole>>> GetActiveRoles()
        {
            var result = _roles.Find(m=>m.TableStatus== RepositoryCore.Enums.Enum.TableStatus.Active).Select(m => new RoleResult<TRole>(m)).ToList();
            return result;
        }
        [HttpGet]
       
        public virtual NetResult<RoleResult<TRole>> GetRoleById(int id)
        {
            var role = _roles.Get(id);
            var result = new RoleResult<TRole>(role);
            return result;
        }
        [HttpDelete]
        //[Auth("RoleManager","actionName","sdsd","sdcsd")]
        public virtual async Task<NetResult<SuccessResult>> DeleteRole(int id)
        { SuccessResult result = new SuccessResult();
            try
            {
               
               result.Success=await _roles.DeleteRole(id, this.UserId());
                
            }
            catch(Exception ext)
            {
                result.Success = false;
                
            }
            return result;

        }
        [HttpPut]
        public virtual async Task<NetResult<SuccessResult>> UpdateRole([FromBody]TRole model)
        {
            SuccessResult result = new SuccessResult();
            try
            {
               result.Success=await _roles.UpdateRole(model, this.UserId());

            }catch(Exception ext)
            {
                result.Success = false;
            }
            return result;
        }

    }

}
