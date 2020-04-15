using AuthModel.Enum;
using AuthModel.ModelView;

using RepositoryCore.CoreState;
using RepositoryCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthModel.Models.Entitys
{
    public abstract partial class IdentityUser<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string NormalizedUserName { get { return UserName?.ToLower(); } }
        public virtual string Email { get; set; }
        public virtual string LastOtp { get; set; }
        public virtual DateTime LastOtpDate { get; set; }
        public virtual DateTime LastLoginDate { get; set; }
        public virtual int ErrorOtpCount { get; set; }
        public virtual bool Confirmed { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual List<DeviceInfo> DeviceList { get; set; }
        public virtual string Password { get; set; }
        public virtual string RefreshToken { get; set; }
        public virtual string Token { get; set; }
        public virtual bool PhoneNumberConfirmed { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual int Position { get; set; }
        public virtual bool IsSendOtp { get; set; }

    }

    public abstract partial class IdentityUser<TKey>
    {
        public bool CheckOtp(string otp)
        {
            if (LastOtp == otp)
            {
                return true;
            }
            return false;
        }
        public void Create<TKey>(RegisterUser reg)
        {
            if (AuthModalOption.SetNameAsPhone)
            {
                UserName = RepositoryState.ParsePhone(reg.UserName);
                PhoneNumber = UserName;
            }
            else
                UserName = reg.UserName;
            Password = RepositoryState.GetHashString(reg.Password);
            Email = reg.Email;
        }
        public abstract bool CheckDevice(string deviceId);

        public abstract void AddDeviceId(string deviceId, string deviceName);

        [NotMapped]
        public bool ShouldSendOtp { get; set; }
        public abstract void ChangeLastIncome(string deviceId);
    }

}
