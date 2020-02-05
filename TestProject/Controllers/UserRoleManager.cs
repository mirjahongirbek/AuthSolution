using AuthService.Controller;
using AuthService.Interfaces.Service;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using TestProject.Models.User;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleManager:UserRoleController<User, UserRole, Role, DeleteData>
    {
        IUserRoleRepository<User, Role, UserRole, DeleteData> _userRole;
        public UserRoleManager(IUserRoleRepository<User, Role, UserRole, DeleteData> userRole):base(userRole)
        {
            _userRole = userRole;
        }
    }
    
}
