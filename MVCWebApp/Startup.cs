using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MVCWebApp.Data;
using MVCWebApp.Models.Home;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace MVCWebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //added lazy loading proxies because of null values during runtime in people/city lists
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseLazyLoadingProxies().
                UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddDefaultUI()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IProjectRepository, MockProjectRepository>(); //add repository
            services.AddScoped<IPersonRepository, PersonRepository>(); //add repository
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                //Default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );

                //Doctor route-> can be used with temperature directly: Doctor/37,9
                endpoints.MapControllerRoute(
                    name: "Doctor",
                    pattern: "Doctor/{temperature:maxlength(15)}",
                    defaults: new { controller = "Doctor", action = "CheckFever" });

                //GuessingGame route
                endpoints.MapControllerRoute(
                    name: "GuessingGame",
                    pattern: "GuessingGame",
                    defaults: new { controller = "GuessingGame", action = "SetSession" });

                //Person route
                endpoints.MapControllerRoute(
                    name: "Personlist",
                    pattern: "Personlist",
                    defaults: new { controller = "Person", action = "Index" });

                //AjaxPerson route
                endpoints.MapControllerRoute(
                    name: "Personlist",
                    pattern: "Personlist",
                    defaults: new { controller = "AjaxController", action = "Index" });
                endpoints.MapRazorPages();
            });

        }
    }
}
