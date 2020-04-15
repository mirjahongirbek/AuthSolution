using AuthModel.Models.Entitys;
using AuthModel.ModelView.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthModel.Interfaces
{
    public interface IUserRoleRepository<TUser, TRole, TUserRole, TKey>
       where TUser : IdentityUser<TKey>
       where TUserRole : IdentityUserRole<TKey>
       where TRole : IdentityRole<TKey>
        // where TDeleteData:DeleteData
    {
        Task<bool> AddUserRole(AddUserRoleModel<TKey> model, TKey UserId);
        List<TRole> GetUserRoles(TKey userId);
        Task<bool> DeleteUserRole(AddUserRoleModel<TKey> model, TKey userId);
    }
}
