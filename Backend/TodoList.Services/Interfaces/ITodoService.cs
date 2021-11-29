using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Models.ViewModels;

namespace TodoList.Services.Interfaces
{
    public interface ITodoService
    {
        Task<TodoItemVm> GetTodoItem(Guid id);
        Task<IEnumerable<TodoItemVm>> GetTodoItems();
        Task<TodoItemVm> PostTodoItem(TodoItemVm todoItemVm);
        Task PutTodoItem( TodoItemVm todoItemVm);
        bool TodoItemIdExists(Guid id);

        bool TodoItemDescriptionExists(string description);
    }
}