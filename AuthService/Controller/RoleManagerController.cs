using AuthModel.Interfaces;
using AuthModel.Models.Entitys;
using AuthModel.ModelView;
using AuthModel.ModelView.Roles;
using AuthService.Attributes;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Controller
{
    public class RoleManagerController<TRole, TKey> : ControllerBase
        where TRole : IdentityRole<TKey>
    {
        IRoleRepository<TRole, TKey> _roles;
        public RoleManagerController(IRoleRepository<TRole, TKey> role)
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
                bool success = await _roles.AddRole(model, this.UserId<TKey>());
                result.Id = model.Id.ToString();
                return result;
            }
            catch(Exception ext)
            {
                return ext;
            }
            
        }
        [HttpGet]
        [Auth(ClaimTypes.Role, "CanReadResource")]
        public virtual NetResult<List<RoleResult<TRole, TKey>>> GetActiveRoles()
        {
            var result = _roles.Find(m=>m.TableStatus== RepositoryCore.Enums.Enum.TableStatus.Active).Select(m => new RoleResult<TRole, TKey>(m)).ToList();
            return result;
        }
        [HttpGet]
       
        public virtual NetResult<RoleResult<TRole, TKey>> GetRoleById(TKey id)
        {
            var role = _roles.Get(id);
            var result = new RoleResult<TRole, TKey>(role);
            return result;
        }
        [HttpDelete]
        //[Auth("RoleManager","actionName","sdsd","sdcsd")]
        public virtual async Task<NetResult<SuccessResult>> DeleteRole(TKey id)
        { SuccessResult result = new SuccessResult();
            try
            {
               
               result.Success=await _roles.DeleteRole(id, this.UserId<TKey>());
                
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
               result.Success=await _roles.UpdateRole(model, this.UserId<TKey>());

            }catch(Exception ext)
            {
                result.Success = false;
            }
            return result;
        }

    }

}
