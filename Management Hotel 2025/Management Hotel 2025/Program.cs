

using Management_Hotel_2025.Modules.AuthenSerive;
using Management_Hotel_2025.Modules.ManagementQRCode;
using Management_Hotel_2025.Modules.Notifications.NotificationsSevices;
using Management_Hotel_2025.Modules.Rooms.ManagementRoom;
using Management_Hotel_2025.Modules.Rooms.RoomService;
using Management_Hotel_2025.Serives.AuthenSerive;
using Management_Hotel_2025.Serives.CallAPI;
using Management_Hotel_2025.Serives.GenarateToken;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;


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
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
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
                options.ClaimActions.MapJsonKey("avatar", "picture", "url");         // Lấy giá trị picture trong JSON của Google và lưu nó vào Claims với tên ‘avatar’. 
            });


            builder.Services.AddSession();  //  đăng ký dịch vụ Session trong ứng dụng ASP.NET Core.

            // CookieAuthenticationDefaults trong ASP.NET Core thực chất là một class chứa các hằng  số (constant) được Microsoft định nghĩa sẵn để dùng cho cấu hình Cookie Authentication.

            // bản chất nó là builder.Services.AddAuthentication("Cookies").AddCookie(); nhưng khuyếch nghị dùng (CookieAuthenticationDefaults.AuthenticationScheme) để tránh lỗi chính tả và dễ dàng thay đổi trong tương lai.



            //Transient : mỗi lần inject sẽ là  1 instance mới  được tạo ra
            // Scoped   : 1 request sẽ inject duy nhất 1 instance 
            //Singleton : 1 instance sẽ tồn tại đến hết vòng đời của ứng dụng  (tất cả các lần inject đều dùng chung 1 instance)
            builder.Services.AddTransient<INotifications, Email>();
            builder.Services.AddScoped<IVnPayService, VnPayService>();
            builder.Services.AddSingleton<IEncoding, MyEncoding>();
            builder.Services.AddScoped<RegisterAccount>();
            builder.Services.AddScoped<ValidationAuthen>();
            builder.Services.AddScoped<Login>();
            builder.Services.AddTransient<GenarateTokenHotel>();
            builder.Services.AddTransient<IApiServices, ApiCall>();
            builder.Services.AddTransient<ApiCall>();
            builder.Services.AddHttpClient(); // Thêm HttpClient để gọi API bên ngoài
            builder.Services.AddHttpContextAccessor();  // Thêm HttpContextAccessor để truy cập HttpContext trong các dịch vụ

            builder.Services.AddTransient<IRoomService, RoomSerices>();
            builder.Services.AddTransient<IManagementRoom, FilterRooms>();
            builder.Services.AddTransient<IManagementBooking, ManagementBooking>();

            builder.Services.AddTransient<IGanarateQRCode, QRCodeBookingDetail>();

            builder.Services.AddTransient<IReceptionService, ReceptionService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();             // Kích hoạt Session trong ứng dụng
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Intro}/{id?}");
            /*            Nếu người dùng truy cập vào địa chỉ gốc của website(ví dụ: /),
                                 thì sẽ chuyển tới controller là Home và action là Index. 

                                  { id ?} là tham số tùy chọn(có hoặc không đều được).*/
            app.Run();
        }
    }
}
