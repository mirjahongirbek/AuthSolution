using AuthService.Controller;
using AuthService.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using TestProject.Models.User;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController: RoleManagerController<Role>
    {
        IRoleRepository<Role> _role;
  public      RolesController(IRoleRepository<Role> role):base(role)
        {
            _role = role;
        }

    }
    
}
