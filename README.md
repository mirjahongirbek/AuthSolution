# AuthSolution

Postman Exeption
https://drive.google.com/drive/folders/1NI0CJZSyqMvOWj4NeXbJGsNfS1DJcJxt?usp=sharing

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

## Create first Models if you wont to add some new propertys in entitys

# Auth Attributes

Check by Roles

```sh
 [Auth(ClaimTypes.Role, "admin")]
 or
 [Auth(ClaimTypes.Role, "admin,nextRoles")]
```

##### By Position

Checking by Position
If User position upper then 10 or equal 10

```sh
 [Auth(10)]
```

##### Checking By Action Name

```sh
    [Auth(true)]
```

#### Or just chekking User authorize

```sh
[Auth]
```

```sh
    public class User: IdentityUser
    {
    }
    public class UserRole: IdentityUserRole
    {
    }
    public class Role :  IdentityRole
    {
    }
```

Connection to Db

```sh
 public class AuthDataContext :DbContext, EntityRepository.Context.IDbContext
    {
        public DbSet<UserRole> UserRoles { get; set; } //add DbSet to entitys
        public DbSet<TestProject.Models.User.User> Users { get; set; }
        public DbSet<TestProject.Models.User.Role> Roles { get; set; }
        public DbSet<AuthService.Models.DeleteData> DeleteData { get; set; }
        public DbContext DataContext => this;
        protected   override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Connection String");
        }
    }
```

Add Configuration Service

```sh
   public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbContext, AuthDataContext>();
            services.AddScoped<IAuthRepository<User, UserRole>, IdentityUserService<User, Role, UserRole>>();
            services.AddAuthSolutionService("mysupersecret_secretkey!123");
            services.AddScoped<IRoleRepository<Role>, IdentityRoleService<Role>>();
            services.AddScoped<IDeleteDataService<DeleteData>, DeleteDataService<DeleteData>>();
            services.AddScoped<IUserRoleRepository<User, Role, UserRole,DeleteData>, UserRoleRepositoryService<User, Role, UserRole, DeleteData>>();
           services.AddContextWithSwagger(); //this is Service in Core Result
        }
```

# Add in Middelware!

```sh
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
            app.UseAuthentication();// Add Authentication //TODO maybe change
            app.ContextWithSwagger(); //Core Result Swagger
            app.UseMvc();
    }
```

### Create AuthController

first Create some controller in project

```sh
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class AuthController : AuthController<User, UserRole>
  {
      IAuthRepository<User, UserRole> _user;
      public AuthController(IAuthRepository<User, UserRole> user) : base(user)
      {
          _user = user;
      }
      protected override void SendSms(string phoneNumber, string otpCode)
      {
          Console.WriteLine("Phone number :" + phoneNumber + "   Otp Code: " + otpCode);
      }
  }
```

### Create Role Controller for control in roles

```sh
  [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController: RoleManagerController<Role>
    {
        IRoleRepository<Role> _role;
        public  RolesController(IRoleRepository<Role> role):base(role)
        {
            _role = role;
        }
    }
```

### Create UserRoleManager controller for controling

```sh
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleManagerController:UserRoleController<User, UserRole, Role, DeleteData>
    {
        IUserRoleRepository<User, Role, UserRole, DeleteData> _userRole;
        public UserRoleManagerController(IUserRoleRepository<User, Role, UserRole, DeleteData> userRole):base(userRole)
        {
            _userRole = userRole;
        }
    }
```

### Todos

- Write MORE functionals

## License
