
using AuthModel.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoAuthService.Models
{
    public class MongoUser : IdentityUser<string>
    {
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        //public override string Id { get; set; }
        public List<MongoUserRole> UserRoles { get; set; } = new List<MongoUserRole>();

        public override void AddDeviceId(string deviceId, string deviceName)
        {
            var device = DeviceList?.FirstOrDefault(m => m.DeviceId == deviceId);

            if (device != null)
            {
                device.DeviceName = deviceName;
                return;
            }
            device = new DeviceInfo()
            {
                AddDate = DateTime.Now,
                DeviceId = deviceId,
                DeviceName = deviceName,
                LastInCome = DateTime.Now,
            };
            if (DeviceList == null) DeviceList = new List<DeviceInfo>();
            DeviceList.Add(device);
        }
        public override void ChangeLastIncome(string deviceId)
        {
            var device = DeviceList?.FirstOrDefault(m => m.DeviceId == deviceId);
            if (device == null) return;
            device.LastInCome = DateTime.Now;

        }
        public override bool CheckDevice(string deviceId)
        {
            var device = DeviceList?.FirstOrDefault(m => m.DeviceId == deviceId);
            return (device != null) ? true : false;
        }
    }

}
