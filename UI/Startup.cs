using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UI.Data;
using UI.Helpers;
using UI.Models;

namespace UI
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
            services.AddSession(opt => {
                opt.IdleTimeout = TimeSpan.FromMinutes(15);
            });
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //.AddDbContext() : Singleton()
            services.AddDbContext<MyDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("MyShop")));

            services.AddAutoMapper();


            //Khai báo để sử dụng Session ở class  (.AddTransient() giá trị khác nhau mỗi lần request)
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ICartService, CartService>();

            //đăng ký MailConfig dạng service để tiêm (inject) vào nơi cầu sử dụng
            services.Configure<MailConfig>(Configuration.GetSection("MailConfig"));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(options =>
 {
     options.LoginPath = "/Customer/Login";
     options.AccessDeniedPath = "/Customer/AccessDenied";
     options.LogoutPath = "/Customer/Logout";
 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            
            app.UseCookiePolicy();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
