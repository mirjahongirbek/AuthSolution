using AuthService.Enum;
using Newtonsoft.Json;
using RepositoryCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Models
{
   
    public  class IdentityUserRole<TKey> : IEntity<TKey>
    {
      
        public TKey Id { get; set; }
        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a role.
        /// </summary>
      
        public virtual TKey UserId { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
      
        public virtual TKey RoleId { get; set; }
      
        public virtual TKey AddUserId { get; set; }
      
        public virtual string Changes { get; set; }
        public virtual UserStatus Status { get; set; }
        [NotMapped]
        public List<UserRoleChange> UserRoleChange
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(Changes)) return new List<UserRoleChange>();
                    var result = JsonConvert.DeserializeObject<List<UserRoleChange>>(Changes);
                    return result;
                }
                catch (Exception ext)
                {
                    return new List<UserRoleChange>();
                }
            }
        }
        public void AddUserRole(UserRoleChange userRoleChange)
        {
            var addModel = UserRoleChange;
            addModel.Add(userRoleChange);
            Changes = JsonConvert.SerializeObject(addModel);
        }
    }
    


}
