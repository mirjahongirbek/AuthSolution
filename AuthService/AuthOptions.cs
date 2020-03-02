using CoreResults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RepositoryCore.Exceptions;
using RepositoryCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuthService
{
    public static class AuthOptions
    {
        public static string ISSUER { get; set; } = "MyAuthServer"; // издатель токена
        public static string AUDIENCE { get; set; } = "http://localhost:2600/"; // потребитель токена
        private static string KEY { get; set; } = "mysupersecret_secretkey!123"; // ключ для шифрации
        public static int LIFETIME { get; set; } = 2000; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
        public static bool CheckDeviceId { get; set; } = true;
        public static bool CheckDefaulAction { get; set; } = false;
        public static TimeSpan Otp { get; set; } = TimeSpan.FromSeconds(180);
        public static bool IsSendOtp { get; set; } = true;
        public static bool SetNameAsPhone { get; set; } = true;
        public static int OtpTime { get; set; } = 100;
        public static void AddAuthSolutionService(this IServiceCollection service, string Key)
        {
            if (Key.Length >= 6)
            {
                AuthOptions.KEY = Key;
            }
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {
                         options.RequireHttpsMetadata = false;
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             // укзывает, будет ли валидироваться издатель при валидации токена
                             ValidateIssuer = true,
                             // строка, представляющая издателя
                             ValidIssuer = ISSUER,

                             // будет ли валидироваться потребитель токена
                             ValidateAudience = true,
                             // установка потребителя токена
                             ValidAudience = AUDIENCE,
                             // будет ли валидироваться время существования
                             ValidateLifetime = true,

                             // установка ключа безопасности
                             IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                             // валидация ключа безопасности
                             ValidateIssuerSigningKey = true,
                         };
                     });
        }
        public static int UserId(this ControllerBase cBase)
        {
            var userId = cBase.User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                cBase.Response.StatusCode = 401;
                throw new CoreException("Anuthorize", 401);

            }
            return int.Parse(userId);
        }
        public static bool IsAuthorize(this AuthorizationFilterContext context)
        {
           if(context.HttpContext.User.Claims.Count() == 0)
            {
                context.SetUnthorize();
              
                return true;
            }
            return false;
        }
        public static void SetUnthorize(this AuthorizationFilterContext context)
        {
            NetResult<ResponseData> result = new NetResult<ResponseData>()
            {
                HttpStatus = 401,
                Error = new RepositoryCore.Result.DefaultResult() { Code = 401, Message = "Unuthorize" }
            };
            context.HttpContext.Response.StatusCode = 401;
            context.Result = new CoreJsonResult(result);
        }
        public static T GetToken<T>(this ControllerBase cBase, string key)
        {
           var value= cBase.User.FindFirst(key.ToLower())?.Value;
            
            if (string.IsNullOrEmpty(value))
            {
                throw new CoreException("Claims not found ",3);                
            }
            if(typeof(T)==  typeof(string))
            {
                return (T)(object)value;
            }
            if(typeof(int)== typeof(T))
            {
              return (T)(object)int.Parse(value);
            }
            return (T)(object)value;
        }
    }

}
