using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Создать базу данных[y|n]:");
        // string? answer = Console.ReadLine();
        bool creat_database = false;

        // if (answer == "y")
        // {
        //     creat_database = true;
        // }

        using (var context = new Context(creat_database)){}

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => 
            {
                options.LoginPath = "/Authorization/Autho";
                options.AccessDeniedPath = "/Access/Denied";
            });
        builder.Services.AddAuthorization(opts => 
        {
            opts.AddPolicy("Admin", police =>
            {
                police.RequireClaim(ClaimTypes.Role, "0");
            });
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}