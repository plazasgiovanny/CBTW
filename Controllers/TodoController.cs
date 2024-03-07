using ListToDo.Model;
using ListToDo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ListToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
        {
            var todoItems = await _todoService.GetAllAsync();
            return Ok(todoItems);
        }
        [Route("{Id:int}")]
        [HttpGet]
        public async Task<ActionResult<TodoItem>> GetById(int Id)
        {
            var todoItems = await _todoService.GetByIdAsync(Id);
            return Ok(todoItems);
        }

        [Route("AddTodoItem")]
        [HttpPost]
        public async Task<ActionResult> AddTodoItem()
        {
            try {
                var bodyStream = new StreamReader(HttpContext.Request.Body);
                var bodyData = await bodyStream.ReadToEndAsync();
                if (string.IsNullOrEmpty(bodyData)) { return BadRequest(); }
                TodoItem? todoItem = JsonSerializer.Deserialize<TodoItem>(bodyData);
                await _todoService.AddAsync(todoItem);
                return Ok();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [Route("UpdateTodoItem/{Id:int?}")]
        [HttpPut]
        public async Task<ActionResult> UpdateTodoItem( int? Id)
        {
            try
            {
                if(Id is null || Id == 0) { return BadRequest(); }

                var bodyStream = new StreamReader(HttpContext.Request.Body);
                var bodyData = await bodyStream.ReadToEndAsync();
                
                if (string.IsNullOrEmpty(bodyData)) { return BadRequest(); }

                TodoItem? todoItem = JsonSerializer.Deserialize<TodoItem>(bodyData);
                await _todoService.UpdateAsync(todoItem,Id.Value);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Route("Delete/{Id:int}")]
        [HttpDelete]
        public async Task<ActionResult<TodoItem>> DeleteById(int Id)
        {
             await _todoService.DeleteAsync(Id);
            return Ok();
        }


        // Implementa los demás métodos HTTP (POST, PUT, DELETE) según sea necesario
    }
}