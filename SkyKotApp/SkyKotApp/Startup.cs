using KotClassLibrary.Helpers;
using KotClassLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SkyKotApp.Data;
using SkyKotApp.Services.Blazor;
using SkyKotApp.Services.General;
using SkyKotApp.Services.Login;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            services.AddHttpContextAccessor();

            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddServerSideBlazor().AddCircuitOptions(option => { option.DetailedErrors = true; });

            #region Culture
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                opt.DefaultRequestCulture = new RequestCulture(SuportedCultresHelper.English);
                opt.SupportedCultures = SuportedCultresHelper.GetCultrueInfoList;
                opt.SupportedUICultures = SuportedCultresHelper.GetCultrueInfoList;
            });

            services.AddMvc().AddViewLocalization(
                LanguageViewLocationExpanderFormat.Suffix
                )
                .AddDataAnnotationsLocalization();
            #endregion

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
            services.AddScoped<ISkyKotRepository, SkyKotPartialRepository>();
            services.AddScoped<IBlazorRepository, BlazorRepository>();

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
                options.ClientId = "446317286664-9ijkf6mbo7fjjqkebh9pm4jluu3kv9of.apps.googleusercontent.com";
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
            //app.UseRequestLocalization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            //var localizationOptions = new RequestLocalizationOptions()
            //    .SetDefaultCulture(SuportedCultresHelper.English)
            //    .AddSupportedCultures(SuportedCultresHelper.GetList)
            //    .AddSupportedUICultures(SuportedCultresHelper.GetList);

            //app.UseRequestLocalization(localizationOptions);

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapBlazorHub();
            });
            Data.Default.SeedData.EnsurePopulated(app).Wait();
        }
    }
}
