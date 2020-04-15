using AuthModel.Models.Entitys;
using AuthModel.ModelView.UserRole;

namespace EntityRepository.Models
{
    public class EntityUserRole : IdentityUserRole<int>
    {     
        public static EntityUserRole Create(AddUserRoleModel<int> model, int userId)
        {
            EntityUserRole entity = new EntityUserRole()
            {
                UserId = model.UserId,
                AddUserId = userId,
                RoleId = model.RoleId,
                Status = AuthModel.Enum.UserStatus.Active
            };
            return entity;
        }
       
    }  

}
