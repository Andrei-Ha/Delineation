using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Delineation.Models;
using Microsoft.AspNetCore.Identity;
using CustomIdentity.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Delineation.Services;

namespace Delineation
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
            services.AddScoped<EmailService>();
            //string connString = Configuration.GetConnectionString("DefaultConnection");
            //string connString = Configuration.GetConnectionString("ConnectionAndr-SQL");
            //string connString = Configuration.GetConnectionString("ConnectionPirr2n");
            string connString = Configuration.GetConnectionString("ConnectionPirr11");
            services.AddDbContext<DelineationContext>(options => options.UseSqlServer(connString));
            //identity
            services.AddIdentity<User, IdentityRole>(oopt =>
            {
                oopt.Password.RequiredLength = 4;   // ����������� �����
                oopt.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
                oopt.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
                oopt.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
                oopt.Password.RequireDigit = false; // ��������� �� �����
                //oopt.User.AllowedUserNameCharacters = null; // ��������� ����� ������� � ����� �����
                oopt.User.AllowedUserNameCharacters = "�������������������������������������Ũ��������������������������- ";
            })
                .AddEntityFrameworkStores<DelineationContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                //options.Cookie.Name = "YourAppCookieName";
                //options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
            });
            //identity--end
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // this block added for run on Ngnix server
            /*app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });*/
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }*/
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Identity_area",
                    areaName: "Identity",
                    pattern: "identity/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
