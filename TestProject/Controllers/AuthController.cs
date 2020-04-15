using AuthModel.Interfaces;
using AuthService.Controller;
using Microsoft.AspNetCore.Mvc;
using MongoAuthService.Models;
using System;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : AuthController<MongoUser, MongoUserRole, string>
    {
        public AuthController(IAuthRepository<MongoUser, MongoUserRole, string> auth) : base(auth)
        {

        }
        protected override void SendSms(string phoneNumber, string otpCode)
        {
            Console.WriteLine(phoneNumber + "   [][]" + otpCode);
        }

        protected override void SenNotify(MongoUser phoneNumber, string otpCode)
        {
            throw new NotImplementedException();
        }
    }

}
