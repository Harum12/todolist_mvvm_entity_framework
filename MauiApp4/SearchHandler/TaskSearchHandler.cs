using MauiApp4.Models;
namespace MauiApp4.SearchHandlers
{
    public class TaskSearchHandler : SearchHandler
    {
        public IList<TaskModel> Tasks { get; set; }
        public string NavigationRoute { get; set; }
        public Type NavigationType { get; set; }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                //ItemsSource = Tasks.Where(task => task.FullName.ToLower().Contains(newValue.ToLower())).ToList();
            }
        }
        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            var navParam = new Dictionary<string, object>();
            navParam.Add("TaskDetail", item);
            if (!string.IsNullOrWhiteSpace(NavigationRoute))
            {
                await Shell.Current.GoToAsync(NavigationRoute, navParam);
            }
        }
    }
}
