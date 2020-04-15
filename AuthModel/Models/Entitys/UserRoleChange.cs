using System;

namespace AuthModel.Models.Entitys
{
    public class UserRoleChange
    {
        public int ChangeUserId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Description { get; set; }
    }
}
