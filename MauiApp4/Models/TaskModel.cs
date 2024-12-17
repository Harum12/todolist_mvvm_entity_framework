using System.ComponentModel.DataAnnotations;

namespace MauiApp4.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Day_of_week { get; set; }
    }
}