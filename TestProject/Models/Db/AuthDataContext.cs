using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoRepositorys.MongoContext;

namespace TestProject.Models.Db
{
    public class AuthDataContext :DbContext, EntityRepository.Context.IDbContext
    {


        //public DbSet<EntityUser> UserRoles { get; set; }
        //public DbSet<TestProject.Models.User.User> Users { get; set; }
        //public DbSet<TestProject.Models.User.Role> Roles { get; set; }
        //public DbSet<AuthService.Models.DeleteData> DeleteData { get; set; }
        public DbContext DataContext => this;
        protected   override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=TestAuthDb.db;");

        }

    }
    public class MongoDataContext: IMongoContext
    {
        public MongoDataContext(string connectionString)
        {
           var client= new MongoClient(connectionString);
           Database= client.GetDatabase("AuthTest");
        }
        public IMongoDatabase Database { get; }
    }
}
