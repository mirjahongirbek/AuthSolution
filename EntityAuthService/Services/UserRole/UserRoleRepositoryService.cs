using AuthService.Interfaces.Service;
using AuthService.ModelView.UserRole;
using EntityRepository.Models;
using RepositoryCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityRepository.Services
{
    public class EntityUserRoleService<TUser, TRole, TUserRole>
        : IUserRoleRepository<TUser, TRole, TUserRole, int>
       where TUser : EntityUser
       where TUserRole : EntityUserRole
       where TRole : EntityRole
    {
        IRepositoryCore<TUserRole, int> _userRole;
        IRoleRepository<TRole, int> _role;
        public EntityUserRoleService(
            IRepositoryCore<TUserRole, int> userRole,
        IRoleRepository<TRole, int> role
            )
        {
            _role = role;
            _userRole = userRole;
        }
        public async Task<bool> AddUserRole(AddUserRoleModel<int> model, int UserId)
        {
            var userRole = _userRole.GetFirst(m => m.UserId == model.UserId && m.RoleId == model.RoleId);
            if (userRole != null)
            {

            }
            userRole = (TUserRole)EntityUserRole.Create(model, UserId);
            _userRole.Add(userRole);
            return true;
        }

        public async Task<bool> DeleteUserRole(AddUserRoleModel<int> model, int userId)
        {
            _userRole.DeleteMany(m => m.UserId == model.UserId && m.RoleId == model.RoleId);
            return true;
        }

        public List<TRole> GetUserRoles(int userId)
        {
            var userRoles = _userRole.Find(m => m.UserId == userId).ToList();
            List<TRole> roles = new List<TRole>();
            foreach (var i in userRoles)
            {
                var role = _role.Get(i.RoleId);
                roles.Add(role);
            }
            return roles;
        }

        // public DbSet<TUserRole> _dbSet;
        //  IRoleRepository<TRole, TKey> _roles;

        //DbContext _context;
        //public UserRoleRepositoryService(IDbContext context,
        //    IRoleRepository<TRole, TKey> roles,
        //     IDeleteDataService<TDeleteData> deleteData

        //    )
        //{
        //    _dbSet = context.DataContext.Set<TUserRole>();
        //    _roles = roles;
        //    _context = context.DataContext;
        //    _deleteData = deleteData;
        //}
        //public void Add(TUserRole model)
        //{
        //    _dbSet.Add(model);
        //    _context.SaveChanges();

        //}
        //public async Task<bool> AddUserRole(AddUserRoleModel model, int UserId)
        //{
        //    var userRole = _dbSet.FirstOrDefault(m => m.UserId == model.UserId && m.RoleId == model.RoleId);
        //    if (userRole != null)
        //    {

        //    }
        //    userRole = (TUserRole)Activator.CreateInstance(typeof(TUserRole));
        //    userRole.UserId = model.UserId;
        //    userRole.RoleId = model.RoleId;
        //    UserRoleChange userRoleChange = new UserRoleChange()
        //    {
        //        ChangeDate = DateTime.Now,
        //        ChangeUserId = UserId,
        //        Description = "User Role add"
        //    };
        //    userRole.AddUserRole(userRoleChange);
        //    //_dbSet.Add(userRole);
        //    Add(userRole);
        //    return true;
        //}
        //public List<TRole> GetUserRoles(int userId)
        //{
        //    var userRoles = _dbSet.Where(m => m.UserId == userId).ToList();
        //    List<TRole> roles = new List<TRole>();

        //    foreach (var i in userRoles)
        //    {
        //        var role = _roles.Get(i.RoleId);
        //        roles.Add(role);
        //    }
        //    return roles;
        //}
        //public async Task<bool> DeleteUserRole(AddUserRoleModel model, int userId)
        //{
        //    var item = _dbSet.Where(m => m.UserId == model.UserId && m.RoleId == model.RoleId).ToList();
        //    if (item.Count == 0) return true;
        //    _dbSet.RemoveRange(item);
        //    var obj = JsonConvert.SerializeObject(item);
        //    var deleteData = (TDeleteData)Activator.CreateInstance(typeof(TDeleteData));
        //    //TODO item
        //    _deleteData.AddData("IdentityUserRole", userId, deleteData);
        //    return true;
        //}

    }
}
