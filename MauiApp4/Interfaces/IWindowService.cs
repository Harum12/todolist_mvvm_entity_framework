using MauiApp4.Base;
using MauiApp4.Models;

namespace MauiApp4.Interfaces
{
    public interface IWindowService
    {
        WindowModel<ContentView, BaseViewModel> CreateContentView<TViewModel>() where TViewModel : BaseViewModel;
        WindowModel<ContentView, BaseViewModel> GetView<TViewModel>() where TViewModel : BaseViewModel;
        WindowModel<ContentPage, BaseViewModel> GetAndCreateContentPage<TViewModel>() where TViewModel : BaseViewModel;
    }
}
