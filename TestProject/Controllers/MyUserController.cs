using AuthService.Attributes;
using AuthService.ModelView;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Exceptions;
using System;
using System.Linq;
using System.Security.Claims;

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
        public NetResult<SuccessResult> ByAction()
        {
            try
            {

                return new SuccessResult() { Success = true };

            }catch(Exception ext)
            {
                return ext;
            }
        }
        [Auth(true)]
        public NetResult<SuccessResult> OtherAction()
        {
            try
            {

                return new SuccessResult() { Success = true };

            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [HttpGet]
        [Auth(5)]
        public NetResult<SuccessResult> ByPositon()
        {
            try
            {
                return new SuccessResult() { Success = true };

            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [HttpGet]
        [Auth(10)]
        public NetResult<SuccessResult> OtherPosition()
        {
            try
            {
                return new SuccessResult() { Success = true };

            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [Auth(ClaimTypes.Role,"admin")]
        public NetResult<SuccessResult> ByRoleName()
        {
            try
            {
                return new SuccessResult() { Success = true };
            }
            catch (Exception ext)
            {
                return ext;
            }
        }
        [Auth(ClaimTypes.Role, "OtherRole")]
        public NetResult<SuccessResult> OtherRole()
        {
            try
            {
                return new SuccessResult() { Success = true };
            }
            catch (Exception ext)
            {
                return ext;
            }
        }

    }
    
}
