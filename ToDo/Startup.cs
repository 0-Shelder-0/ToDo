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
using ToDo.Data.Interfaces;
using ToDo.Data.Repositories;

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
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => { options.LoginPath = new PathString("/login"); });

            services.AddControllersWithViews();
            services.AddRazorPages()
                    .AddRazorRuntimeCompilation();

            services.AddDbContext<ApplicationDbContext>(options =>
                                                            options.UseLazyLoadingProxies()
                                                                   .UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IColumnRepository, ColumnRepository>();
            services.AddTransient<IRecordRepository, RecordRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IThumbnailRepository, ThumbnailRepository>();
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
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                                    {
                                        context.Context.Response.Headers.Add("Cache-Control", "public,max-age=1800");
                                    }
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute(
                                     name: "default",
                                     pattern: "{controller=home}/{action=index}/{id?}");
                             });

            app.UseStatusCodePagesWithRedirects("/error");
        }
    }
}
