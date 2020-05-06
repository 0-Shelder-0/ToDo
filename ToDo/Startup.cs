using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDo.Data;
using ToDo.Interfaces;

namespace ToDo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options
                          .UseLazyLoadingProxies()
                          .UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => { options.LoginPath = new PathString("/Authorization/Login"); });
            services.AddControllersWithViews();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IColumnRepository, ColumnRepository>();
            services.AddTransient<IRecordRepository, RecordRepository>();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute(
                                     name: "home",
                                     pattern: "{controller=Board}/{action=Boards}");
                                 endpoints.MapControllerRoute(
                                     name: "board",
                                     pattern: "Board/Board/{id}");
                             });
            app.UseStatusCodePagesWithRedirects("/Home/Error");
        }
    }
}
