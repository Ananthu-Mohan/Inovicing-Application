using InvoicingApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using InvoicingApplication.Models;

namespace InvoicingApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddScoped<LoginModelClass>();
            builder.Services.AddDbContext<LoginDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));
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
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Identity}/{action=LoginPage}/{id?}");
                //pattern: "{controller=Dashboard}/{action=DashboardPage}/{id?}");
            app.Run();
        }
    }
}