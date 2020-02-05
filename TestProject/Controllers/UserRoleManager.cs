using AuthService.Controller;
using AuthService.Interfaces.Service;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using TestProject.Models.User;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleManagerController:UserRoleController<User, UserRole, Role, DeleteData>
    {
        IUserRoleRepository<User, Role, UserRole, DeleteData> _userRole;
        public UserRoleManagerController(IUserRoleRepository<User, Role, UserRole, DeleteData> userRole):base(userRole)
        {
            _userRole = userRole;
        }
    }
    
}
