using Management_Hotel_2025.Models;
using Management_Hotel_2025.Serives.AuthenSerive;
using Management_Hotel_2025.Serives.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Reflection.Metadata;
namespace Management_Hotel_2025
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ManagermentHotelContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));


            // AddAuthentication là Bật hệ thống xác thực cho ứng dụng
            builder.Services
            .AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = "Google";
            }) 
            .AddCookie(options =>
            {
                options.LoginPath = "/Authen/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Authen/Denied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            }).AddGoogle("Google", options =>
            {
                options.ClientId = builder.Configuration.GetSection("GoogleKeys:GoogleID").Value;
                options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:GoogleSecret").Value;
            }); ;

            // CookieAuthenticationDefaults trong ASP.NET Core thực chất là một class chứa các hằng  số (constant) được Microsoft định nghĩa sẵn để dùng cho cấu hình Cookie Authentication.

            // bản chất nó là builder.Services.AddAuthentication("Cookies").AddCookie(); nhưng khuyếch nghị dùng (CookieAuthenticationDefaults.AuthenticationScheme) để tránh lỗi chính tả và dễ dàng thay đổi trong tương lai.




            builder.Services.AddSingleton<IEncoding, MyEncoding>();
            builder.Services.AddScoped<RegisterAccount>();
            builder.Services.AddScoped<ValidationAuthen>();
            builder.Services.AddScoped<Login>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            /*            Nếu người dùng truy cập vào địa chỉ gốc của website(ví dụ: /),
                                 thì sẽ chuyển tới controller là Home và action là Index. 

                                  { id ?} là tham số tùy chọn(có hoặc không đều được).*/
            app.Run();
        }
    }
}
