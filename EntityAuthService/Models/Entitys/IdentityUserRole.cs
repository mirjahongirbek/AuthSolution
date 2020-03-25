﻿using Newtonsoft.Json;
using RepositoryCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Models
{
    public class IdentityUserRole : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a role.
        /// </summary>
        [Column("user_id")]
        public virtual int UserId { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
        [Column("role_id")]
        public virtual int RoleId { get; set; }
        [Column("add_user_id")]
        public virtual int AddUserId { get; set; }
        [Column("changes")]
        public virtual string Changes { get; set; }
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
