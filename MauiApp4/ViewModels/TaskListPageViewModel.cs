using MauiApp4.Base;
using MauiApp4.Interfaces;
using MauiApp4.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace MauiApp4.ViewModels
{
    public partial class TaskListPageViewModel : BaseViewModel
    {
        private ObservableCollection<TaskModel> _myTasks;
        private bool _isChecked;

        private readonly IDatabaseService _databaseService;
        private readonly IWindowService _windowService;
        public TaskListPageViewModel(IDatabaseService databaseService, IWindowService windowService)
        {
            _databaseService = databaseService;
            _windowService = windowService;

            AddUpdateCommand = new RelayCommand(AddUpdateTaskButton);
            EditTaskCommand = new RelayCommand(EditTaskButton);
            DeleteTaskCommand = new RelayCommand(DeleteTaskButton);
            DisplayActionCommand = new RelayCommand(DisplayActionButton);

            MyTasks = new ObservableCollection<TaskModel>();

            Preferences.Clear();

            InitializationTask();
        }

        #region ICommand
        public ICommand AddUpdateCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand DisplayActionCommand { get; }
        #endregion

        #region View Property
        public ObservableCollection<TaskModel> MyTasks
        {
            get => _myTasks;
            set
            {
                _myTasks = value;
                OnPropertyChanged(nameof(MyTasks));
            }
        }
        public bool CheckedBox
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(CheckBox));
            }
        }
        #endregion


        #region ButtonHandler
        private void InitializationTask()
        {
            MyTasks.Clear();
            _databaseService.GetAllTask().ForEach(p =>
            {
                MyTasks.Add(p);
            });
        }
        private void AddUpdateTaskButton(object parameter)
        {
            Application.Current.MainPage = _windowService.GetAndCreateContentPage<AddUpdateTaskDetailViewModel>().View;
        }

        private void EditTaskButton(object parameter)
        {
            
        }

        private void DeleteTaskButton(object parameter)
        {
            _databaseService.GetAllTask().ForEach(p =>
            {
                Application.Current.MainPage.DisplayAlert("Внимание", p.TaskID.ToString() + p.Title + p.Description, "OK");
            });
        }

        private async void DisplayActionButton(object parameter)
        {
            var response = await Application.Current.MainPage.DisplayActionSheet("Выберите действие", "OK", null, "Редактировать", "Удалить");

            if (response == "Редактировать")
            {
                Preferences.Set("task_id", parameter.ToString());
                Application.Current.MainPage = _windowService.GetAndCreateContentPage<AddUpdateTaskDetailViewModel>().View;
            }
            else if (response == "Удалить")
            {
                try
                {
                    var delResponse = _databaseService.RemoveTask(parameter.ToString());
                    if (delResponse > 0)
                    {
                        InitializationTask();
                        await Application.Current.MainPage.DisplayAlert("Внимание", "Задача успешно удалена!", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", "При удалении произошла ошибка, попробуйте позже", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "При удалении произошла ошибка, попробуйте позже", "OK");
                }
            }
        }
        #endregion
    }
}
