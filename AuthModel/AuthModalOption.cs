using AuthModel.Attrubutes;
using AuthModel.Models.Entitys;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace AuthModel
{
  public static  class AuthModalOption
    {
        public static string ISSUER { get; set; } = "MyAuthServer"; // издатель токена
        public static string AUDIENCE { get; set; } = "http://localhost:2600/"; // потребитель токена
        public static string KEY { get; set; } = "mysupersecret_secretkey!123"; // ключ для шифрации
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
        public static List<Claim> GenerateClaims<TUser, TRole, TKey>(TUser user, List<TRole> roles)
          where TUser : IdentityUser<TKey>
          where TRole : IdentityRole<TKey>
        {
            var usr = user.GetType();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("position", user.Position.ToString()));
            claims.Add(new Claim("email", user.Email ?? ""));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));
                foreach (var i in role.ActionsList)
                {
                    claims.Add(new Claim("actions", i.ActionName));
                }
            }
            foreach (var i in usr.GetProperties())
            {
                var token = i.GetCustomAttribute<TokenAttribute>();
                if (token == null)
                    continue;
                var name = string.IsNullOrEmpty(token.Name) ? i.Name : token.Name;
                if (claims.FirstOrDefault(m => m.Type == name) == null)
                {
                    if (i.GetValue(user) != null)
                        claims.Add(new Claim(name.ToLower(), i.GetValue(user).ToString().ToLower()));
                }
            }
            return claims;
        }
        public static string SetToken<TUser, TKey>(List<Claim> claims, TUser user = null)
            where TUser : IdentityUser<TKey>
        {
            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                 AuthModalOption.ISSUER,
                 AuthModalOption.AUDIENCE,
                 notBefore: now,
                 claims: claimsIdentity.Claims,
                 expires: now.Add(TimeSpan.FromMinutes(AuthModalOption.LIFETIME)),
                 signingCredentials: new SigningCredentials(AuthModalOption.GetSymmetricSecurityKey(),
                     SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            if (user != null)
            {
                user.Token = token;
                user.LastLoginDate = DateTime.Now;
            }
            return token;
        }
    }
}
