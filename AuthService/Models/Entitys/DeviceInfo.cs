using System;

namespace AuthService.Models
{
    public class DeviceInfo
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime LastInCome { get; set; }
        public string Data { get; set; }
    }

}
