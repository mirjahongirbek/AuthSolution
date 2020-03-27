using AuthService.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoAuthService.Models
{
    public class MongoRole : IdentityRole<string>
    {
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
       // public override string Id { get; set; }
        
    }

}
