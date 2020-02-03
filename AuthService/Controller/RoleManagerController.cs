using AuthService.Interfaces.Service;
using AuthService.Models;
using AuthService.ModelView;
using AuthService.ModelView.Roles;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        public virtual async Task<NetResult<SuccessResult>> AddRole([FromBody] TRole model)
        {
            SuccessResult result = new SuccessResult();
            bool success = await _roles.AddRole(model, this.UserId);
            result.Id = model.Id;
            return result;
        }
        [HttpGet]
        public virtual NetResult<List<RoleResult<TRole>>> GetRoles()
        {
            var result = _roles.FindAll().Select(m => new RoleResult<TRole>(m)).ToList();
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
        public virtual NetResult<object> DeleteRole(int id)
        {
            return null;
        }
        [HttpPut]
        public virtual NetResult<object> UpdateRole([FromBody]TRole model)
        {
            return null;
        }

    }

}
