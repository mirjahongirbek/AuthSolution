﻿using AuthModel.Models.Entitys;
using AuthModel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthModel.Interfaces
{
    public interface IAuthRepository<TUser, TUserRole, TKey>
       where TUser : IdentityUser<TKey>
        where TUserRole : IdentityUserRole<TKey>
    {
        #region Get
        Task<TUser> GetByEmail(string model);
        Task<TUser> GetByUserName(string userName);
        TUser Get(TKey id);
        TUser GetFirst(Expression<Func<TUser, bool>> expression);
        #endregion
        #region  Add
        bool AddUser(TUser user);
        Task<bool> RegisterAsync(TUser model);
        #endregion
        bool CheckUserOtp(TUser user, string otp);
        #region Role
        void AddUserRole(TKey userId, TUserRole userRole);
        #endregion
        Task<bool> Delete(TKey id);
        LoginResult LoginByRefresh(string refreshToken);
        LoginResult Login(TUser user);
        Task<(LoginResult, TUser)> Login(LoginViewModal model);
        Task<LoginResult> LoginResult(LoginViewModal model);
        Task<RegisterResult> RegisterAsync(RegisterUser model);
        Task Update(TUser user);
        IEnumerable<TUser> FindAll();
        IEnumerable<TUser> Find(Expression<Func<TUser, bool>> expression);
        long Count();
        long Count(Expression<Func<TUser, bool>> expression);
        Task<bool> Delete(TUser user);
        void SetRefresh(TUser user);
        #region Check
        TUser CheckUser(string userName);
        TUser CheckUserByPhone(string userName, string phoneNumber);
        TUser CheckUserByUserName(string userName, string Password);
        Task<LoginResult> RestorePassword(RestorePasswordModel model);
        #endregion
        #region Otp
        void SetOtp(ClaimsPrincipal user, string Otp);
        TUser SetOtp(TKey UserId, string otp);
        TUser SetOtp(string username, string otp);
        bool SetOtp(TUser user, string otp);
        Task<bool> ChangePassword(TKey userId, ChangePasswordModel model);
        Task<bool> ChangePassword(TUser user, ChangePasswordModel model);
        Task<LoginResult> ActivateUser(ActivateUserModel model);
        #endregion

    }
}
