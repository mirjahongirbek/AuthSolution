using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Exceptions;
using System;
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
        [HttpGet]
        public NetResult<User> MyUser()
        {
            try
            {
                throw new CoreException(5);

            }catch(Exception ext)
            {
                return ext;
            }
        }

    }
    
}
