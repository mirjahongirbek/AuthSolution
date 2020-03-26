using AuthService.Models;
using AuthService.ModelView.UserRole;

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
                Status = AuthService.Enum.UserStatus.Active
            };
            return entity;
        }
       
    }  

}
