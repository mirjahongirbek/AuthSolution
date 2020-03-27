using RepositoryCore.Enums.Enum;
using RepositoryCore.Interfaces;
using System.Collections.Generic;

namespace AuthService.Models
{
    public class IdentityRole<TKey> : IEntity<TKey>      
    {        
        public virtual TKey Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
        public RoleEnum Roles { get; set; }
        public virtual string Description { get; set; }
        public List<RoleActions> ActionsList { get; set; }
        public TableStatus TableStatus { get; set; }
        public virtual TKey AddUserId { get; set; }
        //TableStatus
    }


}
