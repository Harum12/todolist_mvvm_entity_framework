using MauiApp4.Infrastructure;
using MauiApp4.Interfaces;
using MauiApp4.Models;

namespace MauiApp4.Services
{
    public class DatabaseService(ApplicationContext context) : IDatabaseService
    {
        public int AddTask(TaskModel taskModel)
        {
            try
            {
                context.Tasks.Add(taskModel);
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public int UpdateTask(string task_id, string title, string description, string day)
        {
            try
            {
                var task = context.Tasks.FirstOrDefault(p => p.TaskID.ToString() == task_id);
                task.Title = title;
                task.Description = description;
                task.Day_of_week = day;

                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public List<TaskModel> GetAllTask() => context.Tasks.ToList();

        public int RemoveTask(string task_id)
        {
            try
            {
                var task = context.Tasks.FirstOrDefault(p => p.TaskID.ToString() == task_id);
                context.Tasks.Remove(task);
                context.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
    }
}
