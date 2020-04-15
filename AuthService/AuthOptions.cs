
using AuthModel;
using AuthModel.Models.Entitys;
using AuthService.Attributes;
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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace AuthService
{
    public static class AuthOptions
    {
       
        public static void AddAuthSolutionService(this IServiceCollection service, string Key)
        {
            if (Key.Length >= 6)
            {
                AuthModalOption.KEY = Key;
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
                             ValidIssuer = AuthModalOption.ISSUER,

                             // будет ли валидироваться потребитель токена
                             ValidateAudience = true,
                             // установка потребителя токена
                             ValidAudience = AuthModalOption.AUDIENCE,
                             // будет ли валидироваться время существования
                             ValidateLifetime = true,

                             // установка ключа безопасности
                             IssuerSigningKey = AuthModalOption.GetSymmetricSecurityKey(),
                             // валидация ключа безопасности
                             ValidateIssuerSigningKey = true,
                         };
                     });
        }
        public static TKey UserId<TKey>(this ControllerBase cBase)
        {
            var userId = cBase.User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                cBase.Response.StatusCode = 401;
                throw new CoreException("Anuthorize", 401);

            }
            if(typeof(int).Name== typeof(TKey).Name)
            {
                return (TKey)(object)int.Parse(userId);

            }
            if(typeof(long).Name== typeof(TKey).Name)
            {
                return (TKey)(object)long.Parse(userId);
            }
            if(typeof(double).Name== typeof(TKey).Name)
            {
                return (TKey)(object)userId;
            }
            if(typeof(string).Name== typeof(TKey).Name)
            {
             return   (TKey)(object)userId;
            }
            return (TKey)(object)userId;           
        }
        public static bool IsAuthorize(this AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Claims.Count() == 0)
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
            var value = cBase.User.FindFirst(key.ToLower())?.Value;

            if (string.IsNullOrEmpty(value))
            {
                throw new CoreException("Claims not found ", 3);
            }
            if (typeof(T) == typeof(string))
            {
                return (T)(object)value;
            }
            if (typeof(int) == typeof(T))
            {
                return (T)(object)int.Parse(value);
            }
            return (T)(object)value;
        }
      

    }

}
