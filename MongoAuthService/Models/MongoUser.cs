using AuthService.Models;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoAuthService.Models
{
    public class MongoUser: IdentityUser<string>
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public override string Id { get; set; }
        public List<MongoUserRole> UserRoles { get; set; } = new List<MongoUserRole>();
    }

}
