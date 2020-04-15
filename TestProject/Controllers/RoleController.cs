using AuthModel.Interfaces;
using AuthService.Controller;
using Microsoft.AspNetCore.Mvc;
using MongoAuthService.Models;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : RoleManagerController<MongoRole, string>
    {
        public RoleController(IRoleRepository<MongoRole, string> repo) : base(repo)
        {

        }
    }    

}
