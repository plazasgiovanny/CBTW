
using ListToDo.Model;
using Microsoft.EntityFrameworkCore;

namespace Entities.DataContext
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }

    }
}
