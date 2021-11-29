using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoList.Models.ViewModels;
using TodoList.Services.Interfaces;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _svc;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoService svc, ILogger<TodoItemsController> logger)
        {
            _svc = svc;
            _logger = logger;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var results = await _svc.GetTodoItems();
            return Ok(results);
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            var result = await _svc.GetTodoItem(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItemVm todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            

            try
            {
                await _svc.PutTodoItem(todoItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemIdExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItemVm todoItem)
        {
            if (string.IsNullOrEmpty(todoItem?.Description))
            {
                return BadRequest("Description is required");
            }
            else if (TodoItemDescriptionExists(todoItem.Description))
            {
                return BadRequest("Description already exists");
            } 
            var retObj =  await _svc.PostTodoItem(todoItem);
                         
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        } 

        private bool TodoItemIdExists(Guid id)
        {
            return _svc.TodoItemIdExists(id);
            
        }

        private bool TodoItemDescriptionExists(string description)
        {
            return _svc.TodoItemDescriptionExists(description);
        }
    }
}
