﻿using AuthModel;
using AuthModel.Attrubutes;
using AuthModel.ModelView;
using AuthService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryCore.CoreState;
using RepositoryCore.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EntityRepository.Services
{
    public partial class EntityUserService<TUser, TRole, TUserRole>
    {
        /// <summary>
        ///  this Method use for Activate user Using sms confirm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<LoginResult> ActivateUser(ActivateUserModel model)
        {
            
            TUser user = null;
            if (AuthModalOption.SetNameAsPhone)
            {
                user =  GetFirst(m => m.UserName == RepositoryState.ParsePhone(model.UserName));
            }
            else
            {
                user = GetFirst(m => m.UserName == model.UserName);
            }
            if (CheckUserOtp(user, model.Otp))
            {
                user.AddDeviceId(model.DeviceId, model.DeviceName);
                await Update(user);
                return Login(user);
            }
            else
            {
                throw new CoreException("Activate User", 1);
            }

        }
        /// <summary>
        /// this method used for Login result Login Result
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual LoginResult Login(TUser user)
        {
            if (user == null) { return null; }
            var roles = GetRoles(user);
            SetToken(Claims(user), user);
            Update(user).Wait();
            LoginResult loginResult = new LoginResult()
            {
                AccessToken = user.Token,
                UserName = user.UserName,
                RefreshToken = user.RefreshToken,
                UserId = user.Id.ToString(),
                Roles = roles.Select(m => m.Name).ToList()
            };
            return loginResult;
        }
        /// <summary>
        /// Set Refresh token for user
        /// </summary>
        /// <param name="user"></param>
        public void SetRefresh(TUser user)
        {
            var refresh = "";
            var random = new Random();
            for (var i = 0; i < 10; i++) refresh += random.Next(15);
            user.RefreshToken = RepositoryState.GetHashString(refresh);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public LoginResult LoginByRefresh(string refreshToken)
        {
            var user = GetFirst(m => m.RefreshToken == refreshToken);
            return Login(user);
        }
        public virtual async Task<ClaimsIdentity> LoginClaims(string username, string password)
        {
            var user = await _dbSet.FirstOrDefaultAsync(m => m.UserName == username
            && m.Password == RepositoryState.GetHashString(password));
            if (user == null) { return null; }
            _userRole.Where(m => m.UserId == user.Id);
            var clams = Claims(user);
            if (clams == null)
            {
                return null;
            }
            var claimsIdentity = new ClaimsIdentity(clams, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        public string SetToken(List<Claim> claims, TUser user = null)
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
                SetRefresh(user);
                user.LastLoginDate = DateTime.Now;
            }
            return token;
        }
        public virtual async Task<(LoginResult, TUser)> Login(LoginViewModal model)
        {
            if (AuthModalOption.SetNameAsPhone)
            {
                model.UserName = RepositoryState.ParsePhone(model.UserName);
            }
            var user = _dbSet.Where(m => (m.UserName == model.UserName || m.Email == model.UserName) && m.Password == RepositoryState.GetHashString(model.Password)).FirstOrDefault();
            if (user == null)
            {
                throw new CoreException("User Name or Password not Found", 7);

            }

            if (AuthModalOption.CheckDeviceId)
            {
                if (user.CheckDevice(model.DeviceId))
                {
                    user.ChangeLastIncome(model.DeviceId);
                    return (Login(user), user);
                }
                user.ShouldSendOtp = true;
                //TODO
                return (null, user);
            }
            return (Login(user), user);

        }
        public virtual List<Claim> Claims(TUser user)
        {
            var usr = user.GetType();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("position", user.Position.ToString()));
            claims.Add(new Claim("email", user.Email ?? ""));
            var roles = GetRoles(user);
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

    }
}
