using KotClassLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkyKotApp.Data;
using SkyKotApp.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp
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
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddServerSideBlazor();

            services.AddDbContextPool<AppDbContext>(
                        options => options.UseSqlServer(Configuration.GetConnectionString("SkyKotConnString"))
                    );

            services.AddIdentity<CustomUser, IdentityRole>(
            options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<CustomUser>>(TokenOptions.DefaultProvider);

            //services
            services.AddScoped<ILoginRepository, LoginRepository>();
            //External Login
            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters += " '";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication().AddFacebook(fbOpts =>
            {
                fbOpts.AppId = "350430243670950";
                fbOpts.AppSecret = "a375f81c37573055a7633590fbe0c2e7";
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "305914282863-52n4vs661qqqdhkr5mdsek9faap5d3fe.apps.googleusercontent.com";
                options.ClientSecret = "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";
                options.SignInScheme = IdentityConstants.ExternalScheme;
            });
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