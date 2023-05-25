using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PROG3A_POE.Data;
using PROG3A_POE.Models;


namespace PROG3A_POE
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication("SessionAuthentication")
            .AddCookie("SessionAuthentication", options =>
            {
                options.LoginPath = "/User/Login"; // Customize the login path
            });

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "CookieSession";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            //Creating specific roles 
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("EmployeeOnly", policy => policy.RequireRole("Employee"));
                options.AddPolicy("FarmerOnly", policy => policy.RequireRole("Farmer"));
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.Zero; // Cookie expires immediately
                options.SlidingExpiration = false; // Disable sliding expiration
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}");

            //Creating a scope
            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    var roles = new[] { "Employee", "Farmer" };
            //    foreach (var role in roles)
            //    {
            //        if (!await roleManager.RoleExistsAsync(role))
            //        {
            //            await roleManager.CreateAsync(new IdentityRole(role));
            //        }
            //    }
            //}

            //Creating a scope for admin
            //using (var scope = app.Services.CreateScope())
            //{
            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //    string UserID = "Emp1";
            //    string Password = "Pass1";

            //    var roles = new[] { "Employee", "Farmer" };
            //    foreach (var role in roles)
            //    {
            //        if (await userManager.FindByIdAsync(UserID) == null)
            //        {
            //           var user = new IdentityUser();
            //            user.Id = UserID;

            //            await userManager.CreateAsync(user, Password);
            //            await userManager.AddToRoleAsync(user,"Employee");
            //        }
            //    }
            //}


            app.Run();
            }
        }
    }