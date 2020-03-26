using AuthService.Models;
using AuthService.ModelView.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Interfaces.Service
{
    public interface IUserRoleRepository<TUser, TRole, TUserRole, TDeleteData>
       where TUser : IdentityUser
       where TUserRole : IdentityUserRole
       where TRole : IdentityRole
        where TDeleteData:DeleteData
    {
        Task<bool> AddUserRole(AddUserRoleModel model, int UserId);
        List<TRole> GetUserRoles(int userId);
        Task<bool> DeleteUserRole(AddUserRoleModel model, int userId);
    }
    public interface IDeleteDataService<TDeleteData>
        where TDeleteData: DeleteData
    {
       DeleteData AddData(string tableName, int UserId, object data, string schemeName = "");
    }
}
