using AuthService.Models;
using AuthService.ModelView.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Interfaces.Service
{
    public interface IUserRoleRepository<TUser, TRole, TUserRole, TDeleteData, TKey>
       where TUser : IdentityUser<TKey>
       where TUserRole : IdentityUserRole
       where TRole : IdentityRole<TKey>
        where TDeleteData:DeleteData
    {
        Task<bool> AddUserRole(AddUserRoleModel model, int UserId);
        List<TRole> GetUserRoles(int userId);
        Task<bool> DeleteUserRole(AddUserRoleModel model, int userId);
    }
}
