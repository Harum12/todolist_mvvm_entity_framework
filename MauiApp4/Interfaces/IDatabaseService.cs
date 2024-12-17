using MauiApp4.Models;

namespace MauiApp4.Interfaces
{
    public interface IDatabaseService
    {
        int AddTask(TaskModel taskModel);
        int UpdateTask(string task_id, string title, string description, string day);
        int RemoveTask(string task_id);
        List<TaskModel> GetAllTask();
    }
}
