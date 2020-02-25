using AuthService.Attributes;
using AuthService.ModelView;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Exceptions;
using RepositoryCore.Models;
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
        /// <summary>
        /// <description>
        /// Check User by Action Name    Example "ByAction"
        /// </description>
        /// <result>
        /// {
        ///    "statusCode": 0,
        ///    "result": {
        ///        "success": false,
        ///        "id": 0
        ///    },
        ///    "id": null,
        ///    "success": true,
        ///    "httpStatus": 200,
        ///    "error": null
        ///  } 
        /// </result>
        /// </summary>
        /// <returns></returns>
  
        [Auth(true)]
        [HttpGet]
        public NetResult<SuccessResult> ByAction()
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

        /// <summary>
        /// <response>
        /// response status: 401
        /// {
        ///    "statusCode": 0,
        ///    "result": null,
        ///    "id": null,
        ///    "success": true,
        ///    "httpStatus": 401,
        ///    "error": {
        ///        "code": 401,
        ///        "message": "Unuthorize Joha"
        ///    }
        ///}
        /// </response>
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// <response>
        /// {
        ///    "statusCode": 0,
        ///    "result": {
        ///        "success": true,
        ///        "id": 0
        ///    },
        ///    "id": null,
        ///    "success": true,
        ///    "httpStatus": 200,
        ///    "error": null
        ///}
        /// </response>
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// <response>
        /// response status: 401
        /// {
        ///    "statusCode": 0,
        ///    "result": null,
        ///    "id": null,
        ///    "success": true,
        ///    "httpStatus": 401,
        ///    "error": {
        ///        "code": 401,
        ///        "message": "Unuthorize Joha"
        ///    }
        ///}
        /// </response>
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// <response>
        /// {
        ///    "statusCode": 0,
        ///    "result": {
        ///        "success": true,
        ///        "id": 0
        ///    },
        ///    "id": null,
        ///    "success": true,
        ///    "httpStatus": 200,
        ///    "error": null
        ///}
        ///</response>
        /// </summary>
        /// <returns></returns>
        [Auth(ClaimTypes.Role, "admin")]
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
        /// <summary>
        /// <response>
        /// response status: 401
        /// {
        ///    "statusCode": 0,
        ///    "result": null,
        ///    "id": null,
        ///    "success": true,
        ///    "httpStatus": 401,
        ///    "error": {
        ///        "code": 401,
        ///        "message": "Unuthorize Joha"
        ///    }
        ///}
        /// </response>
        /// </summary>
        /// <returns></returns>
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
