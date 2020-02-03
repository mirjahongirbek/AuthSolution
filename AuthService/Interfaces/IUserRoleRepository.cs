using AuthService.Models;
using AuthService.ModelView.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Interfaces.Service
{
    public interface IUserRoleRepository<TUser, TRole, TUserRole>
       where TUser : IdentityUser
       where TUserRole : IdentityUserRole
       where TRole : IdentityRole
    {
        Task<bool> AddUserRole(AddUserRoleModel model, int UserId);
        List<TRole> GetUserRoles(int userId);
    }
}
