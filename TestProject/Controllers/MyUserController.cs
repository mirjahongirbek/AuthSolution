using AuthService.Attributes;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Exceptions;
using System;
using System.Linq;
using TestProject.Models.User;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class MyUserController : ControllerBase
    {
        public MyUserController()
        {


        }
       [Auth(true)]
        [HttpGet]
        public NetResult<User> MyUser()
        {
            try
            {
               var item= User.FindAll("joha").ToList();
               var ss= item.Any(m => m.Value == "joha1");
               var lses= User.FindFirst("joha");
                throw new CoreException(5);

            }catch(Exception ext)
            {
                return ext;
            }
        }

    }
    
}
