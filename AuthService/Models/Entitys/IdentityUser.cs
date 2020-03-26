using AuthService.Enum;
using AuthService.ModelView;
using RepositoryCore.CoreState;
using RepositoryCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Models
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

    public abstract partial  class IdentityUser<TKey>
    {
        public bool CheckOtp(string otp)
        { 


            return false;
        }
        public void Create<TKey>(RegisterUser reg)
        {
            UserName = reg.UserName;
            Password = RepositoryState.GetHashString(reg.Password);
            Email = reg.Email;
        }
        public abstract bool CheckDevice(string deviceId);
        //{
        //    var selectDevice = DeviceList.FirstOrDefault(m => m.DeviceId == deviceId);
        //    if (selectDevice == null)
        //    {
        //        return false;
        //    }
        //    selectDevice.LastInCome = DateTime.Now;
        //    Devices = JsonConvert.SerializeObject(DeviceList);
        //    return true;
        //}
        public abstract void AddDeviceId(string deviceId, string deviceName);
        //{
        //    if (CheckDevice(deviceId))
        //    {
        //        return;
        //    }
        //    DeviceInfo deviceInfo = new DeviceInfo
        //    {
        //        AddDate = DateTime.Now,
        //        DeviceId = deviceId,
        //        DeviceName = deviceName,
        //        LastInCome = DateTime.Now
        //    };
        //    userDevice.Add(deviceInfo);
        //    Devices = JsonConvert.SerializeObject(userDevice);

        //}
        [NotMapped]
        public bool ShouldSendOtp { get; set; }
        public abstract void ChangeLastIncome(string deviceId);
        //{
        //    if (!CheckDevice(deviceId))
        //    {
        //        return;
        //    }
        //    var myDevice = userDevice.FirstOrDefault(m => m.DeviceId == deviceId);
        //    myDevice.LastInCome = DateTime.Now;
        //    Devices = JsonConvert.SerializeObject(userDevice);

        //}

    }


    public class DeviceInfo
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime LastInCome { get; set; }
        public string Data { get; set; }
    }

}
