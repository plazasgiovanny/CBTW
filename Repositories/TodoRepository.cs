using Entities.DataContext;
using ListToDo.Model;
using Microsoft.EntityFrameworkCore;

namespace ListToDo.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ModelDbContext _context;

        public TodoRepository(ModelDbContext context)
        {
            _context = context;
        }

        public   Task AddAsync(TodoItem item)
        {
            _context.Entry<TodoItem>(item).State = EntityState.Added;
           
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Entry<TodoItem>(item).State = EntityState.Deleted;
                _context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public Task<TodoItem?> GetByIdAsync(int id)
        {
            return _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(TodoItem item)
        {
            if(item.Id > 0)
            {
              _context.Entry<TodoItem>(item).State = EntityState.Modified;
              _context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }

    }

}
