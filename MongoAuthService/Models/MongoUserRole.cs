﻿using AuthModel.Models.Entitys;


namespace MongoAuthService.Models
{
    public class MongoUserRole : IdentityUserRole<string>
    {
        public MongoRole MongoRole { get; set; }
    }
  

}
