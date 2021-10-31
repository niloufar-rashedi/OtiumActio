using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using OtiumActio.Interfaces;
using OtiumActio.Controllers;
using System.Reflection;
using OtiumActio.Helpers;
using Microsoft.IdentityModel.Logging;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using OtiumActio.EmailService;
using Microsoft.AspNetCore.Identity;

namespace OtiumActio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            var connectionString = Configuration.GetConnectionString("OtiumActioDb");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x=>x.LoginPath="/User/Login");
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);
            services.AddControllersWithViews();
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "cookie";
            //    options.DefaultChallengeScheme = "oidc";
            //})
            //    .AddCookie("cookie")
            //    .AddOpenIdConnect("oidc", options =>
            //    {
            //        options.Authority = Configuration["InteractiveServiceSettings:AuthorityUrl"];
            //        options.ClientId = Configuration["InteractiveServiceSettings:ClientId"];
            //        options.ClientSecret = Configuration["InteractiveServiceSettings:ClientSecret"];

            //        options.ResponseType = "code";
            //        options.UsePkce = true;
            //        options.ResponseMode = "query";

            //        options.Scope.Add(Configuration["InteractiveServiceSettings:Scopes:0"]);
            //        options.SaveTokens = true;
            //    });
            services.Configure<IdentityServerSettings>(Configuration.GetSection("IdentityServerSettings"));
            services.AddSingleton<ITokenService, TokenService>();

            services.AddDbContext<OtiumActioContext>(options =>
                options.UseSqlServer(connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            services.AddSession(options => {
                options.Cookie.Name = ".List.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEditActivityController, EditActivityController>();
            services.AddScoped<IUserHandler, UserHandler>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddHttpContextAccessor();
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //})
            //.AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
