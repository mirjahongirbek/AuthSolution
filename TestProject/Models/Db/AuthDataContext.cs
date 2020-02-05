using Microsoft.EntityFrameworkCore;
using TestProject.Models.User;
namespace TestProject.Models.Db
{
    public class AuthDataContext :DbContext, EntityRepository.Context.IDbContext
    {


        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TestProject.Models.User.User> Users { get; set; }
        public DbSet<TestProject.Models.User.Role> Roles { get; set; }
        public DbSet<AuthService.Models.DeleteData> DeleteData { get; set; }
        public DbContext DataContext => this;
        protected   override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=TestAuthDb.db;");

        }

    }
}
