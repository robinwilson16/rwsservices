using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWSServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RWSServices.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace RWSServices
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ApplicationDbContext context,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration
        )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //Avoid CORS errors with JS files served over CDN
            app.UseCors(builder => builder.WithOrigins(
                "http://www.rwsservices.net",
                "https://www.rwsservices.net",
                "http://rwsservices.net",
                "https://rwsservices.net"
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "FileUpload",
            //        template: "{controller=FileUpload}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("FileUpload", "{controller=FileUpload}/{action=Index}/{id?}");
            });

            CreateUsers.Initialize(context, userManager, roleManager, configuration).Wait();// seed here
            DbInitializer.Initialize(context, userManager, roleManager).Wait();// seed here
        }
    }
}
