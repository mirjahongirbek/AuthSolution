using AuthService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityRepository.Models
{
    public class EntityUser : IdentityUser<int>
    {

        public virtual string Devices { get; set; }
        private List<DeviceInfo> GetDevices()
        {
            if (string.IsNullOrEmpty(Devices))
            {
                return new List<DeviceInfo>();
            }
            return JsonConvert.DeserializeObject<List<DeviceInfo>>(Devices);
        }
        public override void AddDeviceId(string deviceId, string deviceName)
        {
            var devices = GetDevices();
            if (devices.FirstOrDefault(m => m.DeviceId == deviceId) != null)
            {
                return;
            }
            devices.Add(new DeviceInfo { AddDate = DateTime.Now, DeviceId = deviceId, DeviceName = deviceName });
            Devices = JsonConvert.SerializeObject(devices);
        }

        public override void ChangeLastIncome(string deviceId)
        {
            var devices = GetDevices();
            var obj = devices.FirstOrDefault(m => m.DeviceId == deviceId);
            obj.LastInCome = DateTime.Now;
            Devices = JsonConvert.SerializeObject(devices);
        }

        public override bool CheckDevice(string deviceId)
        {
            var devices = GetDevices();
            return devices.Any(m => m.DeviceId == deviceId);

        }
    }

}
