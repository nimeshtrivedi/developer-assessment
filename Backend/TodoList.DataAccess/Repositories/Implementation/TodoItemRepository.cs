using TodoList.DataAccess.Repositories.Interfaces;
using TodoList.Models.Entities;

namespace TodoList.DataAccess.Repositories.Implementation
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext db) : base(db)
        {
        }
    }
}
