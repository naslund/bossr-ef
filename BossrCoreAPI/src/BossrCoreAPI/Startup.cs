using System.Text;
using BossrCoreAPI.Models.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BossrCoreAPI
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Data:Keys:SecretKey"]));

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationDbContext>(options => 
                    options.UseSqlServer(Configuration["Data:ConnectionStrings:DefaultConnection"]));
            
            services.AddIdentity<ApplicationUser, ApplicationRole>(x => x.User.RequireUniqueEmail = false)
                .AddEntityFrameworkStores<ApplicationDbContext, int>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddDefaultTokenProviders();

            services.AddOpenIddict<ApplicationUser, ApplicationRole, ApplicationDbContext, int>()
                .EnableTokenEndpoint("/connect/token")
                .AllowPasswordFlow()
                .AllowRefreshTokenFlow()
                .UseJsonWebTokens()
                .AddSigningKey(SigningKey);

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseOpenIddict();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Audience = Configuration["Data:Uris:Audience"],
                Authority = Configuration["Data:Uris:Authority"],
                TokenValidationParameters = new TokenValidationParameters { IssuerSigningKey = SigningKey }
            });

            app.UseMvc();
        }
    }
}
