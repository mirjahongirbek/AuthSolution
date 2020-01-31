using AuthService.Controller;
using AuthService.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using TestProject.Models.User;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : AuthController<User, UserRole>
    {
        IAuthRepository<User, UserRole> _user;
        public AuthController(IAuthRepository<User, UserRole> user) : base(user)
        {
            _user = user; 
        }


    }
}
