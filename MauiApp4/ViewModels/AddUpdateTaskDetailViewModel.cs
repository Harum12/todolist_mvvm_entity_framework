using MauiApp4.Base;
using MauiApp4.Interfaces;
using MauiApp4.Models;
using System.Diagnostics;
using System.Windows.Input;

namespace MauiApp4.ViewModels
{
    public partial class AddUpdateTaskDetailViewModel : BaseViewModel
    {
        private string _title;
        private string _description;
        private string _selectedItem;

        private readonly IDatabaseService _databaseService;
        private readonly IWindowService _windowService;
        public AddUpdateTaskDetailViewModel(IDatabaseService databaseService, IWindowService windowService)
        {
            _databaseService = databaseService;
            _windowService = windowService;

            AddUpdateCommand = new RelayCommand(AddUpdateButton);
            GoToMenuCommand = new RelayCommand(GoToMenuButton);

            Title = "";
            Description = "";
            SelectedItem = "";

            _databaseService.GetAllTask().ForEach(p =>
            {
                if (p.TaskID.ToString() == Preferences.Get("task_id", String.Empty))
                {
                    Title = p.Title;
                    Description = p.Description;
                    SelectedItem = p.Day_of_week;
                }
            });
        }
        #region ICommand
        public ICommand AddUpdateCommand { get; }
        public ICommand GoToMenuCommand { get; }
        #endregion

        #region View Property
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        //public string Day
        //{
        //    get => _day;
        //    set
        //    {
        //        _day = value;
        //        OnPropertyChanged(nameof(Day));
        //    }

        //}
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        #endregion

        #region ButtonHandler
        public void AddUpdateButton(object parameter)
        {
            try
            {
                if (Preferences.Get("task_id", String.Empty) == String.Empty)
                    _databaseService.AddTask(new TaskModel { TaskID = _databaseService.GetAllTask().Count() + 1, Title = Title, Description = Description, Day_of_week = SelectedItem });
                else
                    _databaseService.UpdateTask(Preferences.Get("task_id", String.Empty), Title, Description, SelectedItem);

                Application.Current.MainPage.DisplayAlert("Успех", "Заметка успешно сохранена!", "ОК");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Application.Current.MainPage.DisplayAlert("Ошибка", "При сохранении произошла ошибка", "ОК");
            }

            Application.Current.MainPage = _windowService.GetAndCreateContentPage<TaskListPageViewModel>().View;
        }
        public void GoToMenuButton(object parameter) => Application.Current.MainPage = _windowService.GetAndCreateContentPage<TaskListPageViewModel>().View;
        #endregion
    }
}
