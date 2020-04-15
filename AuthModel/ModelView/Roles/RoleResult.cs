
using AuthModel.Models.Entitys;
using System.Collections.Generic;

namespace AuthModel.ModelView.Roles
{
    public class RoleResult<TRole, TKey>
        where TRole : IdentityRole<TKey>
    {

        public RoleResult(TRole model)
        {
            Id = model.Id;
            Name = model.Name;
            Actions = model.ActionsList;

        }
        public TKey Id { get; set; }
        public string Name { get; set; }
        public List<RoleActions> Actions { get; set; }

    }
}
