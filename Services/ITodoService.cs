using ListToDo.Model;

namespace ListToDo.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task AddAsync(TodoItem? item);
        Task UpdateAsync(TodoItem? item, int Id);
        Task DeleteAsync(int id);
    }
}
