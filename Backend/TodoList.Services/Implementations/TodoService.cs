using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DataAccess.Repositories.Interfaces;
using TodoList.Models.Entities;
using TodoList.Models.ViewModels;
using TodoList.Services.Interfaces;

namespace TodoList.Services.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly ITodoItemRepository _db;
        private readonly ILogger<TodoService> _logger;
        private readonly IMapper _mapper;

        public TodoService(ITodoItemRepository db, IMapper mapper, ILogger<TodoService> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TodoItemVm>> GetTodoItems()
        {
            var todoItemsList = await _db.GetAll(x => !x.IsCompleted);
            return _mapper.Map<IEnumerable<TodoItemVm>>(todoItemsList);
        }

        public async Task<TodoItemVm> GetTodoItem(Guid id)
        {
            var todoItem = await _db.GetFirstOrDefault(t => t.Id == id);
            return _mapper.Map<TodoItemVm>(todoItem);
        }

        public async Task PutTodoItem(TodoItemVm todoItemVm)
        {
            try
            {
                await _db.Update(_mapper.Map<TodoItem>(todoItemVm));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TodoItemVm> PostTodoItem(TodoItemAddVm todoItemVm)
        {
            try
            {
                var todoItem = await _db.Add(_mapper.Map<TodoItem>(todoItemVm));
                return _mapper.Map<TodoItemVm>(todoItem);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool TodoItemIdExists(Guid id)
        {
            return _db.GetAll(x => x.Id == id).GetAwaiter().GetResult().Any();
        }
        public bool TodoItemDescriptionExists(string description)
        {
            return _db.GetAll().GetAwaiter().GetResult()
               .Any(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);
        }

    }
}
