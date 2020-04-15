using AuthModel.Interfaces;
using AuthService.Controller;
using Microsoft.AspNetCore.Mvc;
using MongoAuthService.Models;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleController : UserRoleController<MongoUser, MongoUserRole, MongoRole, string>
    {
        public UserRoleController(IUserRoleRepository<MongoUser, MongoRole, MongoUserRole, string> userRole) : base(userRole)
        {

        }
    }

}
