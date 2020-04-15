using AuthService;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using CoreResults;
using EntityRepository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoAuthService;
using MongoAuthService.Models;
using MongoRepositorys.MongoContext;
using MongoRepositorys.Repository;
using RepositoryCore.Interfaces;
using System;
using TestProject.Models.Db;

namespace TestProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //CoreState.AddContextWithSwagger(services, "http://172.17.9.105:1600/api", "authTest", "test", "test");
            // services.AddScoped<IDbContext, AuthDataContext>();
            services.AddSingleton<IMongoContext>(new MongoDataContext("mongodb://127.0.0.1:27017"));
            //  services.AddScoped(typeof(IRepositoryCore<,>), typeof(MongoRepository<>));
            //AuthState.RegisterAuth<MongoUser, MongoRole, MongoUserRole>(services);
             services.AddAuthSolutionService("mysupersecret_secretkey!123");
            //services.AddScoped<IAuthRepository<User, UserRole, int>, IdentityUserService<User, Role, UserRole>>();
            //services.AddScoped<IRoleRepository<Role>, IdentityRoleService<Role>>();
            //services.AddScoped<IDeleteDataService<DeleteData>, DeleteDataService<DeleteData>>();
            //services.AddScoped<IUserRoleRepository<User, Role, UserRole,DeleteData>, UserRoleRepositoryService<User, Role, UserRole, DeleteData>>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddContextWithSwagger();
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IRepositoryCore<,>)).InstancePerDependency();
            builder.Populate(services);
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.ContextWithSwagger();
            app.UseMvc();

        }
    }

}
/* public void SwaggerService(IServiceCollection services)
        {
            services.AddSwaggerDocument();
            services.AddSwaggerExamples();
            services.ConfigureSwaggerGen(options => { options.CustomSchemaIds(m => m.FullName); });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Swagger XML Api Demo",
                    Version = "v1",
                    Description = "A simple \"Personnel department\" ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Uzcard Team", Email = "", Url = "https://qrpay.uz" } //,
                    //License = new License { Name = "Use under LICX", Url = "https://qrpay.uz" }
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description =
                        "Заголовок авторизации JWT с использованием схемы Bearer. Пример: \"Authorization: Bearer { token }\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }}
                });

                c.ExampleFilters();

#pragma warning disable CS0618 // Type or member is obsolete
                c.OperationFilter<AddFileParamTypesOperationFilter
                >(); // Adds an Upload button to endpoints which have [AddSwaggerFileUploadButton]
#pragma warning restore CS0618 // Type or member is obsolete
                //c.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false); // adds any string you like to the request headers - in this case, a correlation id
                c.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]
                c.OperationFilter<SecurityRequirementsOperationFilter>(); // [SwaggerResponseHeader]
                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);
            });
        }*/
