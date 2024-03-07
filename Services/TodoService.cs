using ListToDo.Model;
using ListToDo.Repositories;
using System.Text;

namespace ListToDo.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Task AddAsync(TodoItem? item)
        {
            StringBuilder validation = Validations(item);
            if (validation.Length > 0) { throw new Exception(validation.ToString()); }
#pragma warning disable CS8604 // Possible null reference argument.
            return _todoRepository.AddAsync(item);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public Task DeleteAsync(int id)
        {
           return _todoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _todoRepository.GetAllAsync();
        }

        public Task<TodoItem?> GetByIdAsync(int id)
        {
            return _todoRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(TodoItem? item,int Id)
        {
            StringBuilder validation = Validations(item);
            if (validation.Length>0) { throw new Exception(validation.ToString()); }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            item.Id =Id;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return _todoRepository.UpdateAsync(item);
        }

        /// <summary>
        /// method to validate item object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private StringBuilder Validations(TodoItem? item)
        {
            StringBuilder errors = new StringBuilder();
            if (item is null) { 
              return  errors.AppendLine("To Do Item is empty"); 
            }
            if (string.IsNullOrEmpty(item?.Title))
            {
                errors.AppendLine("Title must have a value");
            }
            if (string.IsNullOrEmpty(item?.Description))
            {
                errors.AppendLine("Description must have a value");
            }
            return errors;
        }

    }
}
