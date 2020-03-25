using System;

namespace AuthService.Models
{
    public class DeleteData
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string TableName { get; set; }
        public string SchemeName { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
    }
   


}
