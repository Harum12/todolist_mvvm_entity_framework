using MauiApp4.Base;
using MauiApp4.Interfaces;
using MauiApp4.Models;

namespace MauiApp4.Services
{
    public sealed class WindowService(IServiceProvider serviceProvider) : IWindowService
    {
        private readonly List<WindowModel<ContentView, BaseViewModel>> _views = new();
        private WindowModel<TView, BaseViewModel> CreateVisualElement<TView, TViewModel>()
            where TView : VisualElement, new()
            where TViewModel : BaseViewModel
        {
            var type = typeof(TViewModel);
            var viewModel = serviceProvider.GetRequiredService<TViewModel>();

            var view = Activator.CreateInstance(Type.GetType($"{type.Namespace.Replace("ViewModel", "View")}.{type.Name.Replace("ViewModel", "View")}")) as TView;
            view.BindingContext = viewModel;

            var window = new WindowModel<TView, BaseViewModel> { View = view, ViewModel = viewModel };

            return window;
        }
        public WindowModel<ContentView, BaseViewModel> CreateContentView<TViewModel>() where TViewModel : BaseViewModel
        {
            var view = CreateVisualElement<ContentView, TViewModel>();
            _views.Add(view);
            return view;
        }
        public WindowModel<ContentView, BaseViewModel> GetView<TViewModel>() where TViewModel : BaseViewModel =>
            _views.FirstOrDefault(p => p.ViewModel.GetType() == typeof(TViewModel)) ?? CreateContentView<TViewModel>();
        public WindowModel<ContentPage, BaseViewModel> GetAndCreateContentPage<TViewModel>() where TViewModel : BaseViewModel => CreateVisualElement<ContentPage, TViewModel>();
    }
}
