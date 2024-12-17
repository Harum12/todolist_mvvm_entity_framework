using MauiApp4.Infrastructure;
using MauiApp4.Interfaces;
using MauiApp4.Services;
using MauiApp4.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MauiApp4;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlite($"Filename={Path.Combine(FileSystem.AppDataDirectory, "TaskDatabase.db")}");
        });

        builder.Services.AddTransient<IDatabaseService, DatabaseService>();
        builder.Services.AddTransient<IWindowService, WindowService>();

        builder.Services.AddTransient<TaskListPageViewModel>();
        builder.Services.AddTransient<AddUpdateTaskDetailViewModel>();

        builder.Logging.AddDebug();
        return builder.Build();
    }
}
