using MauiApp4.Interfaces;
using MauiApp4.ViewModels;

namespace MauiApp4;

public partial class App : Application
{
    public App(IWindowService windowService)
    {
        InitializeComponent();
        MainPage = windowService.GetAndCreateContentPage<TaskListPageViewModel>().View;
    }
}
