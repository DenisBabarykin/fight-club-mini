using FightClub.Core;
using FightClub.FileUtils;

namespace FightClub.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews().AddNewtonsoftJson();
        builder.Services.AddSingleton<IActionExtractor, ActionExtractor>();
        builder.Services.AddSingleton<IJokeExtractor, JokeExtractor>();
        builder.Services.AddSingleton<IFightClubFacade, FightClubMockFacade>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        if (app.Environment.IsDevelopment())
        {
            app.Run();
        }
        else
        {
            app.Run("http://0.0.0.0:5000");
        }
    }
}