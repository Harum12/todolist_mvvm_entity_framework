using MauiApp4.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiApp4.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TaskModel> Tasks => Set<TaskModel>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) => Database.EnsureCreated();
    }
}

