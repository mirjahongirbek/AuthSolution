
using AuthService.Models;
using System.Collections.Generic;

namespace AuthService.ModelView.Roles
{
    public class RoleResult<TRole>
        where TRole :IdentityRole
    {
     
        public RoleResult(TRole model)
        {
            Id = model.Id;
            Name = model.Name;
            Actions= model.ActionsList;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoleActions> Actions { get; set; }
       
    }
}
